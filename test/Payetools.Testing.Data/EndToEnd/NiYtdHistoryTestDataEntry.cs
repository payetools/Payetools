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

public class NiYtdHistoryTestDataEntry : INiYtdHistoryTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string TestReference { get; set; } = string.Empty;
    public NiCategory NiCategoryPertaining { get; set; }
    public decimal GrossNicableEarnings { get; set; }
    public decimal EmployeeContribution { get; set; }
    public decimal EmployerContribution { get; set; }
    public decimal TotalContribution { get; set; }
    public decimal EarningsUpToAndIncludingLEL { get; set; }
    public decimal EarningsAboveLELUpToAndIncludingST { get; set; }
    public decimal EarningsAboveSTUpToAndIncludingPT { get; set; }
    public decimal EarningsAbovePTUpToAndIncludingFUST { get; set; }
    public decimal EarningsAboveFUSTUpToAndIncludingUEL { get; set; }
    public decimal EarningsAboveUEL { get; set; }
    public decimal EarningsAboveSTUpToAndIncludingUEL { get; set; }
}
