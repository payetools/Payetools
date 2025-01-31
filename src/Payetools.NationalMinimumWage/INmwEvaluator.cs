// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.NationalMinimumWage;

/// <summary>
/// Interface for types that provide National Minimum/Living Wage evaluations.
/// </summary>
public interface INmwEvaluator
{
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
    /// the rate that applies to each worker depends on their age at the start of the pay reference period.</remarks>
    NmwEvaluationResult Evaluate(
        DateRange payPeriod,
        DateOnly dateOfBirth,
        decimal grossPay,
        decimal hoursWorkedForPay,
        bool isApprentice = false,
        decimal? yearsAsApprentice = null);

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
    decimal GetNmwHourlyRateForEmployee(
        int ageAtStartOfPeriod,
        bool isApprentice = false,
        decimal? yearsAsApprentice = null);
}