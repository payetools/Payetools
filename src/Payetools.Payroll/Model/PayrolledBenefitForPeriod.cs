// Copyright (c) 2023 Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents a payrolled benefit as applicable to one payroll period.
/// </summary>
public record PayrolledBenefitForPeriod : IPayrolledBenefitForPeriod
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
