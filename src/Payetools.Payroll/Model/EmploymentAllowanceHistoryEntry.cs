// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an Employment Allowance history entry.
/// </summary>
public struct EmploymentAllowanceHistoryEntry
{
    /// <summary>
    /// Gets the tax year that this history entry pertains to.
    /// </summary>
    public TaxYearEnding RelevantTaxYear { get; init; }

    /// <summary>
    /// Gets a value indicating whether the employer is eligible to claim Employment Allowance.
    /// </summary>
    public bool IsEligible { get; init; }
}