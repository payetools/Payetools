using Paytools.Common;
using Paytools.Common.Diagnostics;
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

        Assert.Throws<InconsistentDataException>(action);
    }
}