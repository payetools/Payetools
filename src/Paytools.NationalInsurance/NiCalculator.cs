// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
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

using Paytools.Common.Model;
using Paytools.NationalInsurance.Extensions;
using Paytools.NationalInsurance.Model;
using Paytools.NationalInsurance.ReferenceData;
using System.Collections.ObjectModel;
using static Paytools.NationalInsurance.Model.NiThresholdType;

namespace Paytools.NationalInsurance;

/// <summary>
/// Represents a National Insurance calculator that implements <see cref="INiCalculator"/>.
/// </summary>
public class NiCalculator : INiCalculator
{
    // Step constants to assist readability of the code
    private const int Step1 = 0;
    private const int Step2 = 1;
    private const int Step3 = 2;
    private const int Step4 = 3;
    private const int Step5 = 4;
    private const int Step6 = 5;

    private readonly ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> _niRateEntriesForRegularEmployees;
    private readonly ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> _niRateEntriesForDirectors;
    private readonly INiThresholdSet _niAnnualThresholds;
    private readonly INiPeriodThresholdSet _niPeriodThresholds;

    private static readonly (NiThresholdType, NiThresholdType)[] _thresholdPairs = { (LEL, ST), (ST, PT), (PT, FUST), (FUST, UEL) };
    private static readonly (NiThresholdType, NiThresholdType)[] _directorThresholdPairs = { (LEL, ST), (ST, DPT), (DPT, FUST), (FUST, UEL) };

    internal const int CalculationStepCount = 6;

    /// <summary>
    /// Initialises a new <see cref="NiCalculator"/> with the supplied thresholds and rates for the period.
    /// </summary>
    /// <param name="niAnnualThresholds">NI threshold set for the full tax year applicable to this NI calculator.</param>
    /// <param name="niPeriodThresholds">NI threshold set for the tax period applicable to this NI calculator.</param>
    /// <param name="niRateEntriesForRegularEmployees">NI rates for the tax period applicable to non-directors for this NI calculator.</param>
    /// <param name="niDirectorsRateEntries">NI rates for the tax period applicable to directors for this NI calculator.  Optional;
    /// where the same set of rates applies to both non-directors and directors, this parameter should be null.</param>
    public NiCalculator(INiThresholdSet niAnnualThresholds,
        INiPeriodThresholdSet niPeriodThresholds,
        ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> niRateEntriesForRegularEmployees,
        ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry>? niDirectorsRateEntries = null)
    {
        _niAnnualThresholds = niAnnualThresholds;
        _niPeriodThresholds = niPeriodThresholds;
        _niRateEntriesForRegularEmployees = niRateEntriesForRegularEmployees;
        _niRateEntriesForDirectors = niDirectorsRateEntries ?? niRateEntriesForRegularEmployees;
    }

    /// <summary>
    /// Calculates the National Insurance contributions required for an employee for a given pay period,
    /// based on their NI-able salary and their allocated National Insurance category letter.
    /// </summary>
    /// <param name="niCategory">National Insurance category.</param>
    /// <param name="nicableEarningsInPeriod">NI-able salary for the period.</param>
    /// <param name="result">The NI contributions due via an instance of a type that implements <see cref="INiCalculationResult"/>.</param>
    public void Calculate(NiCategory niCategory, decimal nicableEarningsInPeriod, out INiCalculationResult result)
    {
        decimal[] results = new decimal[CalculationStepCount];

        // Step 1: Earnings up to and including LEL. If answer is negative no NICs due and no recording required. If answer is zero
        // or positive record result and proceed to Step 2.

        decimal threshold = _niPeriodThresholds.GetThreshold(LEL);
        decimal resultOfStep1 = nicableEarningsInPeriod - threshold;

        if (resultOfStep1 < 0.0m)
        {
            result = NiCalculationResult.NoRecordingRequired;

            return;
        }

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
            decimal upperThreshold = _niPeriodThresholds.GetThreshold1(_thresholdPairs[stepIndex].Item2);
            decimal earningsAboveUpperThreshold = Math.Max(0.0m, nicableEarningsInPeriod - upperThreshold);

            decimal resultOfStep = previousEarningsAboveThreshold - earningsAboveUpperThreshold;

            if (resultOfStep == 0.0m)
                break;

            results[stepIndex + 1] = decimal.Round(resultOfStep, 4, MidpointRounding.ToZero);
            previousEarningsAboveThreshold = earningsAboveUpperThreshold;
        }

        // Step 6: Earnings above UEL. If answer is zero or negative no earnings above UEL.

        results[CalculationStepCount - 1] = Math.Max(0.0m, nicableEarningsInPeriod - _niPeriodThresholds.GetThreshold1(UEL));

        // Step 7: Calculate employee NICs
        // Step 8: Calculate employer NICs

        if (!_niRateEntriesForRegularEmployees.TryGetValue(niCategory, out var rates))
            throw new InvalidOperationException($"Unable to obtain National Insurance rates for category {niCategory}");

        GetNiEarningsBreakdownFromCalculationResults(results, out var earningsBreakdown);

        result = new NiCalculationResult(
            niCategory,
            nicableEarningsInPeriod,
            rates,
            _niPeriodThresholds,
            earningsBreakdown,
            CalculateEmployeesNi(rates, results),
            CalculateEmployersNi(rates, results));
    }

    /// <summary>
    /// Calculates the National Insurance contributions required for a company director for a given pay period,
    /// based on their NI-able salary and their allocated National Insurance category letter.
    /// </summary>
    /// <param name="calculationMethod">Calculation method to use.</param>
    /// <param name="niCategory">National Insurance category.</param>
    /// <param name="nicableEarningsYearToDate">NI-able salary for the period.</param>
    /// <param name="employeesNiPaidYearToDate">Total employees NI paid so far this tax year up to and including the end of the
    /// previous period.</param>
    /// <param name="employersNiPaidYearToDate">Total employers NI paid so far this tax year up to and including the end of the
    /// previous period.</param>
    /// <param name="proRataFactor">Factor to apply to annual thresholds when the employee starts being a director part way through
    /// the tax year.  Null if not applicable.</param>
    /// <param name="result">The NI contributions due via an instance of a type that implements <see cref="INiCalculationResult"/>.</param>
    public void CalculateDirectors(
        DirectorsNiCalculationMethod calculationMethod,
        NiCategory niCategory,
        decimal nicableEarningsYearToDate,
        decimal employeesNiPaidYearToDate,
        decimal employersNiPaidYearToDate,
        decimal? proRataFactor,
        out INiCalculationResult result)
    {
        // TODO: This is broken as it uses the wrong rates
        if (calculationMethod == DirectorsNiCalculationMethod.AlternativeMethod)
        {
            Calculate(niCategory, nicableEarningsYearToDate, out result);

            return;
        }

        decimal[] results = new decimal[CalculationStepCount];

        INiThresholdSet thresholds;

        if (proRataFactor == null)
            thresholds = _niAnnualThresholds;
        else
            GetProRataAnnualThresholds((decimal)proRataFactor, out thresholds);

        // Step 1: Earnings up to and including LEL. If answer is negative no NICs due and no recording required. If answer is zero
        // or positive record result and proceed to Step 2.

        decimal threshold = thresholds.GetThreshold(LEL);
        decimal resultOfStep1 = nicableEarningsYearToDate - threshold;

        if (resultOfStep1 < 0.0m)
        {
            result = NiCalculationResult.NoRecordingRequired;

            return;
        }

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

        for (int stepIndex = 0; stepIndex < _directorThresholdPairs.Length; stepIndex++)
        {
            decimal upperThreshold = thresholds.GetThreshold(_directorThresholdPairs[stepIndex].Item2) * (proRataFactor ?? 1.0m);
            decimal earningsAboveUpperThreshold = Math.Max(0.0m, nicableEarningsYearToDate - upperThreshold);

            decimal resultOfStep = previousEarningsAboveThreshold - earningsAboveUpperThreshold;

            if (resultOfStep == 0.0m)
                break;

            results[stepIndex + 1] = decimal.Round(resultOfStep, 4, MidpointRounding.ToZero);
            previousEarningsAboveThreshold = earningsAboveUpperThreshold;
        }

        // Step 6: Earnings above UEL. If answer is zero or negative no earnings above UEL.

        results[CalculationStepCount - 1] = Math.Max(0.0m, nicableEarningsYearToDate - thresholds.GetThreshold(UEL));

        // Step 7: Calculate employee NICs
        // Step 8: Calculate employer NICs

        if (!_niRateEntriesForDirectors.TryGetValue(niCategory, out var rates))
            throw new InvalidOperationException($"Unable to obtain director's National Insurance rates for category {niCategory}");

        GetNiEarningsBreakdownFromCalculationResults(results, out var earningsBreakdown);

        result = new NiCalculationResult(
            niCategory,
            nicableEarningsYearToDate,
            rates,
            thresholds,
            earningsBreakdown,
            CalculateEmployeesNi(rates, results) - employeesNiPaidYearToDate,
            CalculateEmployersNi(rates, results) - employersNiPaidYearToDate);
    }

    private void GetProRataAnnualThresholds(decimal proRataFactor, out INiThresholdSet thresholds)
    {
        thresholds = new NiThresholdSet(_niAnnualThresholds, proRataFactor);
    }

    private static decimal CalculateEmployeesNi(INiCategoryRatesEntry rates, decimal[] calculationStepResults)
    {
        if (calculationStepResults.Length != CalculationStepCount)
            throw new InvalidOperationException($"Unexpected number of calculation step results; should be {NiCalculator.CalculationStepCount}, was {calculationStepResults.Length}");

        return ((calculationStepResults[Step4] + calculationStepResults[Step5]) * rates.EmployeeRatePTToUEL)
            .NiRound() +
            (calculationStepResults[Step6] * rates.EmployeeRateAboveUEL)
            .NiRound();
    }

    private static decimal CalculateEmployersNi(INiCategoryRatesEntry rates, decimal[] calculationStepResults)
    {
        if (calculationStepResults.Length != CalculationStepCount)
            throw new InvalidOperationException($"Unexpected number of calculation step results; should be {NiCalculator.CalculationStepCount}, was {calculationStepResults.Length}");

        // From HMRC documentation: Step 3 plus Step 4 multiplied by employer’s band D % rate(don’t round) = a
        // PLUS
        // Step 5 multiplied by employer’s band E % rate (don’t round) = b
        // a plus b = (round)
        // PLUS
        // Step 6 multiplied by employer’s band F % rate(round).
        return (((calculationStepResults[Step3] + calculationStepResults[Step4]) * rates.EmployerRateSTtoFUST) +
            (calculationStepResults[Step5] * rates.EmployerRateFUSTtoUEL))
            .NiRound() +
            (calculationStepResults[Step6] * rates.EmployerRateAboveUEL)
            .NiRound();
    }

    private static void GetNiEarningsBreakdownFromCalculationResults(decimal[] calculationStepResults, out NiEarningsBreakdown breakdown)
    {
        if (calculationStepResults.Length != NiCalculator.CalculationStepCount)
            throw new InvalidOperationException($"Unexpected number of calculation step results; should be {NiCalculator.CalculationStepCount}, was {calculationStepResults.Length}");

        breakdown = new NiEarningsBreakdown()
        {
            EarningsUpToAndIncludingLEL = calculationStepResults[Step1],
            EarningsAboveLELUpToAndIncludingST = calculationStepResults[Step2],
            EarningsAboveSTUpToAndIncludingPT = calculationStepResults[Step3],
            EarningsAbovePTUpToAndIncludingFUST = calculationStepResults[Step4],
            EarningsAboveFUSTUpToAndIncludingUEL = calculationStepResults[Step5],
            EarningsAboveUEL = calculationStepResults[Step6],
            EarningsAboveSTUpToAndIncludingUEL = calculationStepResults[Step3] + calculationStepResults[Step4] + calculationStepResults[Step5]
        };
    }
}