// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Diagnostics;
using Payetools.Common.Model;
using Test = Payetools.Common.Tests.TaxCodeTestHelper;

namespace Payetools.Common.Tests;

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