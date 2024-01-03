// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using System.Collections.Immutable;

namespace Payetools.Employment.Model;

/// <summary>
/// Interface that represents an employee's deductions history for the tax year to date.
/// </summary>
public interface IDeductionHistoryYtd
{
    /// <summary>
    /// Gets the list of deductions for this employee for a given payrun.  May be empty but usually not.
    /// </summary>
    ImmutableList<IDeductionEntry> Deductions { get; }
}
