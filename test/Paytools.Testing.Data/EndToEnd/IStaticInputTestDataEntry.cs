// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using Paytools.StudentLoans.Model;

namespace Paytools.Testing.Data.EndToEnd;

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
