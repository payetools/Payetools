// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
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
using Paytools.NationalInsurance.ReferenceData;
using Paytools.ReferenceData.NationalInsurance;
using Paytools.Testing.Data;
using Xunit.Abstractions;

namespace Paytools.NationalInsurance.Tests;

public class HmrcTests
{
    private readonly ITestOutputHelper Output;

    public HmrcTests(ITestOutputHelper output)
    {
        Output = output;
    }

    [Fact]
    public void RunAllTests()
    {

        using var db = new TestDataRepository();

        var testData = db.GetTestData<IHmrcNiTestDataEntry>(TestSource.Hmrc, TestScope.NationalInsurance);

        int testsCompleted = 0;

        foreach (var test in testData.ToList().Where(t => t.NiCategory == NiCategory.A &&
            t.PayFrequency == PayFrequency.Monthly))
        {
            // test.Period is ignored for non-directors NI
            var provider = GetProvider(GetThresholdList(test.Period), GetCategoryRateSet(test.Period));

            var taxYear = new TaxYear(test.TaxYearEnding);

            var factory = new NiCalculatorFactory(provider);
            var calculator = factory.GetCalculator(taxYear, test.PayFrequency, test.Period);

            var result1 = calculator.Calculate(NiCategory.A, test.GrossPay);

            test.EmployeeNiContribution.Should().Be(result1.EmployeeContribution, "input is {0} and output is {1}", test.ToDebugString(), result1.ToString());
            test.EmployerNiContribution.Should().Be(result1.EmployerContribution, "input is {0} and output is {1}", test.ToDebugString(), result1.ToString());

            testsCompleted++;
        }

        Output.WriteLine($"{testsCompleted} tests completed");
    }

    private INiReferenceDataProvider GetProvider(List<INiThresholdEntry> thresholds,
        NiCategoryRateSet ratesByCategory)
    {
        return new NiReferenceDataProvider(new NiThresholdSet(thresholds), ratesByCategory);
    }

    private List<INiThresholdEntry> GetThresholdList(int taxPeriod)
    {
        var thresholdList = new List<INiThresholdEntry>();
        //{
        //    new NiThresholdEntry() { ThresholdType = NiThresholdType.LEL, ThresholdValuePerYear = 6396.0m },
        //    new NiThresholdEntry() { ThresholdType = NiThresholdType.PT, ThresholdValuePerYear = taxPeriod < 4 ? 9880.0m : 12570.0m },
        //    new NiThresholdEntry() { ThresholdType = NiThresholdType.ST, ThresholdValuePerYear = 9100.0m },
        //    new NiThresholdEntry() { ThresholdType = NiThresholdType.FUST, ThresholdValuePerYear = 25000.0m },
        //    new NiThresholdEntry() { ThresholdType = NiThresholdType.UST, ThresholdValuePerYear = 50270.0m },
        //    new NiThresholdEntry() { ThresholdType = NiThresholdType.AUST, ThresholdValuePerYear = 50270.0m },
        //    new NiThresholdEntry() { ThresholdType = NiThresholdType.VUST, ThresholdValuePerYear = 50270.0m },
        //    new NiThresholdEntry() { ThresholdType = NiThresholdType.UEL, ThresholdValuePerYear = 50270.0m }
        //};

        return thresholdList;
    }

    private NiCategoryRateSet GetCategoryRateSet(int taxPeriod)
    {
        NiCategoryRateSet ratesByCategory = new();

        ratesByCategory.Add(NiCategory.A, new NiCategoryRatesEntry()
        {
            Category = NiCategory.A,
            EmployeeRateToPT = 0.0m,
            EmployeeRatePTToUEL = taxPeriod > 7 ? 0.12m : 0.1325m,
            EmployeeRateAboveUEL = taxPeriod > 7 ? 0.02m : 0.0325m,
            EmployerRateLELtoST = 0.0m,
            EmployerRateSTtoFUST = taxPeriod > 7 ? 0.138m : 0.1505m,
            EmployerRateFUSTtoUEL = taxPeriod > 7 ? 0.138m : 0.1505m,
            EmployerRateAboveUEL = taxPeriod > 7 ? 0.138m : 0.1505m
        });

        return ratesByCategory;
    }
}
