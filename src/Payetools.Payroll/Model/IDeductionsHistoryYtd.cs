// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents an employee's deductions history for the tax year to date.
/// </summary>
public interface IDeductionsHistoryYtd
{
    /// <summary>
    /// Gets a dictionary of deductions year to date keyed on the deduction details.
    /// </summary>
    ImmutableDictionary<IDeductionDetails, IDeductionEntry> Deductions { get; }

    /// <summary>
    /// Adds the supplied deductions to the current instance.
    /// </summary>
    /// <param name="deductions">Deductions to apply.</param>
    /// <returns>A reference to the current history, as a convenience.</returns>
    IDeductionsHistoryYtd Apply(IEnumerable<IDeductionEntry> deductions);
}