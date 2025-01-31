// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public interface IDeductionsTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string ShortName { get; }
    string Description { get; }
    bool ReducesGrossPay { get; }
    bool ReducesTaxablePay { get; }
    bool ReducesNicablePay { get; }
    bool ReducesPensionablePay { get; }
    bool IsUnderSalaryExchangeArrangement { get; }
}
