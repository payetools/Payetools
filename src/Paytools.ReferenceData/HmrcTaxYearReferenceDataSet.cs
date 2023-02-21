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
using Paytools.ReferenceData.IncomeTax;
using Paytools.ReferenceData.NationalInsurance;
using Paytools.ReferenceData.Pensions;

namespace Paytools.ReferenceData;

/// <summary>
/// Data structure used to represent HMRC reference data for a given tax year.
/// </summary>
public class HmrcTaxYearReferenceDataSet
{
    /// <summary>
    /// Gets the version of this data set.  Every time the data set is updated centrally, the version number is incremented.
    /// </summary>
    public string Version { get; init; } = "Not specified";

    /// <summary>
    /// Gets the latest update timestamp for this data set.
    /// </summary>
    public DateTime LatestUpdateTimestamp { get; init; }

    public TaxYearEnding ApplicableTaxYearEnding { get; init; }

    public List<IncomeTaxReferenceDataSet> IncomeTax { get; init; } = default!;

    public List<NiReferenceDataEntry> NationalInsurance { get; init; } = default!;

    public List<PensionsReferenceDataSet> Pensions { get; init; } = default!;
}