// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
