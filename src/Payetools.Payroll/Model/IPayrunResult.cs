// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
