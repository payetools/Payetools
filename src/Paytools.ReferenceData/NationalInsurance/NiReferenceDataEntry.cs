// Copyright (c) 2023 Paytools Foundation.
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
    public ImmutableList<NiThresholdEntry> NiThresholds { get; init; } = default!;

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
    public ImmutableList<NiEmployeeRatesEntry>? DirectorEmployerRates { get; init; } = default!;

    /// <summary>
    /// Gets applicable employee NI rates for directors.  Only applicable when there has been an in-year
    /// change to National Insurance rates.
    /// </summary>
    public ImmutableList<NiEmployeeRatesEntry>? DirectorEmployeeRates { get; init; } = default!;
}