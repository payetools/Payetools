// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Model;

namespace Payetools.Payroll.Ytd;

/// <summary>
/// Aggregator that provides summarisation across multiple pay runs.
/// </summary>
public class PayRunHistoryAggregator : IHmrcRepaymentAndLiabilityCalculator
{
    /// <summary>
    /// Summarises the supplied employee histories into a s single <see cref="IEmployerHistoryEntry"/>.
    /// </summary>
    /// <param name="employeeHistories">Employee year-to-date histories.</param>
    /// <param name="summary">Summarised information as an instance of <see cref="IEmployerHistoryEntry"/>.</param>
    public void Summarise(in IEnumerable<IEmployeePayrollHistoryYtd> employeeHistories, out IEmployerHistoryEntry summary)
    {
        throw new NotImplementedException();
    }
}
