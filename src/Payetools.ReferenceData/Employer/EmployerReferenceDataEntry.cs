// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.ReferenceData.Employer;

/// <summary>
/// Represents the set of rates and thresholds for employers for a given period within a tax year.
/// </summary>
public class EmployerReferenceDataEntry : IApplicableFromTill
{
    /// <summary>
    /// Gets the start date (i.e., the first full day) for applicability.
    /// </summary>
    public DateOnly ApplicableFrom { get; init; }

    /// <summary>
    /// Gets the end date (i.e., the last full day) for applicability.
    /// </summary>
    public DateOnly ApplicableTill { get; init; }

    /// <summary>
    /// Gets reference information about Employment Allowance for the specified period.
    /// </summary>
    public EmploymentAllowanceInfo EmploymentAllowance { get; init; } = default!;

    /// <summary>
    /// Gets reference information about reclaiming some or all of statutory payments (e.g., SMP, SPP) for
    /// the specified period.
    /// </summary>
    public StatutoryPaymentReclaimInfo StatutoryPaymentReclaim { get; init; } = default!;

    /// <summary>
    /// Gets reference information about the Apprentice Levy as applicable for the specified period.
    /// </summary>
    public ApprenticeLevyInfo ApprenticeLevy { get; init; } = default!;
}