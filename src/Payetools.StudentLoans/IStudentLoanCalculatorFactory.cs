// Copyright (c) 2023 Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
