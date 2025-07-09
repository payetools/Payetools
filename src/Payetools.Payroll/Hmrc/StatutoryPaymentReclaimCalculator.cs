// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Model;
using Payetools.ReferenceData.Employer;

namespace Payetools.Payroll.Hmrc;

/// <summary>
/// Aggregator that provides summarisation across multiple pay runs.
/// </summary>
public class StatutoryPaymentReclaimCalculator : IStatutoryPaymentReclaimCalculator
{
    private readonly StatutoryPaymentReclaimInfo _statutoryPaymentReclaimInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatutoryPaymentReclaimCalculator"/> class.
    /// </summary>
    /// <param name="statutoryPaymentReclaimInfo">Statutory payment reclaim reference data.</param>
    public StatutoryPaymentReclaimCalculator(StatutoryPaymentReclaimInfo statutoryPaymentReclaimInfo)
    {
        _statutoryPaymentReclaimInfo = statutoryPaymentReclaimInfo;
    }

    /// <summary>
    /// Calculates the allowable reclaim amounts for all reclaimable statutory payments.
    /// </summary>
    /// <param name="employer">Employer that this calculation pertains to.</param>
    /// <param name="employerMonthEntry">Aggregated month's figures for a given employer.</param>
    /// <param name="reclaim">New instance of object that implements <see cref="IEmployerHistoryEntry"/> containing
    /// the reclaimable amounts for each statutory payment.</param>
    /// <remarks>Fractions of a penny are rounded up, as per
    /// https://www.gov.uk/hmrc-internal-manuals/statutory-payments-manual/spm180500.</remarks>
    public void Calculate(
        in IEmployer employer,
        in IEmployerHistoryEntry employerMonthEntry,
        out IStatutoryPaymentReclaim reclaim)
    {
        decimal reclaimRate = employer.IsEligibleForSmallEmployersRelief ?
            1.0m : _statutoryPaymentReclaimInfo.DefaultReclaimRate;

        decimal additionalRate = employer.IsEligibleForSmallEmployersRelief ?
            _statutoryPaymentReclaimInfo.SmallEmployersReclaimRate - 1.0m : 0.0m;

        reclaim = new StatutoryPaymentReclaim
        {
            ReclaimableStatutoryMaternityPay = decimal.Round(employerMonthEntry.TotalStatutoryMaternityPay * reclaimRate, 2, MidpointRounding.ToPositiveInfinity),
            ReclaimableStatutoryAdoptionPay = decimal.Round(employerMonthEntry.TotalStatutoryAdoptionPay * reclaimRate, 2, MidpointRounding.ToPositiveInfinity),
            ReclaimableStatutoryPaternityPay = decimal.Round(employerMonthEntry.TotalStatutoryPaternityPay * reclaimRate, 2, MidpointRounding.ToPositiveInfinity),
            ReclaimableStatutorySharedParentalPay = decimal.Round(employerMonthEntry.TotalStatutorySharedParentalPay * reclaimRate, 2, MidpointRounding.ToPositiveInfinity),
            ReclaimableStatutoryParentalBereavementPay = decimal.Round(employerMonthEntry.TotalStatutoryParentalBereavementPay * reclaimRate, 2, MidpointRounding.ToPositiveInfinity),
            ReclaimableStatutoryNeonatalCarePay = decimal.Round(employerMonthEntry.TotalStatutoryNeonatalCarePay * reclaimRate, 2, MidpointRounding.ToPositiveInfinity),
            AdditionalNiCompensationOnSMP = decimal.Round(employerMonthEntry.TotalStatutoryMaternityPay * additionalRate, 2, MidpointRounding.ToPositiveInfinity),
            AdditionalNiCompensationOnSAP = decimal.Round(employerMonthEntry.TotalStatutoryAdoptionPay * additionalRate, 2, MidpointRounding.ToPositiveInfinity),
            AdditionalNiCompensationOnSPP = decimal.Round(employerMonthEntry.TotalStatutoryPaternityPay * additionalRate, 2, MidpointRounding.ToPositiveInfinity),
            AdditionalNiCompensationOnSShPP = decimal.Round(employerMonthEntry.TotalStatutorySharedParentalPay * additionalRate, 2, MidpointRounding.ToPositiveInfinity),
            AdditionalNiCompensationOnSPBP = decimal.Round(employerMonthEntry.TotalStatutoryParentalBereavementPay * additionalRate, 2, MidpointRounding.ToPositiveInfinity),
            AdditionalNiCompensationOnSNCP = decimal.Round(employerMonthEntry.TotalStatutoryNeonatalCarePay * additionalRate, 2, MidpointRounding.ToPositiveInfinity)
        };
    }
}