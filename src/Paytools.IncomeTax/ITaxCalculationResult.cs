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

namespace Paytools.IncomeTax;

/// <summary>
/// Interface that represents the result of an income tax calculation.  The purpose of this interface (and its implementations)
/// is to provide a richer set of data about the calculation than just the final tax due number.  This can be helpful in
/// employee support situations in particular.
/// </summary>
public interface ITaxCalculationResult
{
    /// <summary>
    /// Gets the calculator (implementation of <see cref="ITaxCalculator"/>) used to perform this calculation.
    /// </summary>
    ITaxCalculator Calculator { get; }

    /// <summary>
    /// Gets the <see cref="TaxCode"/> used in this calculation.
    /// </summary>
    TaxCode TaxCode { get; }

    /// <summary>
    /// Gets the taxable salary used in this calculation.  This is the gross salary less any tax-free pay (or plus any additional
    /// notional pay in the case of K tax codes).
    /// </summary>
    decimal TaxableSalaryAfterTaxFreePay { get; }

    /// <summary>
    /// Gets the tax-free pay applicable to the end of the period, as given by the specified tax code.  May be negative
    /// in the case of K tax codes.
    /// </summary>
    public decimal TaxFreePayToEndOfPeriod { get; }

    /// <summary>
    /// Gets the year to date taxable salary paid up to and including the end of the previous period.
    /// </summary>
    decimal PreviousPeriodSalaryYearToDate { get; }

    /// <summary>
    /// Gets the year to date tax paid up to and including the end of the previous period.
    /// </summary>
    decimal PreviousPeriodTaxPaidYearToDate { get; }

    /// <summary>
    /// Gets any unpaid tax due to the regulatory limit.
    /// TODO: implement this properly.
    /// </summary>
    decimal TaxUnpaidDueToRegulatoryLimit { get; }

    /// <summary>
    /// Gets the tax due at the end of the period, based on the taxable earnings to the end of the period and
    /// accounting for any tax-free pay up to the end of the period.
    /// </summary>
    decimal TaxDue { get; }

    /// <summary>
    /// Gets the numeric index of the highest tax band used in the calculation.
    /// </summary>
    int HighestApplicableTaxBandIndex { get; }

    /// <summary>
    /// Gets the total income to date that falls within the highest tax band used in the calculation.
    /// </summary>
    decimal IncomeAtHighestApplicableBand { get; }

    /// <summary>
    /// Gets the total tax due for income to date that falls within the highest tax band used in the calculation.
    /// </summary>
    decimal TaxAtHighestApplicableBand { get; }
}
