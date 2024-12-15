// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the amounts that can be reclaimed from HMRC against eligible statutory payments.
/// </summary>
public readonly struct StatutoryPaymentReclaim : IStatutoryPaymentReclaim
{
    /// <summary>
     /// Gets the total Statutory Maternity Pay amount for the tax month. May be zero.
     /// </summary>
     /// <remarks>This is the statutory amount repayable by HMRC, which is either the reduced amount using the current
     /// HMRC published percentage (e.g., 92% in 2024/5) for larger employers, or the total amount claimed (i.e., 100%)
     /// for employers that are entitled to Small Employers Relief. Any additional percentage-based reclaim amount
     /// (e.g., 3% in 2024/5) is given via the <see cref="AdditionalNiCompensationOnSMP"/> property.</remarks>
    public decimal ReclaimableStatutoryMaternityPay { get; init; }

    /// <summary>
    /// Gets the total Statutory Paternity Pay amount for the tax month. May be zero.
    /// </summary>
    /// <remarks>This is the statutory amount repayable by HMRC, which is either the reduced amount using the current
    /// HMRC published percentage (e.g., 92% in 2024/5) for larger employers, or the total amount claimed (i.e., 100%)
    /// for employers that are entitled to Small Employers Relief. Any additional percentage-based reclaim amount
    /// (e.g., 3% in 2024/5) is given via the <see cref="AdditionalNiCompensationOnSPP"/> property.</remarks>
    public decimal ReclaimableStatutoryPaternityPay { get; init; }

    /// <summary>
    /// Gets the total Statutory Adoption Pay amount for the tax month. May be zero.
    /// </summary>
    /// <remarks>This is the statutory amount repayable by HMRC, which is either the reduced amount using the current
    /// HMRC published percentage (e.g., 92% in 2024/5) for larger employers, or the total amount claimed (i.e., 100%)
    /// for employers that are entitled to Small Employers Relief. Any additional percentage-based reclaim amount
    /// (e.g., 3% in 2024/5) is given via the <see cref="AdditionalNiCompensationOnSAP"/> property.</remarks>
    public decimal ReclaimableStatutoryAdoptionPay { get; init; }

    /// <summary>
    /// Gets the total Statutory Shared Parental Pay amount for the tax month. May be zero.
    /// </summary>
    /// <remarks>This is the statutory amount repayable by HMRC, which is either the reduced amount using the current
    /// HMRC published percentage (e.g., 92% in 2024/5) for larger employers, or the total amount claimed (i.e., 100%)
    /// for employers that are entitled to Small Employers Relief. Any additional percentage-based reclaim amount
    /// (e.g., 3% in 2024/5) is given via the <see cref="AdditionalNiCompensationOnSShPP"/> property.</remarks>
    public decimal ReclaimableStatutorySharedParentalPay { get; init; }

    /// <summary>
    /// Gets the total Statutory Parental Bereavement Pay amount for the tax month. May be zero.
    /// </summary>
    /// <remarks>This is the statutory amount repayable by HMRC, which is either the amount reduced by the current
    /// HMRC published percentage (e.g., 92% in 2024/5) for larger employers, or the total amount claimed (i.e., 100%)
    /// for employers that are entitled to Small Employers Relief. Any additional percentage-based reclaim amount
    /// (e.g., 3% in 2024/5) is given via the <see cref="AdditionalNiCompensationOnSPBP"/> property.</remarks>
    public decimal ReclaimableStatutoryParentalBereavementPay { get; init; }

    /// <summary>
    /// Gets the total Statutory Neonatal Care Pay amount for the tax month. May be zero.
    /// </summary>
    /// <remarks>This is the statutory amount repayable by HMRC, which is either the amount reduced by the current
    /// HMRC published percentage (e.g., 92% in 2024/5) for larger employers, or the total amount claimed (i.e., 100%)
    /// for employers that are entitled to Small Employers Relief. Any additional percentage-based reclaim amount
    /// (e.g., 3% in 2024/5) is given via the <see cref="AdditionalNiCompensationOnSNCP"/> property.</remarks>
    public decimal ReclaimableStatutoryNeonatalCarePay { get; init; }

    /// <summary>
    /// Gets any additional National Insurance compensation available for Statutory Maternity Pay
    /// in the case that the employer qualifies for Small Employers Relief.  Note that the value
    /// <see cref="ReclaimableStatutoryMaternityPay"/> already includes this amount (if applicable).
    /// </summary>
    public decimal AdditionalNiCompensationOnSMP { get; init; }

    /// <summary>
    /// Gets any additional National Insurance compensation available for Statutory Paternity Pay
    /// in the case that the employer qualifies for Small Employers Relief.  Note that the value
    /// <see cref="ReclaimableStatutoryPaternityPay"/> already includes this amount (if applicable).
    /// </summary>
    public decimal AdditionalNiCompensationOnSPP { get; init; }

    /// <summary>
    /// Gets any additional National Insurance compensation available for Statutory Adoption Pay
    /// in the case that the employer qualifies for Small Employers Relief.  Note that the value
    /// <see cref="ReclaimableStatutoryAdoptionPay"/> already includes this amount (if applicable).
    /// </summary>
    public decimal AdditionalNiCompensationOnSAP { get; init; }

    /// <summary>
    /// Gets any additional National Insurance compensation available for Statutory Shared Parental Pay
    /// in the case that the employer qualifies for Small Employers Relief.  Note that the value
    /// <see cref="ReclaimableStatutorySharedParentalPay"/> already includes this amount (if applicable).
    /// </summary>
    public decimal AdditionalNiCompensationOnSShPP { get; init; }

    /// <summary>
    /// Gets any additional National Insurance compensation available for Statutory Parental Bereavement Pay
    /// in the case that the employer qualifies for Small Employers Relief.  Note that the value
    /// <see cref="ReclaimableStatutoryParentalBereavementPay"/> already includes this amount (if applicable).
    /// </summary>
    public decimal AdditionalNiCompensationOnSPBP { get; init; }

    /// <summary>
    /// Gets any additional National Insurance compensation available for Statutory Neonatal Care Pay
    /// in the case that the employer qualifies for Small Employers Relief.  Note that the value
    /// <see cref="ReclaimableStatutoryNeonatalCarePay"/> already includes this amount (if applicable).
    /// </summary>
    public decimal AdditionalNiCompensationOnSNCP { get; init; }

    /// <summary>
    /// Gets any CIS deductions suffered.
    /// </summary>
    public decimal CisDeductionsSuffered { get; init; }
}