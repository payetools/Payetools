// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Paytools.Common.Model;

namespace Paytools.NationalInsurance.ReferenceData;

/// <summary>
/// Represents a specific National Insurance threshold (e.g., LEL, UEL) adjusted for a specific pay frequency and
/// number of periods, as NI is calculated based on the appropriate fraction of the applicable annual threshold.
/// </summary>
public record NiPeriodThresholdEntry
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

        // From HMRC documentation: 'p1' = number of weeks/months in pay period. If equals 1 round result of calculation
        // at this point to nearest whole pound. If more than 1 round up to whole pounds.
        ThresholdForPeriod1 = numberOfTaxPeriods == 1 ?
            decimal.Round(rawThresholdForPeriod, 0, MidpointRounding.AwayFromZero) :
            decimal.Round(rawThresholdForPeriod, 0, MidpointRounding.ToPositiveInfinity);
    }
}