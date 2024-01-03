// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using Payetools.Testing.Data.IncomeTax;

namespace Payetools.Testing.Data;

public static class IHmrcIncomeTaxTestDataEntryExtensions
{
    public static TaxCode GetFullTaxCode(this IHmrcIncomeTaxTestDataEntry value, TaxYear taxYear)
    {
        var nonCumulative = value.W1M1Flag?.ToLowerInvariant() == "wm1" ? " X" : string.Empty;
        var input = $"{value.TaxCode}{nonCumulative}";

        if (!TaxCode.TryParse(input, taxYear, out var taxCode))
            throw new ArgumentException($"Unrecognised tax code '{input}'", nameof(value));

        return taxCode;
    }
}