// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the pay structure for hourly paid employees.
/// </summary>
public record HourlyPaidPayStructure : IEmployeePayStructure
{
    /// <inheritdoc/>>
    public Guid Id { get; init; }

    /// <inheritdoc/>>
    public decimal PayRate { get; init; }

    /// <inheritdoc/>>
    public PayRateType PayRateType => PayRateType.HourlyPaid;

    /// <inheritdoc/>>
    public IEarningsDetails PayComponent { get; init; } = default!;
}