// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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
