// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;

namespace Payetools.AttachmentOrders;

/// <summary>
/// Calculator that provide calculation of attachment of earnings orders.
/// </summary>
public class AttachmentOrdersCalculator : IAttachmentOrdersCalculator
{
    /// <summary>
    /// Calculates the attachment order deductions based on the provided attachment orders, earnings,
    /// deductions, student loans, employee pension contributions, tax due, and National Insurance contribution.
    /// </summary>
    /// <param name="attachmentOrders">Collection of attachment orders to be applied during the calculation.</param>
    /// <param name="earnings">Collection of earnings entries representing the employee's income.</param>
    /// <param name="deductions">Collection of deduction entries to be subtracted from the earnings.</param>
    /// <param name="payPeriod">Pay period for which the calculation is being performed.</param>
    /// <param name="finalTaxDue">Total tax amount due, which will be factored into the calculation.</param>
    /// <param name="employeeNiContribution">Employee's National Insurance contribution, which will be factored into the calculation.</param>
    /// <param name="studentLoanDeductions">Employee's student and postgraduate loan repayments, if any.</param>
    /// <param name="employeePensionContribution">Employee pension contribution, if any.</param>
    /// <param name="attachmentOrdersCalculationResult">Output parameter that contains the result of the attachment order calculation.
    /// If no calculation is appropriate, this will be <see langword="null"/>.</param>
    public void Calculate(
        IEnumerable<IAttachmentOrder> attachmentOrders,
        IEnumerable<IEarningsEntry> earnings,
        IEnumerable<IDeductionEntry> deductions,
        DateRange payPeriod,
        decimal finalTaxDue,
        decimal employeeNiContribution,
        decimal studentLoanDeductions,
        decimal employeePensionContribution,
        out IAttachmentOrderCalculationResult? attachmentOrdersCalculationResult)
    {
        attachmentOrdersCalculationResult = null;
    }
}