// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an employee's earnings history for the tax year to date.
/// </summary>
/// <remarks>Note that this type is mutable (via the <see cref="Apply(IEnumerable{IEarningsEntry})"/> method;
/// however its one property is not mutable, since it returns an immutable dictionary which contains a type that is itself
/// immutable.</remarks>
public class EarningsHistoryYtd : IEarningsHistoryYtd
{
    private readonly Dictionary<IEarningsDetails, IEarningsEntry> _entries;

    /// <summary>
    /// Gets an array of earnings for the year to date.
    /// </summary>
    public ImmutableDictionary<IEarningsDetails, IEarningsEntry> Earnings => _entries.ToImmutableDictionary();

    /// <summary>
    /// Initialises a new empty <see cref="EarningsHistoryYtd"/>.
    /// </summary>
    public EarningsHistoryYtd()
    {
        _entries = new Dictionary<IEarningsDetails, IEarningsEntry>();
    }

    /// <summary>
    /// Applies the supplied earnings to the current instance.
    /// </summary>
    /// <param name="earnings">IEnumerable of earnings to apply.</param>
    /// <returns>A reference to the current history, as a convenience.</returns>
    public IEarningsHistoryYtd Apply(IEnumerable<IEarningsEntry> earnings)
    {
        foreach (var entry in earnings)
        {
            if (EmployeePayrollHistoryYtd.IsReclaimableStatutoryPayment(entry.EarningsDetails.PaymentType))
                continue;

            if (_entries.TryGetValue(entry.EarningsDetails, out var existingEntry))
                _entries[entry.EarningsDetails] = existingEntry.Add(entry);
            else
                _entries.Add(entry.EarningsDetails, entry);
        }

        return this;
    }
}
