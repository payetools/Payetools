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
    /// Initialises a new instance of <see cref="PayrollId"/> with the supplied value.
    /// </summary>
    /// <param name="payrollId">Payroll ID value.</param>
    public PayrollId(string payrollId)
    {
        _payrollId = payrollId;
    }

    /// <summary>
    /// Operator for casting implicitly from a <see cref="PayrollId"/> instance to its string representation.
    /// </summary>
    /// <param name="payrollId">An instance of PayrollId.</param>
    public static implicit operator string(PayrollId payrollId) => payrollId._payrollId;

    /// <summary>
    /// Operator for casting implicitly from a payroll ID string value to a <see cref="PayrollId"/> instance.
    /// </summary>
    /// <param name="payrollId">String representation of payroll ID.</param>
    public static implicit operator PayrollId(string payrollId) => new PayrollId(payrollId);

    /// <summary>
    /// Parses the supplied payroll ID.  TBA.
    /// </summary>
    /// <param name="value">Value to parse.</param>
    /// <returns>Parsed value.  TBA.</returns>
    public static PayrollId Parse(string value)
    {
        return new PayrollId(value);
    }
}