// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Entity that represents the results of an attachment of earnings calculation.
/// </summary>
public readonly struct AttachmentOrderCalculationResult : IAttachmentOrderCalculationResult
{
    /// <summary>
    /// Gets the total deduction applicable as a result of any attachment of earnings orders.
    /// </summary>
    public decimal TotalDeduction { get; }

    /// <summary>
    /// Gets a value indicating whether the results of the attachment orders calculation requires
    /// student loans deductions to be recalculated.
    /// </summary>
    public bool StudentLoanRecalculationRequired { get; }

    /// <summary>
    /// Gets the collection of entries representing the results of attachment order calculations.
    /// </summary>
    public IReadOnlyCollection<AttachmentOrderCalculationResultEntry> Entries { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentOrderCalculationResult"/> struct.
    /// </summary>
    /// <param name="totalDeduction">Total deduction to apply.</param>
    /// <param name="studentLoanRecalculationRequired">Indicates whether the results of this attachment
    /// orders calculation requires student loans deductions to be recalculated.</param>
    /// <param name="entries">Attachment order calculation entries.</param>
    public AttachmentOrderCalculationResult(
        decimal totalDeduction,
        bool studentLoanRecalculationRequired,
        IReadOnlyCollection<AttachmentOrderCalculationResultEntry> entries)
    {
        TotalDeduction = totalDeduction;
        StudentLoanRecalculationRequired = studentLoanRecalculationRequired;
        Entries = entries;
    }
}