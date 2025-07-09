// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Interface that represents a pay component (e.g., salary, bonus, sick pay, etc.).
/// </summary>
/// <remarks>As this type is used as the key to one or more dictionaries in the library,
/// it is recommended to override <see cref="object.GetHashCode"/> in any implementations.</remarks>
public interface IEarningsDetails
{
    /// <summary>
    /// Gets the full name of this pay component.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the type of this payment.
    /// </summary>
    EarningsType PaymentType { get; }

    /// <summary>
    /// Gets the units for this pay component, if applicable.  Null if not applicable.
    /// </summary>
    PayRateUnits? Units { get; }

    /// <summary>
    /// Gets a value indicating whether this pay component is subject to tax.
    /// </summary>
    bool IsSubjectToTax { get; }

    /// <summary>
    /// Gets a value indicating whether this pay component is subject to National Insurance.
    /// </summary>
    bool IsSubjectToNi { get; }

    /// <summary>
    /// Gets a value indicating whether this pay component should be included in the employee's
    /// pensionable salary.
    /// </summary>
    bool IsPensionable { get; }

    /// <summary>
    /// Gets a value indicating whether this pay component refers to a net amount that should be
    /// "grossed up", ensuring the employee receives the net amount in their take-home pay.
    /// </summary>
    bool IsNetToGross { get; }

    /// <summary>
    /// Gets a value indicating whether this pay component should be treated as overtime for the
    /// purposes of average overtime calculations.
    /// </summary>
    bool IsTreatedAsOvertime { get; }
}