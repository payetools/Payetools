// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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
    /// <param name="taxPeriod">Application tax period.</param>
    /// <returns>An instance of <see cref="IPensionsReferenceDataSet"/> containing the thresholds for the specified point
    /// in time.</returns>
    IPensionsReferenceDataSet GetThresholdsForQualifyingEarnings(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod);
}
