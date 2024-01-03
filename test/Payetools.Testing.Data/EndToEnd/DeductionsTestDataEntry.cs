// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
