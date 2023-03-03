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

namespace Paytools.Pensions.ReferenceData;

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