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

using Paytools.Employment.Model;
using Paytools.Payroll.Model;

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Represents a payrun, i.e., the running of payroll for a single pay reference period
/// on a single pay date for a predefined set of employees within one employer's employment.
/// </summary>
public class PayrunProcessor : IPayrunProcessor
{
    private readonly IPayrunEntryProcessor _payrunCalculator;
    private readonly IEmployer _employer;

    /// <summary>
    /// Initialises a new instance of <see cref="PayrunProcessor"/> with the supplied calculator.
    /// </summary>
    /// <param name="calculator">Calculator to be used to calculate earnings, deductions
    /// and net pay.</param>
    /// <param name="employer">Employer that this payrun processor relates to.</param>
    public PayrunProcessor(IPayrunEntryProcessor calculator, IEmployer employer)
    {
        _payrunCalculator = calculator;
        _employer = employer;
    }

    /// <summary>
    /// Processes this payrun.
    /// </summary>
    /// <param name="employeePayrunEntries">List of payrun information for each employee in the payrun.</param>
    /// <param name="result">An instance of a class that implements <see cref="IPayrunResult"/> containing the results
    /// of this payrun.</param>
    public void Process(List<IEmployeePayrunInputEntry> employeePayrunEntries, out IPayrunResult result)
    {
        var payrunOutputs = new List<IEmployeePayrunResult>();

        for (int i = 0; i < employeePayrunEntries.Count; i++)
        {
            _payrunCalculator.Process(employeePayrunEntries[i], out var employeeResult);

            payrunOutputs.Add(employeeResult);
        }

        result = new PayrunResult()
        {
            EmployeePayrunEntries = payrunOutputs,
            Employer = _employer,
            PayDate = _payrunCalculator.PayDate
        };
    }
}
