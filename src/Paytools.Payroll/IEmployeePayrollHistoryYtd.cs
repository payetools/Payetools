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

using Paytools.NationalInsurance;

namespace Paytools.Payroll;

/// <summary>
/// Interface for types that represent the historical set of information for an employee's payroll for the
/// current tax year.
/// </summary>
public interface IEmployeePayrollHistoryYtd
{
    /// <summary>
    /// Gets any statutory maternity pay paid to date this tax year.
    /// </summary>
    decimal StatutoryMaternityPayYtd { get; }

    /// <summary>
    /// Gets any statutory paternity pay paid to date this tax year.
    /// </summary>
    decimal StatutoryPaternityPayYtd { get; }

    /// <summary>
    /// Gets any statutory adoption pay paid to date this tax year.
    /// </summary>
    decimal StatutoryAdoptionPayYtd { get; }

    /// <summary>
    /// Gets any statutory parental pay paid to date this tax year.
    /// </summary>
    decimal SharedParentalPayYtd { get; }

    /// <summary>
    /// Gets any statutory parental bereavement pay paid to date this tax year.
    /// </summary>
    decimal StatutoryParentalBereavementPayYtd { get; }

    /// <summary>
    /// Gets the National Insurance payment history for the current tax year.  Employees may
    /// transition between NI categories during the tax year and each NI category's payment
    /// record must be retained.
    /// </summary>
    List<EmployeeNiHistoryEntry> EmployeeNiHistoryEntries { get; }

    /// <summary>
    /// Gets the gross pay paid to date this tax year.
    /// </summary>
    decimal GrossPayYtd { get; }

    /// <summary>
    /// Gets the taxable pay paid to date this tax year.
    /// </summary>
    decimal TaxablePayYtd { get; }

    /// <summary>
    /// Gets the income tax paid to date this tax year.
    /// </summary>
    decimal TaxPaidYtd { get; }

    /// <summary>
    /// Gets the student loan deductions made to date this tax year.
    /// </summary>
    decimal StudentLoanRepaymentsYtd { get; }

    /// <summary>
    /// Gets the graduate loan deductions made to date this tax year.
    /// </summary>
    decimal GraduateLoanRepaymentsYtd { get; }

    /// <summary>
    /// Gets the amount accrued against payrolled benefits to date this tax year.
    /// </summary>
    decimal PayrolledBenefitsYtd { get; }

    /// <summary>
    /// Gets the total employee pension contributions made under a net pay arrangement to date this tax year.
    /// </summary>
    decimal EmployeePensionContributionsUnderNpaYtd { get; }

    /// <summary>
    /// Gets the total employee pension contributions made under relief at source to date this tax year.
    /// </summary>
    decimal EmployeePensionContributionsUnderRasYtd { get; }

    /// <summary>
    /// Gets the total employer pension contributions made to date this tax year.
    /// </summary>
    decimal EmployerPensionContributionsYtd { get; }
}