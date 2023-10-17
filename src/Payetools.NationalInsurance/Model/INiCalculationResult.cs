// Copyright (c) 2023 Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using Payetools.NationalInsurance.ReferenceData;

namespace Payetools.NationalInsurance.Model;

/// <summary>
/// Interface for types representing National Insurance calculation results.
/// </summary>
public interface INiCalculationResult
{
    /// <summary>
    /// Gets the NI category used for this calculation.
    /// </summary>
    NiCategory NiCategory { get; }

    /// <summary>
    /// Gets the gross pay for NI purposes ("Nicable pay") used in this calculation.
    /// </summary>
    decimal NicablePay { get; }

    /// <summary>
    /// Gets the rates used for this calculation.
    /// </summary>
    INiCategoryRatesEntry RatesUsed { get; }

    /// <summary>
    /// Gets the set of thresholds used for this calculation.  These thresholds are adjusted to match the
    /// length of the pay period.
    /// </summary>
    INiThresholdSet ThresholdsUsed { get; }

    /// <summary>
    /// Gets the breakdown of earnings across each of the different National Insurance thresholds.
    /// </summary>
    NiEarningsBreakdown EarningsBreakdown { get; }

    /// <summary>
    /// Gets the total employee contribution due as a result of this calculation.
    /// </summary>
    decimal EmployeeContribution { get; }

    /// <summary>
    /// Gets the total employer contribution due as a result of this calculation.
    /// </summary>
    decimal EmployerContribution { get; }

    /// <summary>
    /// Gets the total contribution due (employee + employer) as a result of this calculation.
    /// </summary>
    decimal TotalContribution { get; }

    /// <summary>
    /// Gets a value indicating whether the results of this calculation need to be reported to HMRC.
    /// </summary>
    bool NoRecordingRequiredIndicator { get; }
}
