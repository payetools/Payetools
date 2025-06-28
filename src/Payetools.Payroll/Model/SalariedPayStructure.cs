// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the pay structure for salaried (i.e., annually paid) employees.
/// </summary>
public class SalariedPayStructure : IEmployeePayStructure
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