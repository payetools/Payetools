// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Paytools.Common.Diagnostics;
using Paytools.Common.Model;
using Test = Paytools.Common.Tests.TaxCodeTestHelper;

namespace Paytools.Common.Tests;

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