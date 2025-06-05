// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

// This example code may be freely used without restriction; parts may be freely copied and
// used without attribution but when the entire file is copied, the copyright notice above
// must be retained.

// Assumes the project has a reference to Payetools.Payroll available on nuget

using Payetools.Common.Model;
using Payetools.Example;
using Payetools.Example.Earnings;
using Payetools.Payroll.Model;
using Payetools.Payroll.PayRuns;
using Payroll;

string[] ReferenceDataResources =
{
    @"Resources\hmrc-reference-data-2025-2026.json"
};

// ##### Step 1 - create the employee inputs #####
var employeePayRunInputs = new EmployeePayRunInputs<int>(
    1001,                                                     // Employee ID
    "1257L",                                                  // Tax code
    NiCategory.A,                                             // NI category
    null,                                                     // No director info for this example
    new StudentLoanInfo { StudentLoanType = StudentLoanType.Plan1 },
    [
        new EarningsEntry
        {
            EarningsDetails = new SalaryEarningsDetails(),
            FixedAmount = 2500.00m
        }
    ],
    [],                                                       // No deductions for this example
    [],                                                       // No payrolled benefits for this example
    null,                                                     // No attachment of earnings orders
    new PensionContributions(
        PensionsEarningsBasis.QualifyingEarnings,             // Earnings basis
        PensionTaxTreatment.NetPayArrangement,                // Tax treatment
        5.0m,                                                 // Employee contribution (5%)
        false,                                                // Employee contribution is not a fixed amount
        3.0m,                                                 // Employer contribution (3%)
        false),                                               // Employer contribution is not a fixed amount
    new EmployeeCoreYtdFigures());

// ##### Step 2 - create the pay run #####
var payDate = new PayDate(new DateOnly(2025, 5, 17), PayFrequency.Monthly);
var payRunDetails = new PayRunDetails(
    payDate,
    new DateRange(new DateOnly(2025, 5, 1), new DateOnly(2025, 5, 31)));

var payRunInputs = new PayrollPayRunInputs
{
    PayRunId = 101,                                           // Unique identifier for the pay run
    PayRunDetails = payRunDetails,
    EmployeePayRunInputs = [ employeePayRunInputs ]
};

// ##### Step 3 - get a reference data provider, then get a pay run processor and run the pay run #####
var helper = new ReferenceDataHelper(ReferenceDataResources);
var provider = await helper.CreateProviderAsync();
var factory = new PayRunProcessorFactory(provider);

var processor = factory.GetProcessor(payRunDetails);

processor.Process<int>(payRunInputs, true, out var payRunResult);

foreach (var er in payRunResult.EmployeePayRunOutputs)
{
    Console.WriteLine($"Employee #{er.EmployeeId}");
    Console.WriteLine($"   Gross pay: {er.TotalGrossPay:c}");
    Console.WriteLine($"   Income tax: {er.TaxCalculationResult.FinalTaxDue:c}");
    Console.WriteLine($"   Employees NI: {er.NiCalculationResult.EmployeeContribution:c} (Employers NI: {er.NiCalculationResult.EmployerContribution:c})");
    Console.WriteLine($"   Student loan repayments: {er.StudentLoanCalculationResult?.TotalDeduction ?? 0.00m:c}");
    Console.WriteLine($"   Employee pension contribution: {er.PensionContributionCalculationResult?.CalculatedEmployeeContributionAmount ?? 0.00m:c}" +
        $" (Employer contribution: {er.PensionContributionCalculationResult?.CalculatedEmployerContributionAmount ?? 0.00m:c})");
    Console.WriteLine($"   Net pay: {er.NetPay:c}");
}

// #### Step 4 - once finalised, remember to apply the pay run information to the employment history ####