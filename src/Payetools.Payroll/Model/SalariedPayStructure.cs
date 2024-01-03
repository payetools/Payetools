// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the pay structure for salaried (i.e., annually paid) employees.
/// </summary>
public record SalariedPayStructure : IEmployeePayStructure
{
    /// <inheritdoc/>>
    public Guid Id { get; init; }

    /// <inheritdoc/>>
    public decimal PayRate { get; init; }

    /// <inheritdoc/>>
    public PayRateType PayRateType => PayRateType.Salaried;

    /// <inheritdoc/>>
    public IEarningsDetails PayComponent { get; init; } = default!;
}
