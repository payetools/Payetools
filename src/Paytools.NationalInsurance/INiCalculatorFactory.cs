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
    /// <param name="payDate">Applicable pay date.</param>
    /// <param name="numberOfTaxPeriods">Number of tax periods applicable, usually 1.  Defaults to 1.</param>
    /// <param name="applyWeek53Treatment">Flag that indicates whether to apply "week 53" treatment, i.e., where
    /// there are 53 weeks in a tax year (or 27 periods in a two-weekly pay cycle, etc.).  Must be false
    /// for monthly, quarterly and annual payrolls.  Optional, defaulting to false.</param>
    /// <returns>Instance of <see cref="INiCalculator"/> for the specified tax regime and pay date.</returns>
    INiCalculator GetCalculator(PayDate payDate, int numberOfTaxPeriods = 1, bool applyWeek53Treatment = false);
}