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

namespace Paytools.Pensions;

/// <summary>
/// Interface that calculators must implement when evaluating the amount of employer NI saving that can be re-invested in the
/// employee's pension under salary exchange.
/// </summary>
public interface IEmployerNiSavingsCalculator
{
    /// <summary>
    /// Gets the percentage of employer's NI contribution saving that may be re-invested in the employee's
    /// pension.  Expressed in percentage points, i.e., 50% = 50.0m.
    /// </summary>
    decimal EmployerNiSavingsReinvestmentPercentage { get; }

    /// <summary>
    /// Calculates the amount that should be added to the employer-only pension contribution under salary
    /// exchange as a result of employer's NI savings.
    /// </summary>
    /// <param name="salaryExchanged">Amount of salary that has been exchanged.</param>
    /// <returns>Amount to be added to the employer-only pension contribution.</returns>
    decimal Calculate(decimal salaryExchanged);
}
