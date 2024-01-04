// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public interface IEarningsTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string ShortName { get; }
    string Description { get; }
    bool IsSubjectToTax { get; }
    bool IsSubjectToNi { get; }
    bool IsPensionable { get; }
    bool IsNetToGross { get; }
    bool IsTreatedAsOvertime { get; }
}
