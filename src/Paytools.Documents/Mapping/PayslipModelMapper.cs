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

using Paytools.Documents.Model;
using Paytools.Payroll.Model;

namespace Paytools.Documents.Mapping;

/// <summary>
/// Represents a mapper that maps from the payrun output model to the
/// payslip rendering model.
/// </summary>
public static class PayslipModelMapper
{
    /// <summary>
    /// Maps the supplied payrun output for an employee to a payslip model ready for
    /// rendering.
    /// </summary>
    /// <param name="payrunResult">An instance of <see cref="IEmployeePayrunResult"/> containing the
    /// payrun output for a given employee.</param>
    /// <returns>An implementation of <see cref="IPayslip"/> ready for rendering.</returns>
    public static IPayslip Map(IEmployeePayrunResult payrunResult)
    {
        var payslipModel = new Payslip();

        payslipModel.EmployerName = "Test Employer One";

        return payslipModel;
    }
}
