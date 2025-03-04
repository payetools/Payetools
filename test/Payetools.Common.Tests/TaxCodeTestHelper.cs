// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Shouldly;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

public static class TaxCodeTestHelper
{
    private static readonly TaxYear _testTaxYear = new(TaxYearEnding.Apr5_2022);

    public static void RunFixedCodeTest(string input, TaxTreatment expectedTreatment)
    {
        var result = TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode);

        result.ShouldBeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.ShouldBe(expectedTreatment);

        result = TaxCode.TryParse(input.ToUpper(), out taxCode);

        result.ShouldBeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.ShouldBe(expectedTreatment);
    }

    public static void RunInvalidCodeTest(string input)
    {
        var result = TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode);

        result.ShouldBeFalse();
        result.ShouldHaveDefaultValue();

        result = TaxCode.TryParse(input.ToUpper(), out taxCode);

        result.ShouldBeFalse();
        result.ShouldHaveDefaultValue();
    }

    public static void RunValidNonCumulativeCodeTest(string input, TaxTreatment expectedTreatment)
    {
        var result = TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode);

        result.ShouldBeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.IsNonCumulative.ShouldBeTrue();

        result = TaxCode.TryParse(input.ToUpper(), out taxCode);

        result.ShouldBeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.IsNonCumulative.ShouldBeTrue();
        taxCode.TaxTreatment.ShouldBe(expectedTreatment);
    }

    public static void RunFixedCodeCountrySpecificTest(string input,
        TaxYear taxYear,
        TaxTreatment expectedTreatment,
        CountriesForTaxPurposes expectedCountries)
    {
        var result = TaxCode.TryParse(input.ToLower(), taxYear, out var taxCode);

        result.ShouldBeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.ShouldBe(expectedTreatment);
        taxCode.ApplicableCountries.ShouldBe(expectedCountries);

        result = TaxCode.TryParse(input.ToUpper(), taxYear, out taxCode);

        result.ShouldBeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.ShouldBe(expectedTreatment);
        taxCode.ApplicableCountries.ShouldBe(expectedCountries);
    }

    public static void RunStandardCodeTest(string input,
        TaxYear taxYear,
        TaxTreatment expectedTreatment,
        int expectedAllowance,
        CountriesForTaxPurposes expectedCountries)
    {
        var result = TaxCode.TryParse(input.ToLower(), taxYear, out var taxCode);

        result.ShouldBeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.ShouldBe(expectedTreatment);
        taxCode.ApplicableCountries.ShouldBe(expectedCountries);
        taxCode.NotionalAllowance.ShouldBe(expectedAllowance);

        result = TaxCode.TryParse(input.ToUpper(), taxYear, out taxCode);

        result.ShouldBeTrue();
        result.ShouldNotHaveDefaultValue();
        taxCode.TaxTreatment.ShouldBe(expectedTreatment);
        taxCode.ApplicableCountries.ShouldBe(expectedCountries);
        taxCode.NotionalAllowance.ShouldBe(expectedAllowance);
    }

    public static void RunToStringTest(string input, string expectedOutput, bool expectedIsNonCumulative)
    {
        TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode).ShouldBeTrue();
        taxCode.ToString().ShouldBe(expectedOutput);
        taxCode.IsNonCumulative.ShouldBe(expectedIsNonCumulative);

        TaxCode.TryParse(input.ToUpper(), _testTaxYear, out taxCode).ShouldBeTrue();
        taxCode.ToString().ShouldBe(expectedOutput);
        taxCode.IsNonCumulative.ShouldBe(expectedIsNonCumulative);
    }

    public static void RunToFullStringTest(string input, string expectedOutput, bool expectedIsNonCumulative)
    {
        TaxCode.TryParse(input.ToLower(), _testTaxYear, out var taxCode).ShouldBeTrue();
        taxCode.ToString(true, true).ShouldBe(expectedOutput);
        taxCode.IsNonCumulative.ShouldBe(expectedIsNonCumulative);

        TaxCode.TryParse(input.ToUpper(), _testTaxYear, out taxCode).ShouldBeTrue();
        taxCode.ToString(true, true).ShouldBe(expectedOutput);
        taxCode.IsNonCumulative.ShouldBe(expectedIsNonCumulative);
    }
}
