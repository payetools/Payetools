// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.StudentLoans.Model;

namespace Payetools.Testing.Data.EndToEnd;

public class StaticInputTestDataEntry : IStaticInputTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string TestReference { get; set; } = string.Empty;
    public string EmployeeFirstName { get; set; } = string.Empty;
    public string EmployeeLastName { get; set; } = string.Empty;
    public TaxCode TaxCode { get; set; }
    public NiCategory NiCategory { get; set; }
    public StudentLoanType? StudentLoanPlan { get; set; }
    public bool GraduateLoan { get; set; }
    public string? PensionScheme { get; set; }
    public bool UsesSalaryExchange { get; set; }
    public decimal? EmployeePercentage { get; set; }
    public decimal? EmployeeFixedAmount { get; set; }
    public decimal? EmployerPercentage { get; set; }
}
