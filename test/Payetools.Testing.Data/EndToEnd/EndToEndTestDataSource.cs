// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using System;
using Xunit.Abstractions;

namespace Payetools.Testing.Data.EndToEnd;

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
