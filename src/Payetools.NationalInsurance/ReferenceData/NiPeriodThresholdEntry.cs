// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;

namespace Payetools.NationalInsurance.ReferenceData;

/// <summary>
/// Represents a specific National Insurance threshold (e.g., LEL, UEL) adjusted for a specific pay frequency and
/// number of periods, as NI is calculated based on the appropriate fraction of the applicable annual threshold.
/// </summary>
public class NiPeriodThresholdEntry
{
    /// <summary>
    /// Gets the type of this threshold entry (e.g., LEL, PT, ST, etc.).
    /// </summary>
    public NiThresholdType ThresholdType { get; }

    /// <summary>
    /// Gets the applicable pay frequency for this period threshold.
    /// </summary>
    public PayFrequency PayFrequency { get; }

    /// <summary>
    /// Gets the number of tax periods applicable for this period threshold.
    /// </summary>
    public int NumberOfTaxPeriods { get; }

    /// <summary>
    /// Gets the base threshold for the period, as distinct from <see cref="ThresholdForPeriod1"/> (see below).
    /// </summary>
    public decimal ThresholdForPeriod { get; }

    /// <summary>
    /// Gets the modified threshold for the period (as distinct from <see cref="ThresholdForPeriod"/>) where rounding is
    /// applied based on whether the pay frequency is weekly or monthly, or otherwise.  As detailed in HMRC's NI calculation
    /// documentation as 'p1'.
    /// </summary>
    public decimal ThresholdForPeriod1 { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="NiPeriodThresholdEntry"/> based on the supplied annual threshold,
    /// specified pay frequency and optional number of tax periods.
    /// </summary>
    /// <param name="baseEntry">Annual threshold entry to calculate period threshold from.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="numberOfTaxPeriods">Number of pay periods.  Defaults to 1.</param>
    public NiPeriodThresholdEntry(INiThresholdEntry baseEntry, PayFrequency payFrequency, int numberOfTaxPeriods = 1)
    {
        ThresholdType = baseEntry.ThresholdType;
        PayFrequency = payFrequency;
        NumberOfTaxPeriods = numberOfTaxPeriods;

        // * This is what we might expect for obtaining the correct threshold for the period.  But HMRC guidance is that we
        // * we MUST always use the annual figure divided by the number of periods in the tax year.
        //
        // ThresholdForPeriod = PayFrequency switch
        // {
        //    PayFrequency.Weekly => baseEntry.ThresholdValuePerWeek * TaxPeriod,
        //    PayFrequency.TwoWeekly => baseEntry.ThresholdValuePerWeek * TaxPeriod * 2,
        //    PayFrequency.FourWeekly => baseEntry.ThresholdValuePerWeek * TaxPeriod * 4,
        //    PayFrequency.Monthly => baseEntry.ThresholdValuePerMonth * TaxPeriod,
        //    PayFrequency.Annually => baseEntry.ThresholdValuePerYear,
        //    _ => throw new ArgumentException("Unrecognised PayFrequency value", nameof(payFrequency))
        // };
        //
        // (On this basis, ThresholdValuePerWeek and ThresholdValuePerMonth are excluded from INiThresholdEntry.)

        var rawThresholdForPeriod = baseEntry.ThresholdValuePerYear / payFrequency.GetStandardTaxPeriodCount() * numberOfTaxPeriods;

        // From HMRC documentation: 'p' = number of weeks/months in pay period. Round result of calculation at this point
        // up to nearest whole pound.
        ThresholdForPeriod = decimal.Round(rawThresholdForPeriod, 0, MidpointRounding.AwayFromZero);

        var useStandardRoundingApproach = (payFrequency == PayFrequency.Weekly || payFrequency == PayFrequency.Monthly) && numberOfTaxPeriods == 1;

        // From HMRC documentation: 'p1' = number of weeks/months in pay period. If equals 1 round result of calculation
        // at this point to nearest whole pound. If more than 1 round up to whole pounds.
        ThresholdForPeriod1 = useStandardRoundingApproach ?
            decimal.Round(rawThresholdForPeriod, 0, MidpointRounding.AwayFromZero) :
            decimal.Round(rawThresholdForPeriod, 0, MidpointRounding.ToPositiveInfinity);
    }

    /// <summary>
    /// Gets a string representation of this <see cref="NiPeriodThresholdEntry"/>.
    /// </summary>
    /// <returns>String representation of this instance.</returns>
    public override string ToString() =>
        $"{ThresholdType}: {PayFrequency} ({ThresholdForPeriod}, {ThresholdForPeriod})";
}