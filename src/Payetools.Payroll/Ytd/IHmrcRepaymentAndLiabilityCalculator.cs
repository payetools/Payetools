// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Model;

namespace Payetools.Payroll.Ytd;

/// <summary>
/// Interface that represents aggregators that summarise year-to-date pay run data.
/// </summary>
public interface IHmrcRepaymentAndLiabilityCalculator
{
    /// <summary>
    /// Summarises the supplied employee histories into a single <see cref="IEmployerHistoryEntry"/>.
    /// </summary>
    /// <param name="employeeHistories">Employee year-to-date histories.</param>
    /// <param name="summary">Summary.</param>
    void Summarise(in IEnumerable<IEmployeePayrollHistoryYtd> employeeHistories, out IEmployerHistoryEntry summary);
}