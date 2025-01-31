// Copyright (c) 2023-2025, Payetools Foundation.
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
    /// <summary>Not specified.</summary>
    Unspecified,

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

    /// <summary>Statutory Neonatal Care Pay.</summary>
    StatutoryNeonatalCarePay,

    /// <summary>Occupational (i.e., employer voluntary) sick pay; allows single-line reporting of SSP and occupational
    /// sick pay.</summary>
    OccupationalSickPay,

    /// <summary>Occupational (i.e., employer voluntary) maternity pay; allows single-line reporting of SMP and
    /// occupational maternity pay.</summary>
    OccupationalMaternityPay,

    /// <summary>Occupational (i.e., employer voluntary) paternity pay; allows single-line reporting of SPP and
    /// occupational paternity pay.</summary>
    OccupationalPaternityPay,

    /// <summary>Occupational (i.e., employer voluntary) shared parental pay; allows single-line reporting of ShPP and
    /// occupational shared parental pay.</summary>
    OccupationalSharedParentalPay,

    /// <summary>Occupational (i.e., employer voluntary) adoption pay; allows single-line reporting of SAP and occupational
    /// adoption pay.</summary>
    OccupationalAdoptionPay,

    /// <summary>Occupational (i.e., employer voluntary) parental bereavement pay; allows single-line reporting of SPBP and
    /// occupational shared parental bereavement pay.</summary>
    OccupationalParentalBereavementPay,

    /// <summary>Adjustment to pay, for example, refund of previous deduction.</summary>
    Adjustment
}
