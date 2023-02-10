// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
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

/// <summary>
/// Enumerated value for payment frequency.
/// </summary>
public enum PayFrequency
{
    /// <summary>Not specified</summary>
    Unspecified,
    /// <summary>Weekly</summary>
    Weekly,
    /// <summary>Every two weeks</summary>
    TwoWeekly,
    /// <summary>Every four weeks</summary>
    FourWeekly,
    /// <summary>Monthly</summary>
    Monthly,
    /// <summary>Every three months</summary>
    Quarterly,
    /// <summary>Every six months</summary>
    BiAnnually,
    /// <summary>Once a year</summary>
    Annually
}

/// <summary>
/// 
/// </summary>
public static class PayFrequencyExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="payFrequency"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
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