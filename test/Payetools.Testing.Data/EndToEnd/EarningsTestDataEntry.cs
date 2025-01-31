// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public class EarningsTestDataEntry : IEarningsTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string ShortName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsSubjectToTax { get; set; }
    public bool IsSubjectToNi { get; set; }
    public bool IsPensionable { get; set; }
    public bool IsNetToGross { get; set; }
    public bool IsTreatedAsOvertime { get; set; }
}
