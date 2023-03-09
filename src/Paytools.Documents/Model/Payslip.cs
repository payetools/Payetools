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
/// Represents a payslip data model.
/// </summary>
public class Payslip : IPayslip
{
    /// <inheritdoc/>
    public string EmployerName { get; set; } = default!;

    /// <inheritdoc/>
    public PayDate PayDate { get; private set; }

    /// <inheritdoc/>
    public PayReferencePeriod PayPeriod { get; private set; }

    /// <inheritdoc/>
    public IPersonName Name { get; private set; } = default!;

    /// <inheritdoc/>
    public NiNumber NiNumber { get; private set; }

    /// <inheritdoc/>
    public NiCategory NiCategory { get; private set; }

    /// <inheritdoc/>
    public PayrollId PayrollId { get; private set; } = default!;

    /// <inheritdoc/>
    public PostalAddress PostalAddress { get; private set; } = default!;

    /// <inheritdoc/>
    public TaxCode TaxCode { get; private set; }

    /// <inheritdoc/>
    public decimal GrossEarnings { get; private set; }

    /// <inheritdoc/>
    public decimal TaxableEarnings { get; private set; }

    /// <inheritdoc/>
    public decimal NetPayment { get; private set; }

    /// <inheritdoc/>
    public IPayslipItemGroup Earnings { get; private set; } = default!;

    /// <inheritdoc/>
    public IPayslipItemGroup Deductions { get; private set; } = default!;

    /// <inheritdoc/>
    public IPayslipItemGroup PayrolledBenefits { get; private set; } = default!;

    /// <inheritdoc/>
    public IPayslipItemGroup Pension { get; private set; } = default!;

    /// <inheritdoc/>
    public IPayslipItemGroup EmployerSpecificEntries { get; private set; } = default!;

    /// <summary>
    /// Sets the earnings line group for this payslip.
    /// </summary>
    /// <param name="title">Group title.</param>
    /// <param name="earningsLines">List of earnings lines.</param>
    public void SetEarnings(string title, List<IPayslipLineItem> earningsLines)
    {
        Earnings = new PayslipItemGroup(title, earningsLines);
    }
}
