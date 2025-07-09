// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.NationalInsurance.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Provides information about whether an employee is a director, and if so, related details.
/// </summary>
public class DirectorInfo : IDirectorInfo
{
    /// <summary>
    /// Gets a value indicating whether the employee is a company director.
    /// </summary>
    public bool IsDirector { get; init; }

    /// <summary>
    /// Gets the method for calculating National Insurance contributions. Applicable only
    /// for directors; null otherwise.
    /// </summary>
    public DirectorsNiCalculationMethod? DirectorsNiCalculationMethod { get; init; }

    /// <summary>
    /// Gets the date the employee was appointed as a director, where appropriate; null otherwise.
    /// </summary>
    public DateOnly? DirectorsAppointmentDate { get; init; }

    /// <summary>
    /// Gets the date the employee ceased to be a director, where appropriate; null otherwise.
    /// </summary>
    public DateOnly? CeasedToBeDirectorDate { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectorInfo"/> class with the specified values.
    /// </summary>
    /// <param name="isDirector">Indicates whether the employee is a company director.</param>
    /// <param name="directorsNiCalculationMethod">The method used for calculating National Insurance contributions, applicable only for directors.</param>
    /// <param name="directorsAppointmentDate">The date the employee was appointed as a director, or null if not applicable.</param>
    /// <param name="ceasedToBeDirectorDate">The date the employee ceased to be a director, or null if not applicable.</param>
    public DirectorInfo(
        bool isDirector,
        DirectorsNiCalculationMethod? directorsNiCalculationMethod,
        DateOnly? directorsAppointmentDate,
        DateOnly? ceasedToBeDirectorDate)
    {
        IsDirector = isDirector;
        DirectorsNiCalculationMethod = directorsNiCalculationMethod;
        DirectorsAppointmentDate = directorsAppointmentDate;
        CeasedToBeDirectorDate = ceasedToBeDirectorDate;
    }
}