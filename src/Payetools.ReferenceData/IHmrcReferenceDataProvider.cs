// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.IncomeTax.ReferenceData;
using Payetools.NationalInsurance.ReferenceData;
using Payetools.NationalMinimumWage.ReferenceData;
using Payetools.Pensions.ReferenceData;
using Payetools.StudentLoans.ReferenceData;

namespace Payetools.ReferenceData;

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