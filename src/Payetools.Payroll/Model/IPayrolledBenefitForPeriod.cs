// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents the effect of a payrolled on a given pay reference period.
/// </summary>
public interface IPayrolledBenefitForPeriod : IPayrolledBenefit
{
    /// <summary>
    /// Gets the amount of benefit to apply for the period.
    /// </summary>
    decimal AmountForPeriod { get; }
}
