// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.IncomeTax;

public interface IHmrcIncomeTaxTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string RelatesTo { get; }
    PayFrequency PayFrequency { get; }
    decimal GrossPay { get; }
    decimal TaxablePayToDate { get; }
    string TaxCode { get; }
    string? W1M1Flag { get; }
    int Period { get; }
    decimal TaxDueInPeriod { get; }
    decimal TaxDueToDate { get; }
}
