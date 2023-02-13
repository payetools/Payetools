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

using FluentAssertions;
using Paytools.Common;
using Paytools.Common.Model;

namespace Paytools.IncomeTax.Tests;

public static class TaxCodeTestHelper
{
    private static readonly TaxYear _testTaxYear = new(TaxYearEnding.Apr5_2022);

    public static void RunFixedCodeTest(string input, TaxTreatment expectedTreatment)
    {
        var result = TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode);

        result.Should().BeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.Should().Be(expectedTreatment);

        result = TaxCode.TryParse(input.ToUpper(), out taxCode);

        result.Should().BeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.Should().Be(expectedTreatment);
    }

    public static void RunInvalidCodeTest(string input)
    {
        var result = TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode);
        
        result.Should().BeFalse();
        result.ShouldHaveDefaultValue();

        result = TaxCode.TryParse(input.ToUpper(), out taxCode);

        result.Should().BeFalse();
        result.ShouldHaveDefaultValue();
    }

    public static void RunValidNonCumulativeCodeTest(string input, TaxTreatment expectedTreatment)
    {
        var result = TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode);

        result.Should().BeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.IsNonCumulative.Should().BeTrue();

        result = TaxCode.TryParse(input.ToUpper(), out taxCode);

        result.Should().BeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.IsNonCumulative.Should().BeTrue();
        taxCode.TaxTreatment.Should().Be(expectedTreatment);
    }

    public static void RunFixedCodeCountrySpecificTest(string input,
        TaxYear taxYear,
        TaxTreatment expectedTreatment,
        CountriesForTaxPurposes expectedCountries)
    {
        var result = TaxCode.TryParse(input.ToLower(), taxYear, out var taxCode);

        result.Should().BeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.Should().Be(expectedTreatment);
        taxCode.ApplicableCountries.Should().Be(expectedCountries);

        result = TaxCode.TryParse(input.ToUpper(), taxYear, out taxCode);

        result.Should().BeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.Should().Be(expectedTreatment);
        taxCode.ApplicableCountries.Should().Be(expectedCountries);
    }

    public static void RunStandardCodeTest(string input,
        TaxYear taxYear,
        TaxTreatment expectedTreatment,
        int expectedAllowance,
        CountriesForTaxPurposes expectedCountries)
    {
        var result = TaxCode.TryParse(input.ToLower(), taxYear, out var taxCode);

        result.Should().BeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.Should().Be(expectedTreatment);
        taxCode.ApplicableCountries.Should().Be(expectedCountries);
        taxCode.NotionalAllowance.Should().Be(expectedAllowance);

        result = TaxCode.TryParse(input.ToUpper(), taxYear, out taxCode);

        result.Should().BeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.Should().Be(expectedTreatment);
        taxCode.ApplicableCountries.Should().Be(expectedCountries);
        taxCode.NotionalAllowance.Should().Be(expectedAllowance);
    }

    public static void RunToStringTest(string input, string expectedOutput, bool expectedIsNonCumulative)
    {
        TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode).Should().BeTrue();
        taxCode.ToString().Should().Be(expectedOutput);
        taxCode.IsNonCumulative.Should().Be(expectedIsNonCumulative);

        TaxCode.TryParse(input.ToUpper(), _testTaxYear, out taxCode).Should().BeTrue();
        taxCode.ToString().Should().Be(expectedOutput);
        taxCode.IsNonCumulative.Should().Be(expectedIsNonCumulative);
    }
}
