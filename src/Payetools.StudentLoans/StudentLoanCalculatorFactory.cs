// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.StudentLoans.ReferenceData;

namespace Payetools.StudentLoans;

/// <summary>
/// Factory to generate <see cref="IStudentLoanCalculator"/> implementations that are for a given pay date.
/// </summary>
public class StudentLoanCalculatorFactory : IStudentLoanCalculatorFactory
{
    private readonly IStudentLoanReferenceDataProvider _referenceDataProvider;

    /// <summary>
    /// Initialises a new instance of <see cref="StudentLoanCalculator"/> using the supplied reference data provider.
    /// </summary>
    /// <param name="referenceDataProvider">Reference data provider that provides access to HMRC-published
    /// thresholds and rates for student loan deductions.</param>
    public StudentLoanCalculatorFactory(IStudentLoanReferenceDataProvider referenceDataProvider)
    {
        _referenceDataProvider = referenceDataProvider;
    }

    /// <summary>
    /// Gets a new <see cref="IStudentLoanCalculator"/> based on the specified pay date and number of tax periods.  The pay date
    /// is provided in order to determine which set of levels to use, noting that these may (but rarely do) change in-year.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>A new calculator instance.</returns>
    public IStudentLoanCalculator GetCalculator(PayDate payDate)
    {
        var thresholds = AdjustThresholds(_referenceDataProvider.GetStudentLoanThresholdsForTaxYearAndPeriod(payDate.TaxYear,
            payDate.PayFrequency, payDate.TaxPeriod), payDate.PayFrequency.GetStandardTaxPeriodCount());
        var rates = _referenceDataProvider.GetStudentLoanRatesForTaxYearAndPeriod(payDate.TaxYear,
            payDate.PayFrequency, payDate.TaxPeriod);

        return new StudentLoanCalculator(thresholds, rates);
    }

    private static IStudentLoanThresholdSet AdjustThresholds(IStudentLoanThresholdSet thresholds, int payPeriodsInYear) =>
        new StudentLoanThresholdSet()
        {
            Plan1PerPeriodThreshold = decimal.Round(thresholds.Plan1PerPeriodThreshold / payPeriodsInYear, 2, MidpointRounding.ToZero),
            Plan2PerPeriodThreshold = decimal.Round(thresholds.Plan2PerPeriodThreshold / payPeriodsInYear, 2, MidpointRounding.ToZero),
            Plan4PerPeriodThreshold = decimal.Round(thresholds.Plan4PerPeriodThreshold / payPeriodsInYear, 2, MidpointRounding.ToZero),
            PostGradPerPeriodThreshold = decimal.Round(thresholds.PostGradPerPeriodThreshold / payPeriodsInYear, 2, MidpointRounding.ToZero)
        };
}