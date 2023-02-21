﻿// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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

namespace Paytools.NationalInsurance;

using Paytools.NationalInsurance.Extensions;
using Paytools.NationalInsurance.ReferenceData;
using System.Collections.ObjectModel;
using static NiThresholdType;

/// <summary>
/// Represents a National Insurance calculator that implements <see cref="INiCalculator"/>.
/// </summary>
public class NiCalculator : INiCalculator
{
    private const int Step3 = 2;
    private const int Step4 = 3;
    private const int Step5 = 4;
    private const int Step6 = 5;

    private readonly ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> _niRateEntries;
    private readonly NiPeriodThresholdSet _niPeriodThresholds;

    private static readonly (NiThresholdType, NiThresholdType)[] _thresholdPairs = { (LEL, ST), (ST, PT), (PT, FUST), (FUST, UEL) };

    internal const int CalculationStepCount = 6;

    /// <summary>
    /// Initialises a new <see cref="NiCalculator"/> with the supplied thresholds and rates for the period.
    /// </summary>
    /// <param name="niPeriodThresholds">NI threshold set for the tax period applicable to this NI calculator.</param>
    /// <param name="niRateEntries">NI rates for the tax period applicable to this NI calculator.</param>
    public NiCalculator(NiPeriodThresholdSet niPeriodThresholds, ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> niRateEntries)
    {
        _niPeriodThresholds = niPeriodThresholds;
        _niRateEntries = niRateEntries;
    }

    /// <summary>
    /// Calculates the National Insurance contributions required for an employee for a given pay period,
    /// based on their NI-able salary and their allocated National Insurance category letter.
    /// </summary>
    /// <param name="niCategory">National Insurance category.</param>
    /// <param name="nicableEarningsInPeriod">NI-able salary for the period.</param>
    /// <returns>NI contributions due via an instance of a type that implements <see cref="INiCalculationResult"/>.</returns>
    public INiCalculationResult Calculate(NiCategory niCategory, decimal nicableEarningsInPeriod)
    {
        decimal[] results = new decimal[CalculationStepCount];

        // Step 1: Earnings up to and including LEL. If answer is negative no NICs due and no recording required. If answer is zero
        // or positive record result and proceed to Step 2.

        decimal threshold = _niPeriodThresholds.GetThreshold(LEL);
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

        if (!_niRateEntries.TryGetValue(niCategory, out var rates))
            throw new InvalidOperationException($"Unable to obtain National Insurance rates for category {niCategory}");

        return new NiCalculationResult(
            GetNiEarningsBreakdownFromCalculationResults(results),
            CalculateEmployeesNi(rates, results),
            CalculateEmployersNi(rates, results));
    }

    /// <summary>
    /// Calculates the National Insurance contributions required for a company director for a given pay period,
    /// based on their NI-able salary and their allocated National Insurance category letter.
    /// </summary>
    /// <param name="niCategory">National Insurance category.</param>
    /// <param name="nicableEarningsInPeriod">NI-able salary for the period.</param>
    /// <returns>NI contributions due via an instance of a type that implements <see cref="INiCalculationResult"/>.</returns>
    public INiCalculationResult CalculateDirectors(NiCategory niCategory, decimal nicableEarningsInPeriod)
    {
        throw new NotImplementedException();
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