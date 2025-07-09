// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.NationalInsurance.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Provides information about whether an employee is a director, and if so related details.
/// </summary>
public interface IDirectorInfo
{
    /// <summary>
    /// Gets a value indicating whether the employee is a company director.
    /// </summary>
    bool IsDirector { get; init; }

    /// <summary>
    /// Gets the method for calculating National Insurance contributions.  Applicable only
    /// for directors; null otherwise.
    /// </summary>
    DirectorsNiCalculationMethod? DirectorsNiCalculationMethod { get; init; }

    /// <summary>
    /// Gets the date the employee was appointed as a director, where appropriate; null otherwise.
    /// </summary>
    DateOnly? DirectorsAppointmentDate { get; init; }

    /// <summary>
    /// Gets the date the employee ceased to be a director, where appropriate; null otherwise.
    /// </summary>
    DateOnly? CeasedToBeDirectorDate { get; init; }
}