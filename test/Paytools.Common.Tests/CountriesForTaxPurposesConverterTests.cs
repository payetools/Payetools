namespace Paytools.Common.Tests;

public class CountriesForTaxPurposesConverterTests
{
    [Fact]
    public void TestConvertToEnum()
    {
        var input = "GB-ENG";
        Assert.Equal(CountriesForTaxPurposes.England, CountriesForTaxPurposesConverter.ToEnum(input));

        input += " GB-NIR";
        Assert.Equal(CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland, CountriesForTaxPurposesConverter.ToEnum(input));

        input += " GB-WLS";
        Assert.Equal(CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland | CountriesForTaxPurposes.Wales, 
            CountriesForTaxPurposesConverter.ToEnum(input));

        input = "GB-WLS GB-NIR GB-ENG";
        Assert.Equal(CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland | CountriesForTaxPurposes.Wales,
            CountriesForTaxPurposesConverter.ToEnum(input));

        input = "GB-SCT";
        Assert.Equal(CountriesForTaxPurposes.Scotland, CountriesForTaxPurposesConverter.ToEnum(input));

        input = "GB-WLS";
        Assert.Equal(CountriesForTaxPurposes.Wales, CountriesForTaxPurposesConverter.ToEnum(input));
    }

    [Fact]
    public void TestConvertToISO3166String()
    {
        var countries = CountriesForTaxPurposes.England;
        Assert.Equal("GB-ENG", CountriesForTaxPurposesConverter.ToString(countries));

        countries |= CountriesForTaxPurposes.NorthernIreland;
        Assert.Equal("GB-ENG GB-NIR", CountriesForTaxPurposesConverter.ToString(countries));

        countries |= CountriesForTaxPurposes.Wales;
        Assert.Equal("GB-ENG GB-NIR GB-WLS", CountriesForTaxPurposesConverter.ToString(countries));

        countries = CountriesForTaxPurposes.Scotland;
        Assert.Equal("GB-SCT", CountriesForTaxPurposesConverter.ToString(countries));

        countries = CountriesForTaxPurposes.Wales;
        Assert.Equal("GB-WLS", CountriesForTaxPurposesConverter.ToString(countries));
    }
}