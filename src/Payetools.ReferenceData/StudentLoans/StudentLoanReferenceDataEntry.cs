// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.ReferenceData.StudentLoans;

/// <summary>
/// Represents the reference data for student loans for a period; where there have been in-year changes,
/// then there may be several such entries for a given tax year, although this is very uncommon.
/// </summary>
public class StudentLoanReferenceDataEntry : IApplicableFromTill
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
    /// Gets the weekly, monthly and annual threshold for Plan 1 student loan deductions.
    /// </summary>
    public StudentLoanThresholdsEntry Plan1Thresholds { get; init; }

    /// <summary>
    /// Gets the weekly, monthly and annual threshold for Plan 2 student loan deductions.
    /// </summary>
    public StudentLoanThresholdsEntry Plan2Thresholds { get; init; }

    /// <summary>
    /// Gets the weekly, monthly and annual threshold for Plan 4 student loan deductions.
    /// </summary>
    public StudentLoanThresholdsEntry Plan4Thresholds { get; init; }

    /// <summary>
    /// Gets the weekly, monthly and annual threshold for post-graduate student loan deductions.
    /// </summary>
    public StudentLoanThresholdsEntry PostGradThresholds { get; init; }

    /// <summary>
    /// Gets the set of rates to be used for student and post-graduate loan deductions.
    /// </summary>
    public StudentLoanRatesSet DeductionRates { get; init; }
}