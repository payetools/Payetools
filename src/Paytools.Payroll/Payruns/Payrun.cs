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

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Represents a payrun, i.e., the running of payroll for a single pay reference period
/// on a single pay date for a predefined set of employees within one employer's employment.
/// </summary>
public class Payrun : IPayrun
{
    private readonly IPayrunCalculator _payrunCalculator;

    /// <summary>
    /// Initialises a new instance of <see cref="Payrun"/> with the supplied calculator.
    /// </summary>
    /// <param name="calculator">Calculator to be used to calculate earnings, deductions
    /// and net pay.</param>
    public Payrun(IPayrunCalculator calculator)
    {
        _payrunCalculator = calculator;
    }

    /// <summary>
    /// Processes this payrun.
    /// </summary>
    /// <param name="employeePayrunEntries">List of payrun information for each employee in the payrun.</param>
    /// <returns>An instance of a class that implements <see cref="IPayrunResult"/> containing the results
    /// of this payrun.</returns>
    public ref IPayrunResult Process(List<IEmployeePayrunEntry> employeePayrunEntries)
    {
        // employeePayrunEntries.ForEach.Process(employeePayrunEntries)
        //    {
        // }
        throw new NotImplementedException();
    }
}
