// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.IncomeTax.ReferenceData;
using Paytools.NationalInsurance.ReferenceData;
using Paytools.NationalMinimumWage.ReferenceData;
using Paytools.Pensions.ReferenceData;
using Paytools.StudentLoans.ReferenceData;

namespace Paytools.ReferenceData;

/// <summary>
/// Interface that HMRC reference data providers must implement.
/// </summary>
public interface IHmrcReferenceDataProvider :
    ITaxReferenceDataProvider,
    INiReferenceDataProvider,
    IPensionsReferenceDataProvider,
    INmwReferenceDataProvider,
    IStudentLoanReferenceDataProvider
{
    /// <summary>
    /// Gets the human-readable 'health' of this reference data provider.
    /// </summary>
    string Health { get; }
}