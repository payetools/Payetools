// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.IncomeTax;

/// <summary>
/// Interface that represents factories that can generate <see cref="ITaxCalculator"/> implementations.
/// </summary>
public interface ITaxCalculatorFactory
{
    /// <summary>
    /// Gets an instance of an <see cref="ITaxCalculator"/> for the specified tax regime and pay date.
    /// </summary>
    /// <param name="applicableCountries">Applicable tax regime.</param>
    /// <param name="payDate">Pay date.</param>
    /// <returns>Instance of <see cref="ITaxCalculator"/> for the specified tax regime and pay date.</returns>
    ITaxCalculator GetCalculator(CountriesForTaxPurposes applicableCountries, PayDate payDate);
}
