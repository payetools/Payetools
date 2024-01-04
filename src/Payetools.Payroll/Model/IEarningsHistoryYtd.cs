// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents an employee's earnings history for the tax year to date.
/// </summary>
public interface IEarningsHistoryYtd
{
    /// <summary>
    /// Gets the list of pay components for this employee for a given payrun.  May be empty but usually not.
    /// </summary>
    ImmutableList<IEarningsEntry> Earnings { get; }
}
