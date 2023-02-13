// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Paytools.Common;
using Paytools.Common.Model;

namespace Paytools.IncomeTax.Tests;

public static class TaxCodeTestHelper
{
    private static readonly TaxYear _testTaxYear = new(TaxYearEnding.Apr5_2022);

    public static void RunFixedCodeTest(string input, TaxTreatment expectedTreatment)
    {
        var result = TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode);

        Assert.True(result);
        Assert.NotNull(taxCode);
        Assert.Equal(expectedTreatment, taxCode?.TaxTreatment);

        result = TaxCode.TryParse(input.ToUpper(), out taxCode);

        Assert.True(result);
        Assert.NotNull(taxCode);
        Assert.Equal(expectedTreatment, taxCode?.TaxTreatment);
    }

    public static void RunInvalidCodeTest(string input)
    {
        var result = TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode);

        Assert.False(result);
        Assert.Null(taxCode);

        result = TaxCode.TryParse(input.ToUpper(), out taxCode);

        Assert.False(result);
        Assert.Null(taxCode);
    }

    public static void RunValidNonCumulativeCodeTest(string input, TaxTreatment expectedTreatment)
    {
        var result = TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode);

        Assert.True(result);
        Assert.NotNull(taxCode);
        Assert.True(taxCode?.IsNonCumulative);

        result = TaxCode.TryParse(input.ToUpper(), out taxCode);

        Assert.True(result);
        Assert.NotNull(taxCode);
        Assert.True(taxCode?.IsNonCumulative);
        Assert.Equal(expectedTreatment, taxCode?.TaxTreatment);
    }

    public static void RunFixedCodeCountrySpecificTest(string input,
        TaxYear taxYear,
        TaxTreatment expectedTreatment,
        CountriesForTaxPurposes expectedCountries)
    {
        var result = TaxCode.TryParse(input.ToLower(), taxYear, out var taxCode);

        Assert.True(result);
        Assert.NotNull(taxCode);
        Assert.Equal(expectedTreatment, taxCode?.TaxTreatment);
        Assert.Equal(expectedCountries, taxCode?.ApplicableCountries);

        result = TaxCode.TryParse(input.ToUpper(), taxYear, out taxCode);

        Assert.True(result);
        Assert.NotNull(taxCode);
        Assert.Equal(expectedTreatment, taxCode?.TaxTreatment);
        Assert.Equal(expectedCountries, taxCode?.ApplicableCountries);
    }

    public static void RunStandardCodeTest(string input,
        TaxYear taxYear,
        TaxTreatment expectedTreatment,
        int expectedAllowance,
        CountriesForTaxPurposes expectedCountries)
    {
        var result = TaxCode.TryParse(input.ToLower(), taxYear, out var taxCode);

        Assert.True(result);
        Assert.NotNull(taxCode);
        Assert.Equal(expectedTreatment, taxCode?.TaxTreatment);
        Assert.Equal(expectedCountries, taxCode?.ApplicableCountries);
        Assert.Equal(expectedAllowance, taxCode?.NotionalAllowance);

        result = TaxCode.TryParse(input.ToUpper(), taxYear, out taxCode);

        Assert.True(result);
        Assert.NotNull(taxCode);
        Assert.Equal(expectedTreatment, taxCode?.TaxTreatment);
        Assert.Equal(expectedCountries, taxCode?.ApplicableCountries);
        Assert.Equal(expectedAllowance, taxCode?.NotionalAllowance);
    }

    public static void RunToStringTest(string input, string expectedOutput, bool expectedIsNonCumulative)
    {
        Assert.True(TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode));
        Assert.Equal(expectedOutput, taxCode?.ToString());
        Assert.Equal(expectedIsNonCumulative, taxCode?.IsNonCumulative);

        Assert.True(TaxCode.TryParse(input.ToUpper(), _testTaxYear, out taxCode));
        Assert.Equal(expectedOutput, taxCode?.ToString());
        Assert.Equal(expectedIsNonCumulative, taxCode?.IsNonCumulative);
    }
}
