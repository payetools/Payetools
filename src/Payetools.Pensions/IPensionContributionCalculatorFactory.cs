// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using Payetools.Pensions.Model;

namespace Payetools.Pensions;

/// <summary>
/// Interface that represents factories that can generate <see cref="IPensionContributionCalculator"/> implementations.
/// </summary>
public interface IPensionContributionCalculatorFactory
{
    /// <summary>
    /// Gets a new <see cref="IPensionContributionCalculator"/> based on the specified pay date and number of tax periods.  The pay date
    /// is provided in order to determine which set of thresholds (Qualifying Earnings only) and rates to use, noting that these may change
    /// in-year.
    /// </summary>
    /// <param name="earningsBasis">Earnings basis for pension calculation (Qualifying Earnings vs Pensionable Pay Set x.</param>
    /// <param name="taxTreatment">Tax treatment (net pay arrangement vs relief at source).</param>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>A new calculator instance.</returns>
    IPensionContributionCalculator GetCalculator(
        PensionsEarningsBasis earningsBasis,
        PensionTaxTreatment taxTreatment,
        PayDate payDate);

    /// <summary>
    /// Gets a new <see cref="IPensionContributionCalculator"/> based on the specified tax year, pay frequency and pay period, along with the
    /// applicable number of tax periods.  The tax year, pay frequency and pay period are provided in order to determine which
    /// set of thresholds and rates to use, noting that these may change in-year.
    /// </summary>
    /// <param name="earningsBasis">Earnings basis for pension calculation (Qualifying Earnings vs Pensionable Pay Set x.</param>
    /// <param name="taxTreatment">Tax treatment (net pay arrangement vs relief at source).</param>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>A new calculator instance.</returns>
    IPensionContributionCalculator GetCalculator(
        PensionsEarningsBasis earningsBasis,
        PensionTaxTreatment taxTreatment,
        TaxYear taxYear,
        PayFrequency payFrequency,
        int taxPeriod);
}