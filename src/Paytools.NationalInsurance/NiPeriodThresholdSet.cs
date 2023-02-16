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

namespace Paytools.NationalInsurance;

public record NiPeriodThresholdSet
{
    private readonly NiPeriodThresholdEntry[] _thresholdEntries;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entries"></param>
    /// <param name="payFrequency"></param>
    /// <param name="taxPeriod">Defaults to 1 for non-directors NI.</param>
    /// <exception cref="ArgumentException"></exception>
    public NiPeriodThresholdSet(List<NiThresholdEntry> entries,
        PayFrequency payFrequency, int taxPeriod = 1)
    {
        int entryCount = (int)NiThreshold.Count;

        if (entries.Count != entryCount)
            throw new ArgumentException($"Expected {entryCount} threshold entries but only {entries.Count} supplied in input list", nameof(entries));

        _thresholdEntries = new NiPeriodThresholdEntry[entryCount];

        for (int index = 0; index < entryCount; index++)
            _thresholdEntries[entries[index].Threshold.GetIndex()] = new NiPeriodThresholdEntry(entries[index],
                payFrequency, taxPeriod);
    }

    public decimal GetThreshold(NiThreshold threshold) =>
        _thresholdEntries[threshold.GetIndex()].ThresholdForPeriod;

    public decimal GetThreshold1(NiThreshold threshold) =>
        _thresholdEntries[threshold.GetIndex()].ThresholdForPeriod1;
}
