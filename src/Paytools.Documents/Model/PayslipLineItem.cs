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

namespace Paytools.Documents.Model;

/// <summary>
/// Represents a line item on a payslip.
/// </summary>
public record PayslipLineItem : IPayslipLineItem
{
    /// <summary>
    /// Gets the descriptive text for this line item.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the optional quantity for this line item.  Null if not applicable.
    /// </summary>
    public decimal? Quantity { get; }

    /// <summary>
    /// Gets the optional units for this line item.  Null if not applicable.
    /// </summary>
    public string? Units { get; }

    /// <summary>
    /// Gets the optional rate for this line item.  Null if not applicable.
    /// </summary>
    public decimal? Rate { get; }

    /// <summary>
    /// Gets the amount for this payslip period.
    /// </summary>
    public decimal AmountForPeriod { get; }

    /// <summary>
    /// Gets the amount for the tax year to date.
    /// </summary>
    public decimal AmountYtd { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="PayslipLineItem"/>.
    /// </summary>
    /// <param name="description">Description of this line item.</param>
    /// <param name="quantity">Optional quantity; null if not applicable.</param>
    /// <param name="units">Optional units; null if not applicable.</param>
    /// <param name="rate">Optional rate; null if not applicable.</param>
    /// <param name="amountForPeriod">Amount for the period of this payslip.</param>
    /// <param name="amountYtd">Amount for the tax year to date.</param>
    public PayslipLineItem(string description, decimal? quantity, string? units, decimal? rate, decimal amountForPeriod, decimal amountYtd)
    {
        Description = description;
        Quantity = quantity;
        Units = units;
        Rate = rate;
        AmountForPeriod = amountForPeriod;
        AmountYtd = amountYtd;
    }

    /// <summary>
    /// Initialises a new <see cref="PayslipLineItem"/> from the supplied earnings entry and its respective YTD history.
    /// </summary>
    /// <param name="earnings">Earnings to initialise this instance with.</param>
    /// <param name="historyYtd">Employeee's earnings history for the tax year to date, including this payrun.</param>
    public PayslipLineItem(in IEarningsEntry earnings, in IEmployeePayrollHistoryYtd historyYtd)
    {
        var earningsType = earnings.EarningsDetails;

        Description = earningsType.Name;
        Quantity = earnings.QuantityInUnits;
        Units = earningsType.Units?.ToString();
        Rate = earnings.ValuePerUnit;
        AmountForPeriod = earnings.TotalEarnings;
        AmountYtd = historyYtd.EarningsHistoryYtd.Earnings
            .Where(e => e.EarningsDetails == earningsType)
            .Select(e => e.TotalEarnings)
            ?.Sum() ?? earnings.TotalEarnings;
    }

    /// <summary>
    /// Initialises a new <see cref="PayslipLineItem"/> from the supplied deduction entry and its respective YTD history.
    /// </summary>
    /// <param name="deduction">Deduction to initialise this instance with.</param>
    /// <param name="historyYtd">Employeee's deductions history for the tax year to date, including this payrun.</param>
    public PayslipLineItem(in IDeductionEntry deduction, in IEmployeePayrollHistoryYtd historyYtd)
    {
        var deductionType = deduction.DeductionClassification;

        Description = deductionType.Name;
        Quantity = deduction.QuantityInUnits;
        Units = deductionType.Units?.ToString();
        Rate = deduction.ValuePerUnit;
        AmountForPeriod = deduction.TotalDeduction;
        AmountYtd = historyYtd.DeductionHistoryYtd.Deductions
            .Where(e => e.DeductionClassification == deductionType)
            .Select(e => e.TotalDeduction)
            ?.Sum() ?? deduction.TotalDeduction;
    }
}
