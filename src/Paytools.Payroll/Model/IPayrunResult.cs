// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using System.Collections.Concurrent;

namespace Paytools.Payroll.Model;

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
