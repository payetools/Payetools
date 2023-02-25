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
using System.Net.Mail;

namespace Paytools.Employment.Model;

/// <summary>
/// Interface that represents an active employee for payroll purposes.
/// </summary>
public interface IEmployee : IEmployableIndividual
{
    /// <summary>
    /// Gets or sets the employee's email address, if known.
    /// </summary>
    public MailAddress? EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the employee's tax code.
    /// </summary>
    public TaxCode TaxCode { get; set; }

    /// <summary>
    /// Gets or sets the employee's postal address.
    /// </summary>
    public UkPostalAddress PostalAddress { get; set; }

    /// <summary>
    /// Gets or sets the employee's official employment start date.
    /// </summary>
    public DateOnly EmploymentStartDate { get; set; }

    /// <summary>
    /// Gets or sets the employee's official employment termination date, i.e., their last working
    /// day.  Null if the employee is still employed.
    /// </summary>
    public DateOnly? EmploymentEndDate { get; set; }

    /// <summary>
    /// Gets or sets the employee's payroll ID, as reported to HMRC.  Sometimes known as "works number".
    /// </summary>
    public PayrollId PayrollId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the employee is a company director.
    /// </summary>
    public bool IsDirector { get; set; }

    /// <summary>
    /// Gets or sets the method for calculating National Insurance contributions.  Applicable only
    /// for directors; null otherwise.
    /// </summary>
    public DirectorsNiCalculationMethod? DirectorsNiCalculationMethod { get; set; }

    /// <summary>
    /// Gets or sets the employee's current student loan status.
    /// </summary>
    public StudentLoanStatus? StudentLoanStatus { get; set; }
}
