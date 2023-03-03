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

using Paytools.Common.Model;
using Paytools.Payroll.Model;

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Interface that represent types that can process an employee's set of input payroll data and
/// provide the results of the calculations in the form of an <see cref="IEmployeePayrunResult"/>.
/// </summary>
public interface IPayrunEntryProcessor
{
    /// <summary>
    /// Gets the pay date for this payrun calculator.
    /// </summary>
    PayDate PayDate { get; }

    /// <summary>
    /// Gets the pay period for this payrun calculator.
    /// </summary>
    PayReferencePeriod PayPeriod { get; }

    /// <summary>
    /// Processes the supplied payrun entry calculating all the earnings and deductions, income tax, national insurance and
    /// other statutory deductions, and generating a result structure which includes the final net pay.
    /// </summary>
    /// <param name="entry">Instance of <see cref="IEmployeePayrunInputEntry"/> containing all the necessary input data for the
    /// payroll calculation.</param>
    /// <param name="result">An instance of <see cref="IEmployeePayrunResult"/> containing the results of the payroll calculations.</param>
    void Process(IEmployeePayrunInputEntry entry, out IEmployeePayrunResult result);
}
