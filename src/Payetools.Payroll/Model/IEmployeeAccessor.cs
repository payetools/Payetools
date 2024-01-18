// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Provides a mechanism to pass references to employees around, without having to pass the full set of
/// employee details.  Also allows for more lazy access to employee data that may change during the lifecycle
/// of a pay run.
/// </summary>
public interface IEmployeeAccessor
{
    /// <summary>
    /// Gets the employee that this accessor pertains to.
    /// </summary>
    /// <returns>An implementation of <see cref="IEmployee"/> containing the employee's data.</returns>
    IEmployee GetEmployee();
}
