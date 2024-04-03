// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Testing.Data.EndToEnd;

public class EndToEndTestDataSet : IEndToEndTestDataSet
{
    public List<IDeductionsTestDataEntry> DeductionDefinitions { get; set; } = default!;

    public List<IEarningsTestDataEntry> EarningsDefinitions { get; set; } = default!;

    public List<IExpectedOutputTestDataEntry> ExpectedOutputs { get; set; } = default!;

    public List<IPeriodInputTestDataEntry> PeriodInputs { get; set; } = default!;

    public List<IPreviousYtdTestDataEntry> PreviousYtdInputs { get; set; } = default!;

    public List<IStaticInputTestDataEntry> StaticInputs { get; set; } = default!;

    public List<INiYtdHistoryTestDataEntry> NiYtdHistory { get; set; } = default!;

    public List<IPensionSchemesTestDataEntry> PensionSchemes { get; set; } = default!;

    public List<IPayRunInfoTestDataEntry> PayRunInfo { get; set; } = default!;
}