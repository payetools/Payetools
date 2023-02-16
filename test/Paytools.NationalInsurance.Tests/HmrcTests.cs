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
using Paytools.Common.Model;
using Paytools.NationalInsurance.ReferenceData;
using Paytools.ReferenceData;
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

            var calculator = new NiCalculator(provider);

            var result1 = calculator.Calculate(test.GrossPay, NiCategory.A);

            test.EmployeeNiContribution.Should().Be(result1.EmployeeContribution, "input is {0} and output is {1}", test.ToDebugString(), result1.ToString());
            test.EmployerNiContribution.Should().Be(result1.EmployerContribution, "input is {0} and output is {1}", test.ToDebugString(), result1.ToString());

            testsCompleted++;
        }

        Output.WriteLine($"{testsCompleted} tests completed");
    }

    private INiReferenceDataProvider GetProvider(List<NiThresholdEntry> thresholds,
        NiCategoryRateSet ratesByCategory)
    {
        Dictionary<NiCategory, NiPeriodThresholdSet> thresholdsDict = new();

        // F
        var thresholdSet = new NiPeriodThresholdSet(thresholds, PayFrequency.Monthly);

        thresholdsDict.Add(NiCategory.A, thresholdSet);

        return new NiReferenceDataProvider(thresholdsDict, ratesByCategory);
    }

    private List<NiThresholdEntry> GetThresholdList(int taxPeriod)
    {
        var thresholdList = new List<NiThresholdEntry>()
        {
            new() { Threshold = NiThreshold.LEL, ThresholdValuePerYear = 6396.0m },
            new() { Threshold = NiThreshold.PT, ThresholdValuePerYear = taxPeriod < 4 ? 9880.0m : 12570.0m },
            new() { Threshold = NiThreshold.ST, ThresholdValuePerYear = 9100.0m },
            new() { Threshold = NiThreshold.FUST, ThresholdValuePerYear = 25000.0m },
            new() { Threshold = NiThreshold.UST, ThresholdValuePerYear = 50270.0m },
            new() { Threshold = NiThreshold.AUST, ThresholdValuePerYear = 50270.0m },
            new() { Threshold = NiThreshold.VUST, ThresholdValuePerYear = 50270.0m },
            new() { Threshold = NiThreshold.UEL, ThresholdValuePerYear = 50270.0m }
        };

        return thresholdList;
    }

    private NiCategoryRateSet GetCategoryRateSet(int taxPeriod)
    {
        NiCategoryRateSet ratesByCategory = new();

        ratesByCategory.Add(NiCategory.A, new NiCategoryRateEntry()
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
