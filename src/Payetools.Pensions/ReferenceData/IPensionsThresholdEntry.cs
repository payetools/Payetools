// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Pensions.ReferenceData;

/// <summary>
/// Interface for types that provide access to a given set of pensions threshold values.
/// </summary>
public interface IPensionsThresholdEntry
{
    /// <summary>
    /// Gets the per week value of the threshold.
    /// </summary>
    decimal ThresholdValuePerWeek { get; }

    /// <summary>
    /// Gets the per 2-week value of the threshold.
    /// </summary>
    decimal ThresholdValuePerTwoWeeks { get; }

    /// <summary>
    /// Gets the per 4-week value of the threshold.
    /// </summary>
    decimal ThresholdValuePerFourWeeks { get; }

    /// <summary>
    /// Gets the per month value of the threshold.
    /// </summary>
    decimal ThresholdValuePerMonth { get; }

    /// <summary>
    /// Gets the per quarter value of the threshold.
    /// </summary>
    decimal ThresholdValuePerQuarter { get; }

    /// <summary>
    /// Gets the per half-year value of the threshold.
    /// </summary>
    decimal ThresholdValuePerHalfYear { get; }

    /// <summary>
    /// Gets the per annum value of the threshold.
    /// </summary>
    decimal ThresholdValuePerYear { get; }
}