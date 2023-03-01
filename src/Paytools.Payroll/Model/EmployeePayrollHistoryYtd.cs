// Copyright (c) 2023 Paytools Foundation.
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

using Paytools.Employment.Model;
using Paytools.NationalInsurance;

namespace Paytools.Payroll.Model;

/// <summary>
/// Represents the historical set of information for an employee's payroll for the
/// current tax year.
/// </summary>
public record EmployeePayrollHistoryYtd : IEmployeePayrollHistoryYtd
{
    /// <summary>
    /// Gets any statutory maternity pay paid to date this tax year.
    /// </summary>
    public decimal StatutoryMaternityPayYtd { get; init; }

    /// <summary>
    /// Gets any statutory paternity pay paid to date this tax year.
    /// </summary>
    public decimal StatutoryPaternityPayYtd { get; init; }

    /// <summary>
    /// Gets any statutory adoption pay paid to date this tax year.
    /// </summary>
    public decimal StatutoryAdoptionPayYtd { get; init; }

    /// <summary>
    /// Gets any statutory shared parental pay paid to date this tax year.
    /// </summary>
    public decimal SharedParentalPayYtd { get; init; }

    /// <summary>
    /// Gets any statutory parental bereavement pay paid to date this tax year.
    /// </summary>
    public decimal StatutoryParentalBereavementPayYtd { get; init; }

    /// <summary>
    /// Gets the National Insurance payment history for the current tax year.  Employees may
    /// transition between NI categories during the tax year and each NI category's payment
    /// record must be retained.
    /// </summary>
    public List<EmployeeNiHistoryEntry> EmployeeNiHistoryEntries { get; init; }

    /// <summary>
    /// Gets the gross pay paid to date this tax year.
    /// </summary>
    public decimal GrossPayYtd { get; init; }

    /// <summary>
    /// Gets the taxable pay paid to date this tax year.
    /// </summary>
    public decimal TaxablePayYtd { get; init; }

    /// <summary>
    /// Gets the income tax paid to date this tax year.
    /// </summary>
    public decimal TaxPaidYtd { get; init; }

    /// <summary>
    /// Gets the student loan deductions made to date this tax year.
    /// </summary>
    public decimal StudentLoanRepaymentsYtd { get; init; }

    /// <summary>
    /// Gets the graduate loan deductions made to date this tax year.
    /// </summary>
    public decimal GraduateLoanRepaymentsYtd { get; init; }

    /// <summary>
    /// Gets the amount accrued against payrolled benefits to date this tax year.
    /// </summary>
    public decimal PayrolledBenefitsYtd { get; init; }

    /// <summary>
    /// Gets the total employee pension contributions made under a net pay arrangement to date this tax year.
    /// </summary>
    public decimal EmployeePensionContributionsUnderNpaYtd { get; init; }

    /// <summary>
    /// Gets the total employee pension contributions made under relief at source to date this tax year.
    /// </summary>
    public decimal EmployeePensionContributionsUnderRasYtd { get; init; }

    /// <summary>
    /// Gets the total employer pension contributions made to date this tax year.
    /// </summary>
    public decimal EmployerPensionContributionsYtd { get; init; }

    /// <summary>
    /// Gets the tax that it has not been possible to collect so far this tax year due to the
    /// regulatory limit on income tax deductions.
    /// </summary>
    public decimal TaxUnpaidDueToRegulatoryLimit { get; init; }

    /// <summary>
    /// Initialises a new instance of <see cref="EmployeePayrollHistoryYtd"/>.
    /// </summary>
    public EmployeePayrollHistoryYtd()
    {
        EmployeeNiHistoryEntries = new List<EmployeeNiHistoryEntry>();
    }
}