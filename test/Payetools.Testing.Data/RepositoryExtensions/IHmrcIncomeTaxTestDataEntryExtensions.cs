// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.Testing.Data.IncomeTax;

namespace Payetools.Testing.Data;

public static class IHmrcIncomeTaxTestDataEntryExtensions
{
    public static TaxCode GetFullTaxCode(this IHmrcIncomeTaxTestDataEntry value, TaxYear taxYear)
    {
        var nonCumulative = value.W1M1Flag ? " X" : string.Empty;
        var input = $"{value.TaxCode}{nonCumulative}";

        if (!TaxCode.TryParse(input, taxYear, out var taxCode))
            throw new ArgumentException($"Unrecognised tax code '{input}'", nameof(value));

        return taxCode;
    }
}