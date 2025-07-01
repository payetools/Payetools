// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.AttachmentOrders.ReferenceData;
using Payetools.Common.Model;

namespace Payetools.AttachmentOrders.Calculators;

/// <summary>
/// Calculator for attachment orders that use a table-based percentage of attachable earnings plus
/// a fixed or minimum amount.
/// </summary>
/// <remarks>This calculator is intended for Scottish Arrestment Orders.</remarks>
public class TableBasedPercentagePlusFixedAmountCalculator : IAttachmentOrderCalculator
{
    private readonly AttachmentOrderRateTable _rateTable;

    /// <summary>
    /// Initializes a new instance of the <see cref="TableBasedPercentageCalculator"/> class.
    /// </summary>
    /// <param name="rateTable">Rate table.</param>
    public TableBasedPercentagePlusFixedAmountCalculator(IEnumerable<AttachmentOrderRateTableEntry> rateTable)
    {
        _rateTable = new AttachmentOrderRateTable(rateTable);
    }

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
    public void Calculate(
        List<AttachmentOrderCalculationResultEntry> previousEntries,
        IAttachmentOrder attachmentOrder,
        IEnumerable<IEarningsEntry> earnings,
        IEnumerable<IDeductionEntry> deductions,
        DateRange payPeriod,
        decimal finalTaxDue,
        decimal employeeNiContribution,
        decimal studentLoanDeductions,
        decimal employeePensionContribution,
        out AttachmentOrderCalculationResultEntry attachmentOrderCalculationResult)
    {
        // TODO: Put methods for attachable earnings, etc in a common place
        var attachableEarnings = earnings.Sum(e => e.FixedAmount ?? 0);

        var rateInfo = _rateTable.GetApplicableDeductionInfo(
            attachableEarnings,
            attachmentOrder.EmployeePayFrequency,
            attachmentOrder.RateType ?? AttachmentOrderRateType.Standard);

        attachmentOrderCalculationResult = new AttachmentOrderCalculationResultEntry
        {
            AttachmentOrder = attachmentOrder,
            Deduction = decimal.Round(attachableEarnings * rateInfo.Rate, 2, MidpointRounding.AwayFromZero)
        };
    }
}