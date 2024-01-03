// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents a generic pay component (e.g., salary, bonus, sick pay, etc.).
/// </summary>
public class GenericEarnings : IEarningsDetails
{
    /// <summary>
    /// Gets or sets the unique ID for this pay component.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the short name for this pay component.
    /// </summary>
    public string ShortName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the full name of this pay component.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the units for this pay component, if applicable.  Null if not applicable.
    /// </summary>
    public PayRateUnits? Units { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pay component is subject to tax.
    /// </summary>
    public bool IsSubjectToTax { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pay component is subject to National Insurance.
    /// </summary>
    public bool IsSubjectToNi { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pay component should be included in the employee's
    /// pensionable salary.
    /// </summary>
    public bool IsPensionable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pay component refers to a net amount that should be
    /// "grossed up", ensuring the employee receives the net amount in their take-home pay.
    /// </summary>
    public bool IsNetToGross { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pay component should be treated as overtime for the
    /// purposes of average overtime calculations.
    /// </summary>
    public bool IsTreatedAsOvertime { get; set; }
}
