// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
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

using Paytools.Payroll.Model;

namespace Paytools.Documents.Services;

/// <summary>
/// Interface for services that render HTML payslips given the output of a payrun
/// for a given employee.
/// </summary>
public interface IHtmlPayslipService
{
    /// <summary>
    /// Renders the supplied payrun output for a given employee to an HTML payslip,
    /// returned as a string.
    /// </summary>
    /// <param name="payrunResult">An instance of <see cref="IEmployeePayrunResult"/> containing the
    /// payrun output for a given employee.</param>
    /// <param name="view">Path to view template.</param>
    /// <returns>Rendered HTML payslip as string.</returns>
    Task<string> RenderAsync(IEmployeePayrunResult payrunResult, string view);
}
