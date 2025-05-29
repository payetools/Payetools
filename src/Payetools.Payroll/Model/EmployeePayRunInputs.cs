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
    /// <inheritdoc/>
    public TaxCode TaxCode { get; init; }

    /// <inheritdoc/>
    public NiCategory NiCategory { get; init; }

    /// <inheritdoc/>
    public IDirectorInfo? DirectorInfo { get; init; }

    /// <inheritdoc/>
    public IStudentLoanInfo? StudentLoanInfo { get; init; }

    /// <inheritdoc/>
    public IEnumerable<IDeductionEntry> Deductions { get; init; }

    /// <inheritdoc/>
    public IEnumerable<IEarningsEntry> Earnings { get; init; }

    /// <inheritdoc/>
    public IEnumerable<IPayrolledBenefitForPeriod> PayrolledBenefits { get; init; }

    /// <inheritdoc/>
    public IEnumerable<IAttachmentOfEarnings>? AttachmentOfEarningsOrders { get; init; }

    /// <inheritdoc/>
    public IPensionContributions? PensionContributions { get; init; }

    /// <inheritdoc/>
    public IEmployeeCoreYtdFigures YtdFigures { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeePayRunInputs"/> class with the provided values.
    /// </summary>
    /// <param name="taxCode">The employee's tax code.</param>
    /// <param name="niCategory">The employee's NI category.</param>
    /// <param name="directorInfo">Information about the employee if they are a director. Optional.</param>
    /// <param name="studentLoanInfo">The student loan information associated with the employee.</param>
    /// <param name="deductions">The list of deductions for this employee for a given payrun.</param>
    /// <param name="earnings">The list of pay components for this employee for a given payrun.</param>
    /// <param name="payrolledBenefits">The list of payrolled benefits for this employee for a given payrun.</param>
    /// <param name="attachmentOfEarningsOrders">The list of attachment of earnings orders for this employee.</param>
    /// <param name="pensionContributions">The employee's pension contributions to be applied in this pay run.</param>
    /// <param name="ytdFigures">The employee's year-to-date tax and NI figures.</param>
    public EmployeePayRunInputs(
        TaxCode taxCode,
        NiCategory niCategory,
        IDirectorInfo? directorInfo,
        IStudentLoanInfo? studentLoanInfo,
        IEnumerable<IDeductionEntry> deductions,
        IEnumerable<IEarningsEntry> earnings,
        IEnumerable<IPayrolledBenefitForPeriod> payrolledBenefits,
        IEnumerable<IAttachmentOfEarnings>? attachmentOfEarningsOrders,
        IPensionContributions? pensionContributions,
        IEmployeeCoreYtdFigures ytdFigures)
    {
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