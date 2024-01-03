// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public interface INiYtdHistoryTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string TestReference { get; }
    NiCategory NiCategoryPertaining { get; }
    decimal GrossNicableEarnings { get; }
    decimal EmployeeContribution { get; }
    decimal EmployerContribution { get; }
    decimal TotalContribution { get; }
    decimal EarningsUpToAndIncludingLEL { get; }
    decimal EarningsAboveLELUpToAndIncludingST { get; }
    decimal EarningsAboveSTUpToAndIncludingPT { get; }
    decimal EarningsAbovePTUpToAndIncludingFUST { get; }
    decimal EarningsAboveFUSTUpToAndIncludingUEL { get; }
    decimal EarningsAboveUEL { get; }
    decimal EarningsAboveSTUpToAndIncludingUEL { get; }
}
