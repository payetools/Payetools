// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public class DeductionsTestDataEntry : IDeductionsTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string ShortName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool ReducesGrossPay { get; set; }
    public bool ReducesTaxablePay { get; set; }
    public bool ReducesNicablePay { get; set; }
    public bool ReducesPensionablePay { get; set; }
    public bool IsUnderSalaryExchangeArrangement { get; set; }
}
