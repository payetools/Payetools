// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.IncomeTax.Model;

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
    /// accounting for any tax-free pay up to the end of the period.  This figure takes account of both the
    /// effect of the regulatory limit and the effect of any unpaid taxes due to the effect of the regulatory
    /// limit in previous periods.
    /// </summary>
    decimal FinalTaxDue { get; }

    /// <summary>
    /// Gets the tax due at the end of the period, based on the taxable earnings to the end of the period and
    /// accounting for any tax-free pay up to the end of the period.  This is before considering the effect of
    /// regulatory limits.
    /// </summary>
    decimal TaxDueBeforeApplicationOfRegulatoryLimit { get; }

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
