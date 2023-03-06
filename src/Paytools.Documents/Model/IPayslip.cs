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

using Paytools.Common.Model;
using Paytools.IncomeTax;
using Paytools.NationalInsurance;

namespace Paytools.Documents.Model;

/// <summary>
/// Interface that represents a payslip model.
/// </summary>
public interface IPayslip
{
    /// <summary>
    /// Gets the name of the applicable employer for this payslip.
    /// </summary>
    string EmployerName { get; }

    /// <summary>
    /// Gets the pay date for this payslip.
    /// </summary>
    PayDate PayDate { get; }

    /// <summary>
    /// Gets the pay period for this payslip.
    /// </summary>
    PayReferencePeriod PayPeriod { get; }

    /// <summary>
    /// Gets the employee's name and title.
    /// </summary>
    IPersonName Name { get; }

    /// <summary>
    /// Gets the employee's National Insurance number.
    /// </summary>
    NiNumber NiNumber { get; }

    /// <summary>
    /// Gets the employee's assigned National Insurance category letter.
    /// </summary>
    NiCategory NiCategory { get; }

    /// <summary>
    /// Gets the employee's payroll Id (aka works number).
    /// </summary>
    PayrollId PayrollId { get; }

    /// <summary>
    /// Gets the employee's postal address.
    /// </summary>
    PostalAddress PostalAddress { get; }

    /// <summary>
    /// Gets the employee's current tax code.
    /// </summary>
    TaxCode TaxCode { get; }

    /// <summary>
    /// Gets the gross earnings for this pay period.
    /// </summary>
    decimal GrossEarnings { get; }

    /// <summary>
    /// Gets the taxable earnings for this pay period.
    /// </summary>
    decimal TaxableEarnings { get; }

    /// <summary>
    /// Gets the net pay for this pay period.
    /// </summary>
    decimal NetPayment { get; }

    /// <summary>
    /// Gets the earnings items group.
    /// </summary>
    IPayslipItemGroup Earnings { get; }

    /// <summary>
    /// Gets the deductions items group.
    /// </summary>
    IPayslipItemGroup Deductions { get; }

    /// <summary>
    /// Gets the payrolled benefits items group.
    /// </summary>
    IPayslipItemGroup PayrolledBenefits { get; }

    /// <summary>
    /// Gets the pension items group.
    /// </summary>
    IPayslipItemGroup Pension { get; }

    /// <summary>
    /// Gets the employer items group.
    /// </summary>
    IPayslipItemGroup Employer { get; }
}