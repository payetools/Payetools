// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Extensions;
using Payetools.Common.Model;
using Payetools.NationalMinimumWage;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an hourly paid pay structure that tracks National Minimum/Living Wage
/// levels.
/// </summary>
public class NmwHourlyPaidPayStructure : IEmployeePayStructure
{
    /// <inheritdoc/>>
    public Guid Id { get; init; }

    /// <inheritdoc/>>
    public decimal PayRate { get; private set; }

    /// <inheritdoc/>>
    public PayRateType PayRateType => PayRateType.HourlyPaid;

    /// <inheritdoc/>>
    public IEarningsDetails PayComponent { get; init; } = default!;

    /// <summary>
    /// Updates the <see cref="PayRate"/> based on the applicable NMW/NLW wage rate for the employee
    /// using their age at the start of the pay period.
    /// </summary>
    /// <param name="nmwEvaluator">Instance of <see cref="INmwEvaluator"/> used to obtain the appropriate rate.</param>
    /// <param name="payPeriod">Pay period pertaining.</param>
    /// <param name="dateOfBirth">Employee's date of birth.</param>
    public void UpdateNmw(INmwEvaluator nmwEvaluator, DateRange payPeriod, DateOnly dateOfBirth)
    {
        var age = dateOfBirth.AgeAt(payPeriod.Start);

        PayRate = nmwEvaluator.GetNmwHourlyRateForEmployee(age);
    }
}