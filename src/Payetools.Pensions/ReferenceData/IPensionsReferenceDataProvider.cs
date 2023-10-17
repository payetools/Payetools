// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.Pensions.ReferenceData;

/// <summary>
/// Interface that classes implement in order to provide access to pensions reference data, i.e.,
/// rates and thresholds.
/// </summary>
public interface IPensionsReferenceDataProvider
{
    /// <summary>
    /// Gets the thresholds for Qualifying Earnings for the specified tax year and tax period, as denoted by the
    /// supplied pay frequency and pay period.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>A tuple containing the lower and upper thresholds for the specified pay frequency and point in time.</returns>
    (decimal LowerLimit, decimal UpperLimit) GetThresholdsForQualifyingEarnings(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod);

    /// <summary>
    /// Gets the basic rate of tax applicable across all tax regimes for relief at source pension contributions, for the specified
    /// tax year.  (As at the time of writing, one basic rate of tax is used across all jurisdictions in spite of the fact that
    /// some have a lower basic rate of tax.)
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.  Only used if there has been an in-year change.</param>
    /// <param name="taxPeriod">Applicable tax period.  Only used if there has been an in-year change.</param>
    /// <returns>Basic rate of tax applicable for the tax year.</returns>
    decimal GetBasicRateOfTaxForTaxRelief(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod);
}