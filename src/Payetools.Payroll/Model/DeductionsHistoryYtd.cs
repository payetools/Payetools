// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an employee's deductions history for the tax year to date.
/// </summary>
/// <remarks>Note that this type is mutable (via the <see cref="Apply(IEnumerable{IDeductionEntry})"/> method;
/// however its one property is not mutable, since it returns an immutable dictionary which contains a type that is itself
/// immutable.</remarks>
public class DeductionsHistoryYtd : IDeductionsHistoryYtd
{
    private readonly Dictionary<IDeductionDetails, IDeductionEntry> _deductions;

    /// <summary>
    /// Gets a dictionary of deductions year to date keyed on the deduction details.
    /// </summary>
    public ImmutableDictionary<IDeductionDetails, IDeductionEntry> Deductions => _deductions.ToImmutableDictionary();

    /// <summary>
    /// Initialises a new empty <see cref="DeductionsHistoryYtd"/>.
    /// </summary>
    public DeductionsHistoryYtd()
    {
        _deductions = new Dictionary<IDeductionDetails, IDeductionEntry>();
    }

    /// <summary>
    /// Adds the supplied deductions to the current instance.
    /// </summary>
    /// <param name="deductions">Deductions to apply.</param>
    /// <returns>A reference to the current history, as a convenience.</returns>
    public IDeductionsHistoryYtd Apply(IEnumerable<IDeductionEntry> deductions)
    {
        foreach (var entry in deductions)
        {
            if (_deductions.TryGetValue(entry.DeductionClassification, out var existingEntry))
                _deductions[entry.DeductionClassification] = existingEntry.Add(entry);
            else
                _deductions.Add(entry.DeductionClassification, entry);
        }

        return this;
    }
}