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
using System.Collections.ObjectModel;

namespace Paytools.IncomeTax.ReferenceData;

/// <summary>
/// Interface that represents a provider of tax band information.
/// </summary>
public interface ITaxReferenceDataProvider
{
    /// <summary>
    /// Retrieves the tax bands for a given tax year in the form of a dictionary (<see cref="ReadOnlyDictionary{CountriesForTaxPurposes, TaxBandwidthSet}"/>)
    /// keyed on tax regime, i.e., <see cref="CountriesForTaxPurposes"/>.
    /// </summary>
    /// <param name="taxYear">Desired tax year.</param>
    /// <param name="payFrequency">Pay frequency pertaining.  Used in conjunction with the taxPeriod parameter to
    /// determine the applicable date (in case of in-year tax changes).</param>
    /// <param name="taxPeriod">Tax period, e.g., 1 for month 1.  Currently ignored on the assumption that in-year
    /// tax changes are not anticipated but provided for future .</param>
    /// <returns>ReadOnlyDictionary of <see cref="TaxBandwidthSet"/>'s keyed on tax regime.</returns>
    /// <remarks>Although ReadOnlyDictionary is not guaranteed to be thread-safe, in the current implementation the
    /// underlying Dictionary is guaranteed not to change, so thread-safety can be assumed.</remarks>
    ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet> GetTaxBandsForTaxYearAndPeriod(
        TaxYear taxYear,
        PayFrequency payFrequency,
        int taxPeriod);
}