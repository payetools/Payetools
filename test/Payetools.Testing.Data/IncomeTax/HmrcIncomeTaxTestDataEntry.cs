// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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
    public bool W1M1Flag { get; set; }
    public int Period { get; set; }
    public decimal TaxDueInPeriod { get; set; }
    public decimal TaxDueToDate { get; set; }
}