// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
