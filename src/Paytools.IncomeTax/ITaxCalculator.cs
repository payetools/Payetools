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
using Paytools.IncomeTax.ReferenceData;

namespace Paytools.IncomeTax;

/// <summary>
/// Interface that represents a calculator for calculating income tax based on tax code, taxable income and tax paid to date.
/// Access to tax calculators is through the <see cref="TaxCalculatorFactory"/>; in normal use, implementations of ITaxCalculator
/// are not be created directly.  An ITaxCalculator instance is specific to a given pay frequency and tax period, which
/// corresponds to an instance of a given <see cref="PayDate"/>.
/// </summary>
public interface ITaxCalculator
{
    /// <summary>
    /// Gets the set of pro-rata tax bandwidths in use for a given tax year, tax regime and tax period.
    /// </summary>
    TaxPeriodBandwidthSet TaxBandwidths { get; }

    /// <summary>
    /// Gets the pay frequency for this calculator.
    /// </summary>
    PayFrequency PayFrequency { get; }

    /// <summary>
    /// Gets the relevant tax period for this calculator.
    /// </summary>
    int TaxPeriod { get; }

    /// <summary>
    /// Gets the number of tax periods within a given tax year, based on the supplied pay frequency.
    /// </summary>
    int TaxPeriodCount { get; }

    /// <summary>
    /// Calculates the tax due based on tax code, total taxable salary and total tax paid to date.
    /// </summary>
    /// <param name="totalTaxableSalaryInPeriod">Taxable pay in period (i.e., gross less pre-tax deductions but including benefits in kind).</param>
    /// <param name="benefitsInKind">Benefits in kind element of the taxable pay for the period.</param>
    /// <param name="taxCode">Individual's tax code.</param>
    /// <param name="taxableSalaryYearToDate">Total year to date taxable salary up to and including the end of the previous tax period.</param>
    /// <param name="taxPaidYearToDate">Total year to date tax paid up to and including the end of the previous tax period.</param>
    /// <param name="taxUnpaidDueToRegulatoryLimit">Any tax outstanding due to the effect of the regulatory limit.</param>
    /// <returns>An <see cref="ITaxCalculationResult"/> containing the tax now due plus related information from the tax calculation.</returns>
    ITaxCalculationResult Calculate(
        decimal totalTaxableSalaryInPeriod,
        decimal benefitsInKind,
        TaxCode taxCode,
        decimal taxableSalaryYearToDate,
        decimal taxPaidYearToDate,
        decimal taxUnpaidDueToRegulatoryLimit = 0.0m);
}