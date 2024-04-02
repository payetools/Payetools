// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Immutable;

namespace Payetools.ReferenceData.NationalInsurance;

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
    public ImmutableArray<NiReferenceDataThresholdEntry> NiThresholds { get; init; } = default!;

    /// <summary>
    /// Gets applicable NI rates for employees.
    /// </summary>
    public ImmutableArray<NiEmployerRatesEntry> EmployerRates { get; init; } = default!;

    /// <summary>
    /// Gets applicable NI rates for employees.
    /// </summary>
    public ImmutableArray<NiEmployeeRatesEntry> EmployeeRates { get; init; } = default!;

    /// <summary>
    /// Gets applicable employer NI rates for directors.  Only applicable when there has been an in-year
    /// change to National Insurance rates.
    /// </summary>
    public ImmutableArray<NiEmployerRatesEntry>? DirectorEmployerRates { get; init; } = default!;

    /// <summary>
    /// Gets applicable employee NI rates for directors.  Only applicable when there has been an in-year
    /// change to National Insurance rates.
    /// </summary>
    public ImmutableArray<NiEmployeeRatesEntry>? DirectorEmployeeRates { get; init; } = default!;
}