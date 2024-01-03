// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

public class CountriesForTaxPurposesConverterTests
{
    [Fact]
    public void TestConvertToEnum()
    {
        var input = "GB-ENG";
        CountriesForTaxPurposesConverter.ToEnum(input).Should().Be(CountriesForTaxPurposes.England);

        input += " GB-NIR";
        CountriesForTaxPurposesConverter.ToEnum(input).Should().Be(CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland);

        input += " GB-WLS";
        CountriesForTaxPurposesConverter.ToEnum(input).Should().Be(CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland | CountriesForTaxPurposes.Wales);

        input = "GB-WLS GB-NIR GB-ENG";
        CountriesForTaxPurposesConverter.ToEnum(input).Should().Be(CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland | CountriesForTaxPurposes.Wales);

        input = "GB-SCT";
        CountriesForTaxPurposesConverter.ToEnum(input).Should().Be(CountriesForTaxPurposes.Scotland);

        input = "GB-WLS";
        CountriesForTaxPurposesConverter.ToEnum(input).Should().Be(CountriesForTaxPurposes.Wales);

        input = "GB-XYZ";
        Action action = () => CountriesForTaxPurposesConverter.ToEnum(input);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Unrecognised country 'GB-XYZ' (Parameter 'iso3166Countries')");
    }

    [Fact]
    public void TestConvertToISO3166String()
    {
        var countries = CountriesForTaxPurposes.England;
        CountriesForTaxPurposesConverter.ToString(countries).Should().Be("GB-ENG");

        countries |= CountriesForTaxPurposes.NorthernIreland;
        CountriesForTaxPurposesConverter.ToString(countries).Should().Be("GB-ENG GB-NIR");

        countries |= CountriesForTaxPurposes.Wales;
        CountriesForTaxPurposesConverter.ToString(countries).Should().Be("GB-ENG GB-NIR GB-WLS");

        countries = CountriesForTaxPurposes.Scotland;
        CountriesForTaxPurposesConverter.ToString(countries).Should().Be("GB-SCT");

        countries = CountriesForTaxPurposes.Wales;
        CountriesForTaxPurposesConverter.ToString(countries).Should().Be("GB-WLS");
    }
}