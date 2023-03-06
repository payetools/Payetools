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

namespace Paytools.NationalInsurance;

/// <summary>
/// Interface for types that provide National Insurance calculations.
/// </summary>
public interface INiCalculator
{
    /// <summary>
    /// Calculates the National Insurance contributions required for an employee for a given pay period,
    /// based on their NI-able salary and their allocated National Insurance category letter.
    /// </summary>
    /// <param name="niCategory">National Insurance category.</param>
    /// <param name="nicableEarningsInPeriod">NI-able salary for the period.</param>
    /// <param name="result">The NI contributions due via an instance of a type that implements <see cref="INiCalculationResult"/>.</param>
    void Calculate(NiCategory niCategory, decimal nicableEarningsInPeriod, out INiCalculationResult result);

    /// <summary>
    /// Calculates the National Insurance contributions required for a company director for a given pay period,
    /// based on their NI-able salary and their allocated National Insurance category letter.
    /// </summary>
    /// <param name="calculationMethod">Calculation method to use.</param>
    /// <param name="niCategory">National Insurance category.</param>
    /// <param name="nicableEarningsYearToDate">NI-able salary for the period.</param>
    /// <param name="employeesNiPaidYearToDate">Total employees NI paid so far this tax year up to and including the end of the
    /// previous period.</param>
    /// <param name="employersNiPaidYearToDate">Total employers NI paid so far this tax year up to and including the end of the
    /// previous period.</param>
    /// <param name="proRataFactor">Factor to apply to annual thresholds when the employee starts being a director part way through
    /// the tax year.  Null if not applicable.</param>
    /// <param name="result">The NI contributions due via an instance of a type that implements <see cref="INiCalculationResult"/>.</param>
    void CalculateDirectors(
        DirectorsNiCalculationMethod calculationMethod,
        NiCategory niCategory,
        decimal nicableEarningsYearToDate,
        decimal employeesNiPaidYearToDate,
        decimal employersNiPaidYearToDate,
        decimal? proRataFactor,
        out INiCalculationResult result);
}