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

using Paytools.Common.Extensions;
using Paytools.Common.Model;
using Paytools.NationalMinimumWage.ReferenceData;
using System.Text;

namespace Paytools.NationalMinimumWage;

/// <summary>
/// Represents a National Minimum/Living Wage evaluator that implements <see cref="INmwEvaluator"/>.
/// </summary>
public class NmwEvaluator : INmwEvaluator
{
    private readonly INmwLevelSet _nmwLevels;

    /// <summary>
    /// Initialises a new instance of <see cref="NmwEvaluator"/> using the supplied set of levels.
    /// </summary>
    /// <param name="nmwLevels">NMW/NLW levels to use.</param>
    public NmwEvaluator(INmwLevelSet nmwLevels)
    {
        _nmwLevels = nmwLevels;
    }

    /// <summary>
    /// Evauates whether an employee's pay is compliant with the NMW/NLW regulations by considering their pay, hours and date of
    /// birth.  Note that apprentices have special treatment.
    /// </summary>
    /// <param name="payPeriod">Applicable pay period.</param>
    /// <param name="dateOfBirth">Employee's date of birth.</param>
    /// <param name="grossPay">Gross pay to be used for the evaluation.  This pay should correspond to the number of hours worked
    /// given in the subequent parameter.</param>
    /// <param name="hoursWorkedForPay">Hours worked that corresponds to the gross pay figure supplied.</param>
    /// <param name="isApprentice">True if the employee is an apprentice; false otherwise.  Optional, defaults to false.</param>
    /// <param name="yearsAsApprentice">Number of years an apprentice has served in their apprenticeship.  May be a figure less
    /// than one.  Optional, defaults to null; not required if the employee is not an apprentice.</param>
    /// <returns>An instance of <see cref="NmwEvaluationResult"/> that indicates whether the pay is compliant with the NMW/NLW
    /// regulations.</returns>
    /// <remarks>As per <see href="https://www.gov.uk/hmrc-internal-manuals/national-minimum-wage-manual/nmwm03010"/>,
    /// the rate that applies to each worker depends on their age at teh start of the pay reference period.</remarks>
    public NmwEvaluationResult Evaluate(
        PayReferencePeriod payPeriod,
        DateOnly dateOfBirth,
        decimal grossPay,
        decimal hoursWorkedForPay,
        bool isApprentice = false,
        decimal? yearsAsApprentice = null)
    {
        StringBuilder commentary = new StringBuilder();
        var age = dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod);

        commentary.Append($"Age at start of pay period = {age}. ");

        if (isApprentice && (age < 19 || yearsAsApprentice < 1.0m))
        {
            commentary.Append("Treated as apprentice ");
            commentary.Append(age < 19 ? "under 19. " : "19 or over but in the first year of their apprenticeship. ");
        }

        var rateApplicable = GetNmwHourlyRateForEmployee(age, isApprentice, yearsAsApprentice);

        var hourlyRate = grossPay / hoursWorkedForPay;
        bool isCompliant = hourlyRate >= rateApplicable;

        commentary.Append(isCompliant ?
            $"Pay is compliant as gross pay per hour of {hourlyRate:F4} is greater than or equal to minimum NMW/NLW rate {rateApplicable}" :
            $"Pay is non-compliant as gross pay per hour of {hourlyRate:F4} is less than minimum NMW/NLW rate {rateApplicable}");

        return new NmwEvaluationResult(isCompliant, rateApplicable, age, commentary.ToString());
    }

    /// <summary>
    /// Gets the expected hourly rate of pay for an employee that is paid is the National Minimum or National Living Wage.
    /// </summary>
    /// <param name="ageAtStartOfPeriod">Age at the start of the applicable pay period.</param>
    /// <param name="isApprentice">True if the employee is an apprentice; false otherwise.  Optional, defaults to false.</param>
    /// <param name="yearsAsApprentice">Number of years an apprentice has served in their apprenticeship.  May be a figure less
    /// than one.  Optional, defaults to null; not required if the employee is not an apprentice.</param>
    /// <returns>Appropriate hourly rate of pay.</returns>
    /// <remarks>As per <see href="https://www.gov.uk/hmrc-internal-manuals/national-minimum-wage-manual/nmwm03010"/>,
    /// the rate that applies to each worker depends on their age at the start of the pay reference period.</remarks>
    public decimal GetNmwHourlyRateForEmployee(
        int ageAtStartOfPeriod,
        bool isApprentice = false,
        decimal? yearsAsApprentice = null)
    {
        // Apprentices are entitled to the apprentice rate if they’re either:
        //   - aged under 19
        //   - aged 19 or over and in the first year of their apprenticeship
        if (isApprentice && (ageAtStartOfPeriod < 19 || yearsAsApprentice < 1.0m))
            return _nmwLevels.ApprenticeLevel;

        return ageAtStartOfPeriod switch
        {
            < 18 => _nmwLevels.Under18Level,
            (>= 18) and (<= 20) => _nmwLevels.Age18To20Level,
            (>= 21) and (<= 22) => _nmwLevels.Age21To22Level,
            >= 23 => _nmwLevels.Age23AndAboveLevel
        };
    }
}