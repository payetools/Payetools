// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Paytools.Common.Model;
using Test = Paytools.Common.Tests.TaxCodeTestHelper;

namespace Paytools.Common.Tests;

public class StandardTaxCodeTests
{
    [Fact]
    public void Check1257LCode()
    {
        Test.RunStandardCodeTest("1257L", new(TaxYearEnding.Apr5_2023), TaxTreatment.L, 12570,
            CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland);
        Test.RunStandardCodeTest("S1257L", new(TaxYearEnding.Apr5_2023), TaxTreatment.L, 12570,
            CountriesForTaxPurposes.Scotland);
        Test.RunStandardCodeTest("C1257L", new(TaxYearEnding.Apr5_2023), TaxTreatment.L, 12570,
            CountriesForTaxPurposes.Wales);
    }

    [Fact]
    public void CheckKCodes()
    {
        Test.RunStandardCodeTest("K1052", new(TaxYearEnding.Apr5_2023), TaxTreatment.K, -10520,
            CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland);
        Test.RunStandardCodeTest("SK1234", new(TaxYearEnding.Apr5_2023), TaxTreatment.K, -12340,
            CountriesForTaxPurposes.Scotland);
        Test.RunStandardCodeTest("CK2345", new(TaxYearEnding.Apr5_2023), TaxTreatment.K, -23450,
            CountriesForTaxPurposes.Wales);
    }

    [Fact]
    public void CheckInvalidCode()
    {
        var result = TaxCode.TryParse("AB123C", new(TaxYearEnding.Apr5_2023), out var taxCode);

        taxCode.ShouldHaveDefaultValue();
        result.Should().BeFalse();
    }
}