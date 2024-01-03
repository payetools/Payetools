// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Concurrent;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents the output of a given payrun.
/// </summary>
public interface IPayrunResult : IEmployerInfoProvider
{
    /// <summary>
    /// Gets the pay date for this payrun.
    /// </summary>
    PayDate PayDate { get; }

    /// <summary>
    /// Gets the list of employee payrun entries.
    /// </summary>
    List<IEmployeePayrunResult> EmployeePayrunEntries { get; }
}
