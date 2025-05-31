// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.Statutory.AttachmentOfEarnings;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents all the inputs to a payrun for a given employee. This implementation
/// is self-contained, i.e., it does not rely on any other interfaces or entities, making
/// it more appropriate for cases where the underlying Payetools models are not being
/// used, in contrast to <see cref="IEmployeePayRunInputEntry"/> which relies upon the
/// IEmployment interface and other related entities.
/// </summary>
public class EmployeePayRunInputs : IEmployeePayRunInputs
{
    /// <summary>
    /// Gets a unique identifier for the employee.
    /// </summary>
    public object EmployeeId { get; init; }

    /// <summary>
    /// Gets the employee's tax code.
    /// </summary>
    public TaxCode TaxCode { get; init; }

    /// <summary>
    /// Gets the employee's NI category.
    /// </summary>
    public NiCategory NiCategory { get; init; }

    /// <summary>
    /// Gets information about the employee if they are also a director.  Optional.
    /// </summary>
    public IDirectorInfo? DirectorInfo { get; init; }

    /// <summary>
    /// Gets the student loan information associated with the employee.
    /// </summary>
    public IStudentLoanInfo? StudentLoanInfo { get; init; }

    /// <summary>
    /// Gets the list of deductions for this employee for a given payrun.  May be empty.
    /// </summary>
    public IEnumerable<IDeductionEntry> Deductions { get; init; }

    /// <summary>
    /// Gets the list of pay components for this employee for a given payrun.  May be empty but usually not.
    /// </summary>
    public IEnumerable<IEarningsEntry> Earnings { get; init; }

    /// <summary>
    /// Gets the list of payrolled benefits for this employee for a given payrun.  Empty if the employee has
    /// no payrolled benefits.
    /// </summary>
    public IEnumerable<IPayrolledBenefitForPeriod> PayrolledBenefits { get; init; }

    /// <summary>
    /// Gets the list of attachment of earnings orders for this employee for a given payrun.  Null (or empty)
    /// if the employee has none.
    /// </summary>
    public IEnumerable<IAttachmentOfEarnings>? AttachmentOfEarningsOrders { get; init; }

    /// <summary>
    /// Gets the employee's pension contributions to be applied in this pay run. Null if no pension payments
    /// are being made.
    /// </summary>
    public IPensionContributions? PensionContributions { get; init; }

    /// <summary>
    /// Gets the employee's year-to-date tax and NI figures.
    /// </summary>
    public IEmployeeCoreYtdFigures YtdFigures { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeePayRunInputs"/> class with the provided values.
    /// </summary>
    /// <param name="employeeId">A unique identifier for the employee.</param>
    /// <param name="taxCode">The employee's tax code.</param>
    /// <param name="niCategory">The employee's NI category.</param>
    /// <param name="directorInfo">Information about the employee if they are a director. Optional.</param>
    /// <param name="studentLoanInfo">The student loan information associated with the employee.</param>
    /// <param name="earnings">The list of pay components for this employee for a given payrun.</param>
    /// <param name="deductions">The list of deductions for this employee for a given payrun.</param>
    /// <param name="payrolledBenefits">The list of payrolled benefits for this employee for a given payrun.</param>
    /// <param name="attachmentOfEarningsOrders">The list of attachment of earnings orders for this employee.</param>
    /// <param name="pensionContributions">The employee's pension contributions to be applied in this pay run.</param>
    /// <param name="ytdFigures">The employee's year-to-date tax and NI figures.</param>
    public EmployeePayRunInputs(
        object employeeId,
        TaxCode taxCode,
        NiCategory niCategory,
        IDirectorInfo? directorInfo,
        IStudentLoanInfo? studentLoanInfo,
        IEnumerable<IEarningsEntry> earnings,
        IEnumerable<IDeductionEntry> deductions,
        IEnumerable<IPayrolledBenefitForPeriod> payrolledBenefits,
        IEnumerable<IAttachmentOfEarnings>? attachmentOfEarningsOrders,
        IPensionContributions? pensionContributions,
        IEmployeeCoreYtdFigures ytdFigures)
    {
        EmployeeId = employeeId ?? throw new ArgumentNullException(nameof(employeeId));
        TaxCode = taxCode;
        NiCategory = niCategory;
        DirectorInfo = directorInfo;
        StudentLoanInfo = studentLoanInfo;
        Deductions = deductions;
        Earnings = earnings;
        PayrolledBenefits = payrolledBenefits;
        AttachmentOfEarningsOrders = attachmentOfEarningsOrders;
        PensionContributions = pensionContributions;
        YtdFigures = ytdFigures;
    }
}