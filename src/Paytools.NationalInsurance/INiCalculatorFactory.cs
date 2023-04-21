// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;

namespace Paytools.NationalInsurance;

/// <summary>
/// Interface that represents factories that can generate <see cref="INiCalculator"/> implementations.
/// </summary>
public interface INiCalculatorFactory
{
    /// <summary>
    /// Gets an instance of an <see cref="INiCalculator"/> for the specified pay date.
    /// </summary>
    /// <param name="payDate">Pay date.</param>
    /// <param name="numberOfTaxPeriods">Number of tax periods be applied by calculator.  Defaults to 1.</param>
    /// <returns>Instance of <see cref="INiCalculator"/> for the specified tax regime and pay date.</returns>
    INiCalculator GetCalculator(PayDate payDate, int numberOfTaxPeriods = 1);
}