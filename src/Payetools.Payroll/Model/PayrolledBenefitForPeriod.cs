// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents a payrolled benefit as applicable to one payroll period.
/// </summary>
public class PayrolledBenefitForPeriod : IPayrolledBenefitForPeriod
{
    /// <summary>
    /// Gets the amount of benefit to apply for the period.
    /// </summary>
    public decimal AmountForPeriod { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="PayrolledBenefitForPeriod"/>.
    /// </summary>
    /// <param name="amountForPeriod">Amount of the benefit for the period.</param>
    public PayrolledBenefitForPeriod(decimal amountForPeriod)
    {
        AmountForPeriod = amountForPeriod;
    }
}