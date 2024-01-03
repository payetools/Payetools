// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Testing.Data.EndToEnd;

public record EndToEndTestDataSet : IEndToEndTestDataSet
{
    public List<IDeductionsTestDataEntry> DeductionDefinitions { get; set; } = default!;

    public List<IEarningsTestDataEntry> EarningsDefinitions { get; set; } = default!;

    public List<IExpectedOutputTestDataEntry> ExpectedOutputs { get; set; } = default!;

    public List<IPeriodInputTestDataEntry> PeriodInputs { get; set; } = default!;

    public List<IPreviousYtdTestDataEntry> PreviousYtdInputs { get; set; } = default!;

    public List<IStaticInputTestDataEntry> StaticInputs { get; set; } = default!;

    public List<INiYtdHistoryTestDataEntry> NiYtdHistory { get; set; } = default!;

    public List<IPensionSchemesTestDataEntry> PensionSchemes { get; set; } = default!;

    public List<IPayrunInfoTestDataEntry> PayrunInfo { get; set; } = default!;
}
