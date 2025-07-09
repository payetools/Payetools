// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public interface IPeriodInputTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string TestReference { get; }
    string EntryType { get; }
    string ShortName { get; }
    string Description { get; }
    decimal? Qty { get; }
    decimal? Rate { get; }
    decimal? FixedAmount { get; }
    decimal FinalAmount { get; }
}
