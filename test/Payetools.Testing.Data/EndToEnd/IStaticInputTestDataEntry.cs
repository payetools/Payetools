// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using Payetools.StudentLoans.Model;

namespace Payetools.Testing.Data.EndToEnd;

public interface IStaticInputTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string TestReference { get; }
    string EmployeeFirstName { get; }
    string EmployeeLastName { get; }
    TaxCode TaxCode { get; }
    NiCategory NiCategory { get; }
    StudentLoanType? StudentLoanPlan { get; }
    bool GraduateLoan { get; }
    string? PensionScheme { get; }
    bool UsesSalaryExchange { get; }
    decimal? EmployeePercentage { get; }
    decimal? EmployeeFixedAmount { get; }
    decimal? EmployerPercentage { get; }
}
