// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Tests;

/// <summary>
/// Represents the various types of deduction that can be made from payroll.
/// </summary>
public class GenericDeduction : IDeductionDetails
{
    /// <summary>
    /// Gets or sets the short name for this type of deduction.
    /// </summary>
    public string ShortName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the full name of this type of deduction.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets the type of this deduction.
    /// </summary>
    public DeductionType DeductionType { get; }

    /// <summary>
    /// Gets or sets the units for this deduction type, if applicable.  Null if not applicable.
    /// </summary>
    public PayRateUnits? Units { get; set; }

    /// <summary>
    /// Gets a value indicating whether this type of deduction reduces the gross pay figure used
    /// to calculate take-home .
    /// </summary>
    public bool ReducesGrossPay { get; }

    /// <summary>
    /// Gets or sets a value indicating whether this type of deduction is applied before or after tax.
    /// </summary>
    public bool ReducesTaxablePay { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this type of deduction affects pay for National Insurance
    /// purposes.
    /// </summary>
    public bool ReducesNicablePay { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this type of deduction affects pay for pension
    /// purposes.
    /// </summary>
    public bool ReducesPensionablePay { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this type of deduction is made as part of a salary exchange
    /// (aka salary sacrifice) arrangement.  Note that when this flag is set, <see cref="ReducesGrossPay"/>,
    /// <see cref="ReducesTaxablePay"/> and <see cref="ReducesNicablePay"/> will also normally be
    /// set to true.
    /// </summary>
    /// <remarks>This property is primarily included to assist when it is time to show the deduction on the
    /// payslip, enabling all salary exchange deductions including pensions to be grouped together.</remarks>
    public bool IsUnderSalaryExchangeArrangement { get; set; }
}