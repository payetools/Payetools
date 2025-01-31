// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public class PensionSchemesTestDataEntry : IPensionSchemesTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string SchemeName { get; set; } = string.Empty;
    public PensionsEarningsBasis EarningsBasis { get; set; }
    public PensionTaxTreatment TaxTreatment { get; set; }
}
