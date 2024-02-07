// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents the various types of deduction that can be made from payroll.
/// </summary>
public interface IDeductionDetails
{
    /// <summary>
    /// Gets the full name of this type of deduction.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the units for this deduction type, if applicable.  Null if not applicable.
    /// </summary>
    PayRateUnits? Units { get; }

    /// <summary>
    /// Gets a value indicating whether this type of deduction reduces the gross pay figure used
    /// to calculate take-home .
    /// </summary>
    bool ReducesGrossPay { get; }

    /// <summary>
    /// Gets a value indicating whether this type of deduction is applied before or after tax.
    /// </summary>
    bool ReducesTaxablePay { get; }

    /// <summary>
    /// Gets a value indicating whether this type of deduction affects pay for National Insurance
    /// purposes.
    /// </summary>
    bool ReducesNicablePay { get; }

    /// <summary>
    /// Gets a value indicating whether this type of deduction affects pay for pension
    /// purposes.
    /// </summary>
    bool ReducesPensionablePay { get; }

    /// <summary>
    /// Gets a value indicating whether this type of deduction is made as part of a salary exchange
    /// (aka salary sacrifice) arrangement.  Note that when this flag is set, <see cref="ReducesGrossPay"/>,
    /// <see cref="ReducesTaxablePay"/> and <see cref="ReducesNicablePay"/> will also normally be
    /// set to true.
    /// </summary>
    /// <remarks>This property is primarily included to assist when it is time to show the deduction on the
    /// payslip, enabling all salary exchange deductions including pensions to be grouped together.</remarks>
    bool IsUnderSalaryExchangeArrangement { get; }
}