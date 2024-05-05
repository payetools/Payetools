// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Enumeration that represents the different types of payments that an employee can receive.
/// </summary>
public enum PaymentType
{
    /// <summary>Not defined.</summary>
    Undefined,

    /// <summary>General earnings (wages/salary, bonus, commission, etc.).</summary>
    GeneralEarnings,

    /// <summary>Expenses claim. Not usually subject to tax or NI.</summary>
    Expenses,

    /// <summary>Statutory Sick Pay.</summary>
    StatutorySickPay,

    /// <summary>Statutory Maternity Pay.</summary>
    StatutoryMaternityPay,

    /// <summary>Statutory Paternity Pay.</summary>
    StatutoryPaternityPay,

    /// <summary>Statutory Shared Parental Pay.</summary>
    StatutorySharedParentalPay,

    /// <summary>Statutory Adoption Pay.</summary>
    StatutoryAdoptionPay,

    /// <summary>Statutory Parental Bereavement Pay.</summary>
    StatutoryParentalBereavementPay,

    /// <summary>Adjustment to pay, for example, refund of previous deduction.</summary>
    Adjustment
}
