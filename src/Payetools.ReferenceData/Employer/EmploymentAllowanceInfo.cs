// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.ReferenceData.Employer;

/// <summary>
/// Represents the reference data needed to perform Employment Allowance calculations.
/// </summary>
public class EmploymentAllowanceInfo
{
    /// <summary>
    /// Gets the value of the annual Employment Allowance.
    /// </summary>
    public decimal AnnualAllowance { get; }

    /// <summary>
    /// Gets the threshold up to which employers are eligible for Employment Allowance.
    /// </summary>
    public decimal EmployersAllowanceThreshold { get; }
}