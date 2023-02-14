// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
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
    /// 
    /// </summary>
    /// <param name="payrollId"></param>
    public static implicit operator string(PayrollId payrollId) => payrollId._payrollId;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payrollId"></param>
    public static implicit operator PayrollId(string payrollId) => new(payrollId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    public PayrollId(string title)
    {
        _payrollId = title;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public static PayrollId Parse(string title)
    {
        return new PayrollId(title);
    }
}