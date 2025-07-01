// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Calculators;
using Payetools.AttachmentOrders.Model;
using Payetools.AttachmentOrders.ReferenceData;
using Payetools.Common.Model;

namespace Payetools.AttachmentOrders;

/// <summary>
/// Calculator that provide calculation of attachment of earnings orders.
/// </summary>
public class AttachmentOrdersCalculator : IAttachmentOrdersCalculator
{
    private readonly Dictionary<AttachmentOrderReferenceDataEntry.LookupKey, AttachmentOrderReferenceDataEntry> _attachmentOrderReferenceDataEntries;

    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentOrdersCalculator"/> class.
    /// </summary>
    /// <param name="attachmentOrderReferenceDataEntries">Dictionary that contains all the
    /// current reference data entries for the tax year.</param>
    public AttachmentOrdersCalculator(Dictionary<AttachmentOrderReferenceDataEntry.LookupKey, AttachmentOrderReferenceDataEntry> attachmentOrderReferenceDataEntries)
    {
        _attachmentOrderReferenceDataEntries = attachmentOrderReferenceDataEntries;
    }

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
        List<AttachmentOrderCalculationResultEntry> resultEntries = [];

        foreach (var attachmentOrder in attachmentOrders)
        {
            var calculator = GetCalculator(attachmentOrder);

            calculator.Calculate(
                resultEntries,
                attachmentOrder,
                earnings,
                deductions,
                payPeriod,
                finalTaxDue,
                employeeNiContribution,
                studentLoanDeductions,
                employeePensionContribution,
                out var result);

            resultEntries.Add(result!);
        }

        attachmentOrdersCalculationResult = new AttachmentOrderCalculationResult(
            resultEntries.Sum(r => r.Deduction),
            resultEntries.AsReadOnly());
    }

    private IAttachmentOrderCalculator GetCalculator(IAttachmentOrder attachmentOrder)
    {
        // TODO: Worry about Scottish attachment orders later; they don't have an issue date.
        var lookupKey = _attachmentOrderReferenceDataEntries.Keys.FirstOrDefault(k =>
            attachmentOrder.CalculationType == k.CalculationType &&
            attachmentOrder.IssueDate >= k.ApplicableDateRange.Start &&
            attachmentOrder.IssueDate <= k.ApplicableDateRange.End) ??
            throw new ArgumentException($"Attachment order with calculation type {attachmentOrder.CalculationType} and issue date {attachmentOrder.IssueDate} does not have a matching reference data entry.", nameof(attachmentOrder));

        var referenceDataEntry = _attachmentOrderReferenceDataEntries[lookupKey];

        return attachmentOrder.CalculationType switch
        {
            AttachmentOrderCalculationType.TableBasedPercentageOfEarnings => new TableBasedPercentageCalculator(referenceDataEntry.RateTable),
            _ => throw new NotSupportedException($"Unsupported attachment order calculation type: {attachmentOrder.CalculationType}")
        };
    }
}