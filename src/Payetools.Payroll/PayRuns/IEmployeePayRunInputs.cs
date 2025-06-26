// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;
using Payetools.Payroll.Model;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Represents all the inputs to a payrun for a given employee.  This interface is
/// self-contained, i.e., it does not rely on any other interfaces or entities, making
/// it more appropriate for cases where the underlying Payetools models are not being
/// used, in contrast to <see cref="IEmployeePayRunInputEntry"/> which relies upon the
/// IEmployment interface and other related entities.
/// </summary>
/// <typeparam name="TIdentifier">Identifier type for payrolls, pay runs, etc.</typeparam>
public interface IEmployeePayRunInputs<TIdentifier>
    where TIdentifier : notnull
{
    /// <summary>
    /// Gets the unique identifier for the employee.
    /// </summary>
    TIdentifier EmployeeId { get; init; }

    /// <summary>
    /// Gets the employee's tax code.
    /// </summary>
    TaxCode TaxCode { get; init; }

    /// <summary>
    /// Gets the employee's NI category.
    /// </summary>
    NiCategory NiCategory { get; init; }

    /// <summary>
    /// Gets information about the employee if they are also a director.  Optional.
    /// </summary>
    IDirectorInfo? DirectorInfo { get; init; }

    /// <summary>
    /// Gets the student loan information associated with the employee.
    /// </summary>
    IStudentLoanInfo? StudentLoanInfo { get; init; }

    /// <summary>
    /// Gets the list of pay components for this employee for a given payrun.  May be empty but usually not.
    /// </summary>
    IEnumerable<IEarningsEntry> Earnings { get; init; }

    /// <summary>
    /// Gets the list of deductions for this employee for a given payrun.  May be empty.
    /// </summary>
    IEnumerable<IDeductionEntry> Deductions { get; init; }

    /// <summary>
    /// Gets the list of payrolled benefits for this employee for a given payrun.  Empty if the employee has
    /// no payrolled benefits.
    /// </summary>
    IEnumerable<IPayrolledBenefitForPeriod> PayrolledBenefits { get; init; }

    /// <summary>
    /// Gets the list of attachment of earnings orders for this employee for a given payrun.  Null (or empty)
    /// if the employee has none.
    /// </summary>
    public IEnumerable<IAttachmentOrder>? AttachmentOrders { get; init; }

    /// <summary>
    /// Gets the employee's pension contributions to be applied in this pay run. Null if no pension payments
    /// are being made.
    /// </summary>
    IPensionContributions? PensionContributions { get; init; }

    /// <summary>
    /// Gets the employee's year-to-date tax and NI figures.
    /// </summary>
    IEmployeeCoreYtdFigures YtdFigures { get; init; }
}