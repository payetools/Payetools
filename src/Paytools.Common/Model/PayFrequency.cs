// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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
/// Extension methods for <see cref="PayFrequency"/>.
/// </summary>
public static class PayFrequencyExtensions
{
    /// <summary>
    /// Provides access to the number of tax periods within a tax year for a given <see cref="PayFrequency"/>.
    /// </summary>
    /// <param name="payFrequency">PayFrequency to provide period count for.</param>
    /// <returns>The number of tax periods within a tax year for a this PayFrequency, for example, PayFrequency.Monthly returns 12.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid PayFrequency value is supplied.</exception>
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

    /// <summary>
    /// Provides access to the number of tax periods within a tax year for a given <see cref="PayFrequency"/>.
    /// </summary>
    /// <param name="payFrequency">PayFrequency to provide period count for.</param>
    /// <returns>The number of tax periods within a tax year for a this PayFrequency, for example, PayFrequency.Monthly returns 12.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid PayFrequency value is supplied.</exception>
    public static int GetTaxPeriodLength(this PayFrequency payFrequency)
    {
        return payFrequency switch
        {
            PayFrequency.Weekly => 7,
            PayFrequency.TwoWeekly => 14,
            PayFrequency.FourWeekly => 28,
            PayFrequency.Monthly => 12,
            PayFrequency.Quarterly => 4,
            PayFrequency.BiAnnually => 2,
            PayFrequency.Annually => 1,
            _ => throw new ArgumentException($"Invalid pay frequency value {payFrequency}", nameof(payFrequency))
        };
    }
}