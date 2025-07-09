// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.StudentLoans;

/// <summary>
/// Interface that represents factories that can generate <see cref="IStudentLoanCalculator"/> implementations.
/// </summary>
public interface IStudentLoanCalculatorFactory
{
    /// <summary>
    /// Gets a new <see cref="IStudentLoanCalculator"/> based on the specified pay date and number of tax periods.  The pay date
    /// is provided in order to determine which set of levels to use, noting that these may (but rarely do) change in-year.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>A new calculator instance.</returns>
    /// <remarks>The supplied PayDate is also used to calculate the appropriate period threshold to apply, from the PayFrequency
    /// property, e.g., weekly, monthly, etc.</remarks>
    IStudentLoanCalculator GetCalculator(PayDate payDate);
}