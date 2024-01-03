// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
