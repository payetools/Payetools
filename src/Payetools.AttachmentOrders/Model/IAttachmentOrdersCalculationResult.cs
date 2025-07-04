// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Interface that represents the results of an attachment of earnings calculation.
/// </summary>
public interface IAttachmentOrdersCalculationResult
{
    /// <summary>
    /// Gets the total deduction applicable as a result of any attachment of earnings orders.
    /// </summary>
    decimal TotalDeduction { get; }

    /// <summary>
    /// Gets a value indicating whether the results of the attachment orders calculation requires
    /// student loans deductions to be recalculated.
    /// </summary>
    bool StudentLoanRecalculationRequired { get; }

    /// <summary>
    /// Gets the collection of entries representing the results of attachment order calculations.
    /// </summary>
    IReadOnlyCollection<AttachmentOrderCalculationResultEntry> Entries { get; }
}