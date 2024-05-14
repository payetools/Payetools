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
/// <remarks>Note that this type is mutable (via the <see cref="Apply(IEnumerable{IEarningsEntry})"/> method;
/// however its one property is not mutable, since it returns an immutable dictionary which contains a type that is itself
/// immutable.</remarks>
public interface IEarningsHistoryYtd
{
    /// <summary>
    /// Gets a dictionary of earnings year to date keyed on earnings details.
    /// </summary>
    ImmutableDictionary<IEarningsDetails, IEarningsEntry> Earnings { get; }

    /// <summary>
    /// Applies the supplied earnings to the current instance.
    /// </summary>
    /// <param name="earnings">IEnumerable of earnings to apply.</param>
    /// <returns>A reference to the current history, as a convenience.</returns>
    IEarningsHistoryYtd Apply(IEnumerable<IEarningsEntry> earnings);
}