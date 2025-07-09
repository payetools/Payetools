// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Shouldly;

namespace Payetools.Common.Tests;

public class CountriesForTaxPurposesConverterTests
{
    [Fact]
    public void TestConvertToEnum()
    {
        var input = "GB-ENG";
        CountriesForTaxPurposesConverter.ToEnum(input).ShouldBe(CountriesForTaxPurposes.England);

        input += " GB-NIR";
        CountriesForTaxPurposesConverter.ToEnum(input).ShouldBe(CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland);

        input += " GB-WLS";
        CountriesForTaxPurposesConverter.ToEnum(input).ShouldBe(CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland | CountriesForTaxPurposes.Wales);

        input = "GB-WLS GB-NIR GB-ENG";
        CountriesForTaxPurposesConverter.ToEnum(input).ShouldBe(CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland | CountriesForTaxPurposes.Wales);

        input = "GB-SCT";
        CountriesForTaxPurposesConverter.ToEnum(input).ShouldBe(CountriesForTaxPurposes.Scotland);

        input = "GB-WLS";
        CountriesForTaxPurposesConverter.ToEnum(input).ShouldBe(CountriesForTaxPurposes.Wales);

        input = "GB-XYZ";
        Action action = () => CountriesForTaxPurposesConverter.ToEnum(input);
        action.ShouldThrow<ArgumentException>()
            .Message.ShouldBe("Unrecognised country 'GB-XYZ' (Parameter 'iso3166Countries')");
    }

    [Fact]
    public void TestConvertToISO3166String()
    {
        var countries = CountriesForTaxPurposes.England;
        CountriesForTaxPurposesConverter.ToString(countries).ShouldBe("GB-ENG");

        countries |= CountriesForTaxPurposes.NorthernIreland;
        CountriesForTaxPurposesConverter.ToString(countries).ShouldBe("GB-ENG GB-NIR");

        countries |= CountriesForTaxPurposes.Wales;
        CountriesForTaxPurposesConverter.ToString(countries).ShouldBe("GB-ENG GB-NIR GB-WLS");

        countries = CountriesForTaxPurposes.Scotland;
        CountriesForTaxPurposesConverter.ToString(countries).ShouldBe("GB-SCT");

        countries = CountriesForTaxPurposes.Wales;
        CountriesForTaxPurposesConverter.ToString(countries).ShouldBe("GB-WLS");
    }
}