// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;

namespace Paytools.Testing.Data.EndToEnd;

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
