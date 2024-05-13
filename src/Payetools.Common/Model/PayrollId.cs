// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Represents a payroll ID (also known as a "worker ID", "payroll number", "works number") as reported to HMRC.
/// </summary>
/// <remarks>This type is meant to aid payroll ID changes.  Further work required.</remarks>
public sealed record PayrollId
{
    private readonly string _payrollId;

    /// <summary>
    /// Gets a value indicating whether this payroll ID is an update to a previously submitted payroll ID.
    /// </summary>
    public bool IsUpdate { get; }

    /// <summary>
    /// Gets the previous payroll ID, where applicable.
    /// </summary>
    public string? PreviousPayrollId { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="PayrollId"/> with the supplied value.
    /// </summary>
    /// <param name="payrollId">Payroll ID value.</param>
    /// <param name="isUpdate">True if this an update to a previously submitted payroll ID; false
    /// otherwise. Default is false.</param>
    /// <param name="previousPayrollId">If <paramref name="isUpdate"/> is set to true, then
    /// this parameter should be set to the previous payroll ID used.</param>
    public PayrollId(in string payrollId, in bool isUpdate = false, in string? previousPayrollId = null)
    {
        _payrollId = payrollId;
        IsUpdate = isUpdate;
        PreviousPayrollId = previousPayrollId;
    }

    /// <summary>
    /// Operator for casting implicitly from a <see cref="PayrollId"/> instance to its string representation.
    /// </summary>
    /// <param name="payrollId">An instance of PayrollId.</param>
    public static implicit operator string(in PayrollId payrollId) => payrollId._payrollId;

    /// <summary>
    /// Operator for casting implicitly from a payroll ID string value to a <see cref="PayrollId"/> instance.
    /// </summary>
    /// <param name="payrollId">String representation of payroll ID.</param>
    public static implicit operator PayrollId(in string payrollId) => new PayrollId(payrollId);

    /// <summary>
    /// Gets the Payroll ID value as a string.
    /// </summary>
    /// <returns>String representation of payroll ID.</returns>
    public override string ToString() => _payrollId;
}