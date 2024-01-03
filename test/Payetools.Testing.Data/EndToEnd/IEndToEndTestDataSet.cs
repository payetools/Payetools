// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Testing.Data.EndToEnd;

public interface IEndToEndTestDataSet
{
    List<IDeductionsTestDataEntry> DeductionDefinitions { get; }
    List<IEarningsTestDataEntry> EarningsDefinitions { get; }
    List<IExpectedOutputTestDataEntry> ExpectedOutputs { get; }
    List<IPeriodInputTestDataEntry> PeriodInputs { get; }
    List<IPreviousYtdTestDataEntry> PreviousYtdInputs { get; }
    List<IStaticInputTestDataEntry> StaticInputs { get; }
    List<INiYtdHistoryTestDataEntry> NiYtdHistory { get; }
    List<IPensionSchemesTestDataEntry> PensionSchemes { get; }
    List<IPayrunInfoTestDataEntry> PayrunInfo { get; }
}