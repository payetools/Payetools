// Copyright (c) 2023 Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
