// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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