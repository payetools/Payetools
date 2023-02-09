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

namespace Paytools.Common.Model;

public enum PayFrequency
{
    Unspecified,
    Weekly,
    TwoWeekly,
    FourWeekly,
    Monthly,
    Quarterly,
    BiAnnually,
    Annually
}

public static class PayFrequencyExtensions
{
    public static int GetStandardTaxPeriodCount(this PayFrequency payFrequency)
    {
        return payFrequency switch
        {
            PayFrequency.Weekly => 52,
            PayFrequency.TwoWeekly => 26,
            PayFrequency.FourWeekly => 13,
            PayFrequency.Monthly => 12,
            PayFrequency.Quarterly => 4,
            PayFrequency.BiAnnually => 2,
            PayFrequency.Annually => 1,
            _ => throw new ArgumentException($"Invalid pay frequency value {payFrequency}", nameof(payFrequency))
        };
    }
}