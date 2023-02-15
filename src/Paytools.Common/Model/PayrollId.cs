// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Paytools.Common.Model;

/// <summary>
/// Represents a payroll ID (also known as a "worker ID", "payroll number", "works number") as reported to HMRC.
/// </summary>
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