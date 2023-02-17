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
    /// <returns>NI contributions due via an instance of a type that implements <see cref="INiCalculationResult"/>.</returns>
    INiCalculationResult Calculate(NiCategory niCategory, decimal nicableEarningsInPeriod);

    /// <summary>
    /// Calculates the National Insurance contributions required for a company director for a given pay period,
    /// based on their NI-able salary and their allocated National Insurance category letter.
    /// </summary>
    /// <param name="niCategory">National Insurance category.</param>
    /// <param name="nicableEarningsInPeriod">NI-able salary for the period.</param>
    /// <returns>NI contributions due via an instance of a type that implements <see cref="INiCalculationResult"/>.</returns>
    INiCalculationResult CalculateDirectors(NiCategory niCategory, decimal nicableEarningsInPeriod);
}