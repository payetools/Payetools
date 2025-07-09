// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Enumeration that represents the different types of deductions that can be made from an employee's pay.
/// </summary>
public enum DeductionType
{
    /// <summary>Not defined.</summary>
    Undefined,

    /// <summary>Adjustment to pay, for example, repayment of previous earnings in error.</summary>
    CorrectionAdjustment,

    /// <summary>Repayment of employee loan.</summary>
    LoanRepayment,

    /// <summary>Attachment order payment.</summary>
    AttachmentOrder
}