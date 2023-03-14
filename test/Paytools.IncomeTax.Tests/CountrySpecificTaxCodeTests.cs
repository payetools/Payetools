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
using Paytools.Common.Diagnostics;
using Paytools.Common.Model;
using Paytools.IncomeTax.Model;
using Test = Paytools.IncomeTax.Tests.TaxCodeTestHelper;

namespace Paytools.IncomeTax.Tests;

public class CountrySpecificTaxCodeTests
{
    [Fact]
    public void CheckD0CountrySpecificCodes()
    {
        Test.RunFixedCodeCountrySpecificTest("D0", new(TaxYearEnding.Apr5_2023), TaxTreatment.D0, CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland);
        Test.RunFixedCodeCountrySpecificTest("SD0", new(TaxYearEnding.Apr5_2023), TaxTreatment.D0, CountriesForTaxPurposes.Scotland);
        Test.RunFixedCodeCountrySpecificTest("CD0", new(TaxYearEnding.Apr5_2023), TaxTreatment.D0, CountriesForTaxPurposes.Wales);
        Test.RunFixedCodeCountrySpecificTest("D0", new(TaxYearEnding.Apr5_2022), TaxTreatment.D0, CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland);
        Test.RunFixedCodeCountrySpecificTest("SD0", new(TaxYearEnding.Apr5_2022), TaxTreatment.D0, CountriesForTaxPurposes.Scotland);
        Test.RunFixedCodeCountrySpecificTest("CD0", new(TaxYearEnding.Apr5_2022), TaxTreatment.D0, CountriesForTaxPurposes.Wales);
        Test.RunFixedCodeCountrySpecificTest("D0", new(TaxYearEnding.Apr5_2021), TaxTreatment.D0, CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland);
        Test.RunFixedCodeCountrySpecificTest("SD0", new(TaxYearEnding.Apr5_2022), TaxTreatment.D0, CountriesForTaxPurposes.Scotland);
        Test.RunFixedCodeCountrySpecificTest("CD0", new(TaxYearEnding.Apr5_2021), TaxTreatment.D0, CountriesForTaxPurposes.Wales);
        Test.RunFixedCodeCountrySpecificTest("D0", new(TaxYearEnding.Apr5_2020), TaxTreatment.D0, CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland);
        Test.RunFixedCodeCountrySpecificTest("SD0", new(TaxYearEnding.Apr5_2020), TaxTreatment.D0, CountriesForTaxPurposes.Scotland);
        Test.RunFixedCodeCountrySpecificTest("CD0", new(TaxYearEnding.Apr5_2020), TaxTreatment.D0, CountriesForTaxPurposes.Wales);
        Test.RunFixedCodeCountrySpecificTest("D0", new(TaxYearEnding.Apr5_2019), TaxTreatment.D0, CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland | CountriesForTaxPurposes.Wales);
        Test.RunFixedCodeCountrySpecificTest("SD0", new(TaxYearEnding.Apr5_2019), TaxTreatment.D0, CountriesForTaxPurposes.Scotland);
    }

    [Fact]
    public void CheckNTCountrySpecificCodes()
    {
        Test.RunFixedCodeCountrySpecificTest("NT", new(TaxYearEnding.Apr5_2023), TaxTreatment.NT, CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland | CountriesForTaxPurposes.Wales | CountriesForTaxPurposes.Scotland);
    }

    [Fact]
    public void CheckInvalidCountryForYear()
    {
        Action action = () => TaxCode.TryParse("C1257L", new(TaxYearEnding.Apr5_2019), out var taxCode);

        action.Should().Throw<InconsistentDataException>()
            .WithMessage("Country-specific tax code supplied but country not valid for tax year");
    }
}