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

namespace Paytools.Pensions;

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
    /// <param name="basicRateOfTax">Basic rate of tax.  Optional.  [NOT YET IMPLEMENTED].</param>
    /// <returns>A new calculator instance.</returns>
    IPensionContributionCalculator GetCalculator(
        EarningsBasis earningsBasis,
        PensionTaxTreatment taxTreatment,
        PayDate payDate,
        decimal? basicRateOfTax = null);

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
    /// <param name="basicRateOfTax">Basic rate of tax.  Optional.  [NOT YET IMPLEMENTED].</param>
    /// <returns>A new calculator instance.</returns>
    IPensionContributionCalculator GetCalculator(
        EarningsBasis earningsBasis,
        PensionTaxTreatment taxTreatment,
        TaxYear taxYear,
        PayFrequency payFrequency,
        int taxPeriod,
        decimal? basicRateOfTax = null);
}