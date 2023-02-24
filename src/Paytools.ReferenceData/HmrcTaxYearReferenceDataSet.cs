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
using Paytools.ReferenceData.NationalMinimumWage;
using Paytools.ReferenceData.Pensions;
using Paytools.ReferenceData.StudentLoans;

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

    /// <summary>
    /// Gets the tax year to which this data set applies.
    /// </summary>
    public TaxYearEnding ApplicableTaxYearEnding { get; init; }

    /// <summary>
    /// Gets a set of <see cref="IncomeTaxReferenceDataEntry"/>s, each entry applicable to a portion of the tax year.  Where
    /// the same regime applies across the entire tax year, this set contains only one entry.
    /// </summary>
    public List<IncomeTaxReferenceDataEntry> IncomeTax { get; init; } = default!;

    /// <summary>
    /// Gets a set of <see cref="NiReferenceDataEntry"/>s, each entry applicable to a portion of the tax year.  Where
    /// the same regime applies across the entire tax year, this set contains only one entry.
    /// </summary>
    public List<NiReferenceDataEntry> NationalInsurance { get; init; } = default!;

    /// <summary>
    /// Gets a set of <see cref="PensionsReferenceDataSet"/>s, each entry applicable to a portion of the tax year.  Where
    /// the same regime applies across the entire tax year, this set contains only one entry.
    /// </summary>
    public List<PensionsReferenceDataSet> Pensions { get; init; } = default!;

    /// <summary>
    /// Gets a set of <see cref="NmwReferenceDataEntry"/>s, each entry applicable to a portion of the tax year.  Where
    /// the same regime applies across the entire tax year, this set contains only one entry.
    /// </summary>
    public List<NmwReferenceDataEntry> NationalMinimumWage { get; init; } = default!;

    /// <summary>
    /// Gets a set of <see cref="StudentLoanReferenceDataEntry"/>s, each entry applicable to a portion of the tax year.  Where
    /// the same regime applies across the entire tax year, this set contains only one entry.
    /// </summary>
    public List<StudentLoanReferenceDataEntry> StudentLoans { get; init; } = default!;
}