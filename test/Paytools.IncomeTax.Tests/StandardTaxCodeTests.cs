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

using Paytools.Common;
using Paytools.Common.Model;
using Test = Paytools.IncomeTax.Tests.TaxCodeTestHelper;

namespace Paytools.IncomeTax.Tests;

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

        Assert.False(result);
    }
}