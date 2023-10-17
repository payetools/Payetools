// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

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

    public static void RunToFullStringTest(string input, string expectedOutput, bool expectedIsNonCumulative)
    {
        TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode).Should().BeTrue();
        taxCode.ToString(true, true).Should().Be(expectedOutput);
        taxCode.IsNonCumulative.Should().Be(expectedIsNonCumulative);

        TaxCode.TryParse(input.ToUpper(), _testTaxYear, out taxCode).Should().BeTrue();
        taxCode.ToString(true, true).Should().Be(expectedOutput);
        taxCode.IsNonCumulative.Should().Be(expectedIsNonCumulative);
    }
}
