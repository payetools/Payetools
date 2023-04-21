// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.NationalInsurance.Model;

namespace Paytools.NationalInsurance.ReferenceData;

/// <summary>
/// Interface for types that provide access to a given NI threshold value.
/// </summary>
public interface INiThresholdEntry
{
    /// <summary>
    /// Gets the type of threshold this instance pertains to.
    /// </summary>
    NiThresholdType ThresholdType { get; }

    /// <summary>
    /// Gets the per annum value of the threshold.
    /// </summary>
    decimal ThresholdValuePerYear { get; }
}