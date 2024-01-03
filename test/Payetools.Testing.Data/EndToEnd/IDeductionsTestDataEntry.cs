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
