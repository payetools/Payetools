// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.ComponentModel;

namespace Payetools.Common.Model;

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
    Fortnightly,

    /// <summary>Every four weeks</summary>
    FourWeekly,

    /// <summary>Monthly</summary>
    Monthly,

    /// <summary>Every three months</summary>
    Quarterly,

    /// <summary>Every six months</summary>
    BiAnnually,

    /// <summary>Once a year</summary>
    Annually,

    /// <summary>
    /// Equivalent to fortnightly (retained for backwards-compatibility).
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    TwoWeekly = Fortnightly
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
            PayFrequency.Fortnightly => 26,
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
            PayFrequency.Fortnightly => 14,
            PayFrequency.FourWeekly => 28,
            PayFrequency.Monthly => 12,
            PayFrequency.Quarterly => 4,
            PayFrequency.BiAnnually => 2,
            PayFrequency.Annually => 1,
            _ => throw new ArgumentException($"Invalid pay frequency value {payFrequency}", nameof(payFrequency))
        };
    }

    /// <summary>
    /// Determines whether the specified tax period is the last tax period in the tax year.
    /// </summary>
    /// <param name="payFrequency">Relevant pay frequency.</param>
    /// <param name="taxPeriod">Tax period to evaluate.</param>
    /// <param name="applyWeek53Treatment">Flag that indicates whether to apply "week 53" treatment, i.e., where
    /// there are 53 weeks in a tax year (or 27 periods in a two-weekly pay cycle, etc.).  Must be false
    /// for monthly, quarterly and annual payrolls.  Optional, defaulting to false.</param>
    /// <returns>true if the supplied tax period is the last period in the tax year; false otherwise.</returns>
    public static bool IsLastTaxPeriodInTaxYear(this PayFrequency payFrequency, in int taxPeriod, in bool applyWeek53Treatment = false)
    {
        if (applyWeek53Treatment && (payFrequency == PayFrequency.Monthly || payFrequency == PayFrequency.Quarterly || payFrequency == PayFrequency.Annually))
            throw new ArgumentException($"Parameter must be false for non-week-based payrolls", nameof(applyWeek53Treatment));

        return taxPeriod == payFrequency.GetStandardTaxPeriodCount() + (applyWeek53Treatment ? 1 : 0);
    }
}