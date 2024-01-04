// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents a recurring earnings for an employee.
/// </summary>
public interface IRecurringEarnings : IApplicableFromTill, IPayrollAmount
{
    /// <summary>
    /// Gets the pay component for this recurring earnings.
    /// </summary>
    IEarningsDetails PayComponent { get; }
}
