// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.ObjectModel;

namespace Payetools.StudentLoans.ReferenceData;

/// <summary>
/// Interface that classes implement in order to provide access to student loan reference data.
/// </summary>
public interface IStudentLoanReferenceDataProvider
{
    /// <summary>
    /// Gets the set of annual thresholds to be applied for a given tax year and tax period.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>An implementation of <see cref="IStudentLoanThresholdSet"/> that provides the appropriate set of annual
    /// thresholds for the specified point.</returns>
    IStudentLoanThresholdSet GetStudentLoanThresholdsForTaxYearAndPeriod(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod);

    /// <summary>
    /// Gets the student and post graduate deduction rates for the specified tax year and tax period, as denoted
    /// by the supplied pay frequency.
    /// and pay period.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>An instance of <see cref="IStudentLoanRateSet"/> containing the rates for the specified point
    /// in time.</returns>
    IStudentLoanRateSet GetStudentLoanRatesForTaxYearAndPeriod(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod);
}