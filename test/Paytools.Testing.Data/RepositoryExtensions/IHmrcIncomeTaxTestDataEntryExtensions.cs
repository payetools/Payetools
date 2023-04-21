// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using Paytools.Testing.Data.IncomeTax;

namespace Paytools.Testing.Data;

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