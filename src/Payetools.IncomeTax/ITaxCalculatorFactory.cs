// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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