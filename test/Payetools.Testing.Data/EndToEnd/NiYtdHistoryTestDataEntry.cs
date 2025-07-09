// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public class NiYtdHistoryTestDataEntry : INiYtdHistoryTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string TestReference { get; set; } = string.Empty;
    public NiCategory NiCategoryPertaining { get; set; }
    public decimal GrossNicableEarnings { get; set; }
    public decimal EmployeeContribution { get; set; }
    public decimal EmployerContribution { get; set; }
    public decimal TotalContribution { get; set; }
    public decimal EarningsUpToAndIncludingLEL { get; set; }
    public decimal EarningsAboveLELUpToAndIncludingST { get; set; }
    public decimal EarningsAboveSTUpToAndIncludingPT { get; set; }
    public decimal EarningsAbovePTUpToAndIncludingFUST { get; set; }
    public decimal EarningsAboveFUSTUpToAndIncludingUEL { get; set; }
    public decimal EarningsAboveUEL { get; set; }
    public decimal EarningsAboveSTUpToAndIncludingUEL { get; set; }
}
