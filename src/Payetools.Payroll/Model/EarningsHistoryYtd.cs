// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an employee's earnings history for the tax year to date.
/// </summary>
public record EarningsHistoryYtd : IEarningsHistoryYtd
{
    /// <summary>
    /// Gets the list of pay components for this employee for a given payrun.  May be empty but usually not.
    /// </summary>
    public ImmutableList<IEarningsEntry> Earnings { get; }

    /// <summary>
    /// Initialises a new empoty <see cref="EarningsHistoryYtd"/>.
    /// </summary>
    public EarningsHistoryYtd()
    {
        Earnings = ImmutableList<IEarningsEntry>.Empty;
    }
}
