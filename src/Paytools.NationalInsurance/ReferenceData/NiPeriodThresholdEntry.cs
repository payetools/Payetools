// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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

public record NiPeriodThresholdEntry
{
    public NiThreshold Threshold { get; }
    public PayFrequency PayFrequency { get; }
    public int NumberOfTaxPeriods { get; }
    public decimal ThresholdForPeriod { get; }
    public decimal ThresholdForPeriod1 { get; }

    public NiPeriodThresholdEntry(INiThresholdEntry baseEntry, PayFrequency payFrequency, int numberOfTaxPeriods)
    {
        Threshold = baseEntry.Threshold;
        PayFrequency = payFrequency;
        NumberOfTaxPeriods = numberOfTaxPeriods;

        // * This is what we might expect for obtaining the correct threshold for the period.  But HMRC guidance is that we
        // * we must always use the annual figure divided by the number of periods in the tax year.

        // ThresholdForPeriod = PayFrequency switch
        // {
        //    PayFrequency.Weekly => baseEntry.ThresholdValuePerWeek * TaxPeriod,
        //    PayFrequency.TwoWeekly => baseEntry.ThresholdValuePerWeek * TaxPeriod * 2,
        //    PayFrequency.FourWeekly => baseEntry.ThresholdValuePerWeek * TaxPeriod * 4,
        //    PayFrequency.Monthly => baseEntry.ThresholdValuePerMonth * TaxPeriod,
        //    PayFrequency.Annually => baseEntry.ThresholdValuePerYear,
        //    _ => throw new ArgumentException("Unrecognised PayFrequency value", nameof(payFrequency))
        // };

        var rawThresholdForPeriod = baseEntry.ThresholdValuePerYear / payFrequency.GetStandardTaxPeriodCount() * numberOfTaxPeriods;

        // From HMRC documentation: 'p' = number of weeks/months in pay period. Round result of calculation at this point
        // up to nearest whole pound.
        ThresholdForPeriod = decimal.Round(rawThresholdForPeriod, 0, MidpointRounding.AwayFromZero);

        // From HMRC documentation: 'p1' = number of weeks/months in pay period. If equals 1 round result of calculation
        // at this point to nearest whole pound. If more than 1 round up to whole pounds.
        ThresholdForPeriod1 = payFrequency == PayFrequency.Weekly || payFrequency == PayFrequency.Monthly ?
            decimal.Round(rawThresholdForPeriod, 0, MidpointRounding.AwayFromZero) :
            decimal.Round(rawThresholdForPeriod, 0, MidpointRounding.ToPositiveInfinity);
    }
}
