// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Paytools.NationalInsurance;

using Paytools.NationalInsurance.ReferenceData;
using static NiThreshold;

public class NiCalculator : INiCalculator
{
    private const int Step3 = 2;
    private const int Step4 = 3;
    private const int Step5 = 4;
    private const int Step6 = 5;

    private readonly INiReferenceDataProvider _referenceDataProvider;

    private static readonly (NiThreshold, NiThreshold)[] _thresholdPairs =
        { (LEL, ST), (ST, PT), (PT, FUST), (FUST, UEL) };

    public const int CalculationStepCount = 6;

    public NiCalculator(INiReferenceDataProvider referenceDataProvider)
    {
        _referenceDataProvider = referenceDataProvider;
    }

    public NiCalculationResult Calculate(decimal nicableEarningsInPeriod, NiCategory niCategory)
    {
        NiPeriodThresholdSet thresholds = _referenceDataProvider.GetThresholdsForCategory(niCategory) ??
            throw new InvalidOperationException($"Unable to obtain National Insurance thresholds for category {niCategory}");

        decimal[] results = new decimal[CalculationStepCount];

        // Step 1: Earnings up to and including LEL. If answer is negative no NICs due and no recording required. If answer is zero
        // or positive record result and proceed to Step 2.

        decimal threshold = thresholds.GetThreshold(LEL);
        decimal resultOfStep1 = nicableEarningsInPeriod - threshold;

        if (resultOfStep1 < 0.0m)
            return NiCalculationResult.NoRecordingRequired;

        results[0] = threshold;

        // Step 2: Earnings above LEL up to and including ST. If answer is zero (or negative) no NICs due and the payroll record
        // should be zero filled. If answer is positive enter on the payroll record and proceed to Step 3.
        // Step 3: Earnings above ST up to and including PT. If answer is zero (or negative) no NICs due and the payroll record
        // should be zero filled. If answer is positive enter on the payroll record and proceed to Step 4.
        // Step 4: Earnings above PT up to and including FUST. If answer is zero (or negative) enter result of calculation
        // of Step 3 on the payroll record. If answer is positive enter result of Step 3 on the payroll record and proceed to Step 5.
        // Step 5: Earnings above FUST up to and including UEL. If answer is zero (or negative) enter result of calculation
        // of Step 4 on the payroll record. If answer is positive enter result of Step 4 on the payroll record and proceed to Step 6.

        decimal previousEarningsAboveThreshold = resultOfStep1;

        for (int stepIndex = 0; stepIndex < _thresholdPairs.Length; stepIndex++)
        {
            decimal upperThreshold = thresholds.GetThreshold1(_thresholdPairs[stepIndex].Item2);
            decimal earningsAboveUpperThreshold = Math.Max(0.0m, nicableEarningsInPeriod - upperThreshold);

            decimal resultOfStep = previousEarningsAboveThreshold - earningsAboveUpperThreshold;

            if (resultOfStep == 0.0m)
                break;

            results[stepIndex + 1] = decimal.Round(resultOfStep, 4, MidpointRounding.ToZero);
            previousEarningsAboveThreshold = earningsAboveUpperThreshold;
        }

        // Step 6: Earnings above UEL. If answer is zero or negative no earnings above UEL.

        results[CalculationStepCount - 1] = Math.Max(0.0m, nicableEarningsInPeriod - thresholds.GetThreshold1(UEL));

        // Step 7: Calculate employee NICs
        // Step 8: Calculate employer NICs

        INiCategoryRateEntry rates = _referenceDataProvider.GetRatesForCategory(niCategory) ??
            throw new InvalidOperationException($"Unable to obtain National Insurance rates for category {niCategory}");

        return new NiCalculationResult(GetNiEarningsBreakdownFromCalculationResults(results),
            CalculateEmployeesNi(rates, results),
            CalculateEmployersNi(rates, results));
    }

    public NiCalculationResult CalculateDirectors(decimal nicableSalaryInPeriod, NiCategory niCategory)
    {
        throw new NotImplementedException();
    }

    private decimal CalculateEmployeesNi(INiCategoryRateEntry rates, decimal[] calculationStepResults)
    {
        if (calculationStepResults.Length != CalculationStepCount)
            throw new InvalidOperationException($"Unexpected number of calculation step results; should be {NiCalculator.CalculationStepCount}, was {calculationStepResults.Length}");

        return ((calculationStepResults[Step4] + calculationStepResults[Step5]) * rates.EmployeeRatePTToUEL)
            .NiRound() +
            (calculationStepResults[Step6] * rates.EmployeeRateAboveUEL)
            .NiRound();
    }

    private decimal CalculateEmployersNi(INiCategoryRateEntry rates, decimal[] calculationStepResults)
    {
        if (calculationStepResults.Length != CalculationStepCount)
            throw new InvalidOperationException($"Unexpected number of calculation step results; should be {NiCalculator.CalculationStepCount}, was {calculationStepResults.Length}");

        // From HMRC documentation: Step 3 plus Step 4 multiplied by employer’s band D % rate(don’t round) = a
        // PLUS
        // Step 5 multiplied by employer’s band E % rate (don’t round) = b
        // a plus b = (round)
        // PLUS
        // Step 6 multiplied by employer’s band F % rate(round).

        return ((calculationStepResults[Step3] + calculationStepResults[Step4]) * rates.EmployerRateSTtoFUST +
            calculationStepResults[Step5] * rates.EmployerRateFUSTtoUEL)
            .NiRound() +
            (calculationStepResults[Step6] * rates.EmployerRateAboveUEL)
            .NiRound();
    }

    private static NiEarningsBreakdown GetNiEarningsBreakdownFromCalculationResults(decimal[] calculationStepResults)
    {
        if (calculationStepResults.Length != NiCalculator.CalculationStepCount)
            throw new InvalidOperationException($"Unexpected number of calculation step results; should be {NiCalculator.CalculationStepCount}, was {calculationStepResults.Length}");

        return new NiEarningsBreakdown()
        {
            EarningsUpToAndIncludingLEL = calculationStepResults[0],
            EarningsAboveLELUpToAndIncludingST = calculationStepResults[1],
            EarningsAboveSTUpToAndIncludingPT = calculationStepResults[2],
            EarningsAbovePTUpToAndIncludingFUST = calculationStepResults[3],
            EarningsAboveFUSTUpToAndIncludingUEL = calculationStepResults[4],
            EarningsAboveUEL = calculationStepResults[5],
            EarningsAboveSTUpToAndIncludingUEL = calculationStepResults[2] + calculationStepResults[3] + calculationStepResults[4]
        };
    }
}