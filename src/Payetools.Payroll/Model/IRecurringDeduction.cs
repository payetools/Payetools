﻿// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents a recurring deduction for an employee.
/// </summary>
public interface IRecurringDeduction : IApplicableFromTill, IPayrollAmount
{
    /// <summary>
    /// Gets the deduction type for this recurring deduction.
    /// </summary>
    IDeductionDetails DeductionType { get; }
}
