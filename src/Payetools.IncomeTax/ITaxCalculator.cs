// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.IncomeTax.Model;
using Payetools.IncomeTax.ReferenceData;

namespace Payetools.IncomeTax;

/// <summary>
/// Interface that represents a calculator for calculating income tax based on tax code, taxable income and tax paid to date.
/// Access to tax calculators is through the <see cref="TaxCalculatorFactory"/>; in normal use, implementations of ITaxCalculator
/// are not be created directly.  An ITaxCalculator instance is specific to a given pay frequency and tax period, which
/// corresponds to an instance of a given <see cref="PayDate"/>.
/// </summary>
public interface ITaxCalculator
{
    /// <summary>
    /// Gets the tax year that this calculator pertains to.
    /// </summary>
    TaxYear TaxYear { get; }

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
    /// <param name="result">An <see cref="ITaxCalculationResult"/> containing the tax now due plus related information from the tax calculation.</param>
    void Calculate(
        decimal totalTaxableSalaryInPeriod,
        decimal benefitsInKind,
        TaxCode taxCode,
        decimal taxableSalaryYearToDate,
        decimal taxPaidYearToDate,
        decimal taxUnpaidDueToRegulatoryLimit,
        out ITaxCalculationResult result);
}