// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.NationalInsurance.Model;

/// <summary>
/// Interface that represents once element of the employee's NI history during the current tax year.  For employees that have
/// only have one NI category throughout the tax year, they will have just one instance of <see cref="EmployeeNiHistoryEntry"/>
/// applicable.  But it is of course possible for an employee's NI category to change throughout the tax year (for example
/// because they turned 21 years of age), and in this case, multiple records must be held.
/// </summary>
public interface IEmployeeNiHistoryEntry
{
    /// <summary>
    /// Gets the National Insurance category letter pertaining to this record.
    /// </summary>
    NiCategory NiCategoryPertaining { get; }

    /// <summary>
    /// Gets the gross NI-able earnings applicable for the duration of this record.
    /// </summary>
    decimal GrossNicableEarnings { get; }

    /// <summary>
    /// Gets the total employee contribution made during the duration of this record.
    /// </summary>
    decimal EmployeeContribution { get; }

    /// <summary>
    /// Gets the total employer contribution made during the duration of this record.
    /// </summary>
    decimal EmployerContribution { get; }

    /// <summary>
    /// Gets the total contribution (employee + employer) made during the duration of this record.
    /// </summary>
    decimal TotalContribution { get; }

    /// <summary>
    /// Gets the earnings at the Lower Earnings Limit for this record. (Earnings below the LEL are
    /// ignored for historical purposes).
    /// </summary>
    decimal EarningsAtLEL { get; }

    /// <summary>
    /// Gets the earnings up above the Lower Earnings Limit and up to and including the Secondary Threshold
    /// for this record.
    /// </summary>
    decimal EarningsAboveLELUpToAndIncludingST { get; }

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Primary Threshold
    /// for this record.
    /// </summary>
    decimal EarningsAboveSTUpToAndIncludingPT { get; }

    /// <summary>
    /// Gets the earnings up above the Primary Threshold and up to and including the Freeport Upper Secondary
    /// Threshold for this record.
    /// </summary>
    decimal EarningsAbovePTUpToAndIncludingFUST { get; }

    /// <summary>
    /// Gets the earnings up above the Freeport Upper Secondary Threshold and up to and including the Upper
    /// Earnings Limit for this record.
    /// </summary>
    decimal EarningsAboveFUSTUpToAndIncludingUEL { get; }

    /// <summary>
    /// Gets the earnings up above the Upper Earnings Limit for this record.
    /// </summary>
    decimal EarningsAboveUEL { get; }

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Upper Earnings Limit
    /// for this record.
    /// </summary>
    decimal EarningsAboveSTUpToAndIncludingUEL { get; }

    /// <summary>
    /// Adds the results of an NI calculation to the current history and returns a new instance of <see cref="IEmployeeNiHistoryEntry"/>
    /// with the results applied.
    /// </summary>
    /// <param name="result">NI calculation results to add to this history entry.</param>
    /// <returns>New instance of <see cref="IEmployeeNiHistoryEntry"/> with the NI calculation result applied.</returns>
    IEmployeeNiHistoryEntry Add(in INiCalculationResult result);
}