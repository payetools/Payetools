// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;

namespace Payetools.AttachmentOrders.Calculators;

/// <summary>
/// Interface for individual attachment order calculators (not to be confused with <see cref="IAttachmentOrdersCalculator"/>),
/// which is the interface that payroll processors call to apply a series of attachment orders.
/// </summary>
public interface IAttachmentOrderCalculator
{
    /// <summary>
    /// Calculates the attachment order deductions based on the provided earnings, deductions, etc.
    /// </summary>
    /// <param name="previousEntries">Collection of previous attachment order entries that have already
    /// been calculated for the employee in this pay run.</param>
    /// <param name="attachmentOrder">Attachment order to be applied during the calculation.</param>
    /// <param name="earnings">Collection of earnings entries representing the employee's income.</param>
    /// <param name="deductions">Collection of deduction entries to be subtracted from the earnings.</param>
    /// <param name="payPeriod">Pay period for which the calculation is being performed.</param>
    /// <param name="finalTaxDue">Total tax amount due, which will be factored into the calculation.</param>
    /// <param name="employeeNiContribution">Employee's National Insurance contribution, which will be factored
    /// into the calculation.</param>
    /// <param name="studentLoanDeductions">Employee's student and postgraduate loan repayments, if any.</param>
    /// <param name="employeePensionContribution">Employee pension contribution, if any.</param>
    /// <param name="attachmentOrderCalculationResult"><see cref="AttachmentOrderCalculationResultEntry"/>
    /// instance containing the results of this calculation.</param>
    void Calculate(
        List<AttachmentOrderCalculationResultEntry> previousEntries,
        IAttachmentOrder attachmentOrder,
        IEnumerable<IEarningsEntry> earnings,
        IEnumerable<IDeductionEntry> deductions,
        DateRange payPeriod,
        decimal finalTaxDue,
        decimal employeeNiContribution,
        decimal studentLoanDeductions,
        decimal employeePensionContribution,
        out AttachmentOrderCalculationResultEntry attachmentOrderCalculationResult);
}