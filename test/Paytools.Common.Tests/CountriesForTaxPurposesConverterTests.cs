// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
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
using Paytools.Common.Model;

namespace Paytools.Common.Tests;

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