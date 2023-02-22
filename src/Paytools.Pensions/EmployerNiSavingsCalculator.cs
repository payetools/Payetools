// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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
/// Represents a calculator that evaluates the amount of employer NI saving that can be re-invested in the
/// employee's pension under salary exchange.
/// </summary>
public class EmployerNiSavingsCalculator : IEmployerNiSavingsCalculator
{
    private readonly decimal _employerNiRate;

    /// <summary>
    /// Gets the percentage of employer's NI contribution saving that may be re-invested in the employee's
    /// pension.  Expressed in percentage points, i.e., 50% = 50.0m.
    /// </summary>
    public decimal EmployerNiSavingsReinvestmentPercentage { get; init; }

    /// <summary>
    /// Initialises a new instance of <see cref="EmployerNiSavingsCalculator"/> with the supplied employer NI
    /// rate and employer NI re-investment level.
    /// </summary>
    /// <param name="employerNiRate">Applicable employers NI rate.  Expressed as a fractional number, e.g., 13.8% = 0.138m.</param>
    /// <param name="employerNiSavingsReinvestmentPercentage">Percentage of employer's NI contribution saving that may be re-invested
    /// in the employee's pension, expressed in percentage points, i.e., 50% = 50.0m.</param>
    public EmployerNiSavingsCalculator(decimal employerNiRate, decimal employerNiSavingsReinvestmentPercentage)
    {
        _employerNiRate = employerNiRate;
        EmployerNiSavingsReinvestmentPercentage = employerNiSavingsReinvestmentPercentage;
    }

    /// <summary>
    /// Calculates the amount that should be added to the employer-only pension contribution under salary
    /// exchange as a result of employer's NI savings.
    /// </summary>
    /// <param name="salaryExchanged">Amount of salary that has been exchanged.</param>
    /// <returns>Amount to be added to the employer-only pension contribution.</returns>
    public decimal Calculate(decimal salaryExchanged)
    {
        return decimal.Round(salaryExchanged * _employerNiRate * (EmployerNiSavingsReinvestmentPercentage / 100.0m),
            2, MidpointRounding.AwayFromZero);
    }
}