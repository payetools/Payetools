// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.ReferenceData.Employer;

/// <summary>
/// Interface that classes implement in order to provide access to employer-level reference data, i.e.,
/// Employment Allowance, Small Employers Relief and Apprentice Levy.
/// </summary>
/// <remarks>Note that all other reference data provider interfaces are hosted within the relevant business area
/// assembly, e.g., Payetools.IncomeTax. This interface is hosted within Payetools.ReferenceData because
/// it is used by Payetools.Payroll, and that assembly has a dependency on the reference data assembly.</remarks>
public interface IEmployerReferenceDataProvider
{
    /// <summary>
    /// Gets reference information about Employment Allowance.
    /// </summary>
    /// <param name="date">Applicable date for this reference information request.</param>
    /// <returns>Reference data information on Employment Allowance for the specified date.</returns>
    EmploymentAllowanceInfo GetEmploymentAllowanceInfoForDate(DateOnly date);

    /// <summary>
    /// Gets reference information about reclaiming some or all of statutory payments (e.g., SMP, SPP).
    /// </summary>
    /// <param name="date">Applicable date for this reference information request.</param>
    /// <returns>Reference data information on reclaiming statutory payments for the specified date.</returns>
    StatutoryPaymentReclaimInfo GetStatutoryPaymentReclaimInfoForDate(DateOnly date);

    /// <summary>
    /// Gets reference information about the Apprentice Levy.
    /// </summary>
    /// <param name="date">Applicable date for this reference information request.</param>
    /// <returns>Reference data information on the Apprentice Levy for the specified date.</returns>
    ApprenticeLevyInfo GetApprenticeLevyInfoForDate(DateOnly date);
}
