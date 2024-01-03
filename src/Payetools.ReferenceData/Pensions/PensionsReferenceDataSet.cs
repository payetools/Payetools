// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.ReferenceData.Pensions;

/// <summary>
/// Represents the reference data for pensions for a period; where there have been in-year changes,
/// then there may be several such entries for a given tax year, although this is very uncommon.
/// </summary>
public class PensionsReferenceDataSet : IApplicableFromTill
{
    /// <summary>
    /// Gets the start date (i.e., the first full day) for applicability.
    /// </summary>
    public DateOnly ApplicableFrom { get; init; }

    /// <summary>
    /// Gets the end date (i.e., the last full day) for applicability.
    /// </summary>
    public DateOnly ApplicableTill { get; init; }

    /// <summary>
    /// Gets the basic rate of tax applicable across all tax regimes for relief at source pension contributions, for the specified
    /// tax year.  (As at the time of writing, one basic rate of tax is used across all jurisdictions in spite of the fact that
    /// some have a lower basic rate of tax.)
    /// </summary>
    public decimal BasicRateOfTaxForTaxRelief { get; init; }

    /// <summary>
    /// Gets the lower set of earnings thresholds for Qualifying Earnings (i.e., per week, per 2-weeks, etc.).
    /// </summary>
    public PensionsThresholdEntry QualifyingEarningsLowerLevel { get; init; }

    /// <summary>
    /// Gets the upper set of earnings thresholds for Qualifying Earnings (i.e., per week, per 2-weeks, etc.).
    /// </summary>
    public PensionsThresholdEntry QualifyingEarningsUpperLevel { get; init; }

    /// <summary>
    /// Gets the set of earnings triggers for Automatic Enrolment (i.e., per week, per 2-weeks, etc.).
    /// </summary>
    public PensionsThresholdEntry AeEarningsTrigger { get; init; }
}
