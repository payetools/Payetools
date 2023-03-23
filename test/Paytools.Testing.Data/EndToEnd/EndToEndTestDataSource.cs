﻿// Copyright (c) 2023 Paytools Foundation.
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

using System;
using Xunit.Abstractions;

namespace Paytools.Testing.Data.EndToEnd;

public static class EndToEndTestDataSource
{
    public static IEndToEndTestDataSet GetAllData(ITestOutputHelper output)
    {
        using var db = new TestDataRepository("End-to-end", output);

        return new EndToEndTestDataSet()
        {
            DeductionDefinitions = db.GetTestData<IDeductionsTestDataEntry>(TestSource.Paytools, TestScope.EndToEnd).ToList(),
            EarningsDefinitions = db.GetTestData<IEarningsTestDataEntry>(TestSource.Paytools, TestScope.EndToEnd).ToList(),
            ExpectedOutputs = db.GetTestData<IExpectedOutputTestDataEntry>(TestSource.Paytools, TestScope.EndToEnd).ToList(),
            PeriodInputs = db.GetTestData<IPeriodInputTestDataEntry>(TestSource.Paytools, TestScope.EndToEnd).ToList(),
            PreviousYtdInputs = db.GetTestData<IPreviousYtdTestDataEntry>(TestSource.Paytools, TestScope.EndToEnd).ToList(),
            StaticInputs = db.GetTestData<IStaticInputTestDataEntry>(TestSource.Paytools, TestScope.EndToEnd).ToList(),
            NiYtdHistory = db.GetTestData<INiYtdHistoryTestDataEntry>(TestSource.Paytools, TestScope.EndToEnd).ToList(),
            PensionSchemes = db.GetTestData<IPensionSchemesTestDataEntry>(TestSource.Paytools, TestScope.EndToEnd).ToList(),
            PayrunInfo = db.GetTestData<IPayrunInfoTestDataEntry>(TestSource.Paytools, TestScope.EndToEnd).ToList()
        };
    }
}
