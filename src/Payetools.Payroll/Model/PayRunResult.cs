// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the output of a payrun.
/// </summary>
public class PayRunResult : IPayRunResult
{
    /// <summary>
    /// Gets the employer that this payrun refers to.
    /// </summary>
    public IEmployer Employer { get; init; }

    /// <summary>
    /// Gets the pay date for this payrun.
    /// </summary>
    public IPayRunDetails PayRunDetails { get; init; }

    /// <summary>
    /// Gets the list of employee payrun entries.
    /// </summary>
    public ImmutableArray<IEmployeePayRunResult> EmployeePayRunEntries { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PayRunResult"/> class.
    /// </summary>
    /// <param name="employer">Employer this pay run refers to.</param>
    /// <param name="payRunDetails">Pay date and pay period.</param>
    /// <param name="employeePayRunEntries">Employee pay run results.</param>
    public PayRunResult(
        IEmployer employer,
        IPayRunDetails payRunDetails,
        ImmutableArray<IEmployeePayRunResult> employeePayRunEntries)
    {
        Employer = employer;
        PayRunDetails = payRunDetails;
        EmployeePayRunEntries = employeePayRunEntries;
    }
}