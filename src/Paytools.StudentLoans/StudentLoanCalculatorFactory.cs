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

using Paytools.Common.Model;
using Paytools.StudentLoans.ReferenceData;

namespace Paytools.StudentLoans;

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
