// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using System.Collections.Immutable;

namespace Paytools.ReferenceData.NationalInsurance;

/// <summary>
/// Represents a set of Ni thresholds and rates for a period; where there have been in-year changes,
/// then there may be several such entries for a given tax year.
/// </summary>
public class NiReferenceDataEntry : IApplicableFromTill
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
    /// Gets the basic rate of tax to be applied for tax relief on employee pension contributions
    /// for relief at source pensions.
    /// </summary>
    public decimal BasicRateOfTaxForTaxRelief { get; init; }

    /// <summary>
    /// Gets a read-only list of applicable NI thresholds.
    /// </summary>
    public ImmutableList<NiReferenceDataThresholdEntry> NiThresholds { get; init; } = default!;

    /// <summary>
    /// Gets applicable NI rates for employees.
    /// </summary>
    public ImmutableList<NiEmployerRatesEntry> EmployerRates { get; init; } = default!;

    /// <summary>
    /// Gets applicable NI rates for employees.
    /// </summary>
    public ImmutableList<NiEmployeeRatesEntry> EmployeeRates { get; init; } = default!;

    /// <summary>
    /// Gets applicable employer NI rates for directors.  Only applicable when there has been an in-year
    /// change to National Insurance rates.
    /// </summary>
    public ImmutableList<NiEmployerRatesEntry>? DirectorEmployerRates { get; init; } = default!;

    /// <summary>
    /// Gets applicable employee NI rates for directors.  Only applicable when there has been an in-year
    /// change to National Insurance rates.
    /// </summary>
    public ImmutableList<NiEmployeeRatesEntry>? DirectorEmployeeRates { get; init; } = default!;
}