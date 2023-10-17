// Copyright (c) 2023 Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.Testing.Data.IncomeTax;

public class HmrcIncomeTaxTestDataEntry : IHmrcIncomeTaxTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string RelatesTo { get; set; } = string.Empty;
    public PayFrequency PayFrequency { get; set; }
    public decimal GrossPay { get; set; }
    public decimal TaxablePayToDate { get; set; }
    public string TaxCode { get; set; } = string.Empty;
    public string? W1M1Flag { get; set; }
    public int Period { get; set; }
    public decimal TaxDueInPeriod { get; set; }
    public decimal TaxDueToDate { get; set; }
}
