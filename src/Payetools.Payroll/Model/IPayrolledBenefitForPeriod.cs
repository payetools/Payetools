// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Employment.Model;

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
