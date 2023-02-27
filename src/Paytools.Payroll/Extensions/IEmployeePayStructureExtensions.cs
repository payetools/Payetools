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
using Paytools.Employment.Model;

namespace Paytools.Payroll.Extensions;

/// <summary>
/// Extension methods for <see cref="IEmployeePayStructure"/>.
/// </summary>
public static class IEmployeePayStructureExtensions
{
    /// <summary>
    /// Calculates the pay for one period at the specified pay frequency.
    /// </summary>
    /// <param name="value"><see cref="IEmployeePayStructure"/> instance.</param>
    /// <param name="payFrequency">Pay frequency.</param>
    /// <returns>Pay for the period.</returns>
    /// <exception cref="ArgumentException">Thrown if the pay structure does not refer to an annually paid
    /// employee.</exception>
    public static decimal GetPayForSinglePeriod(this IEmployeePayStructure value, PayFrequency payFrequency)
    {
        if (value.PayRateType != PayRateType.Salaried)
            throw new ArgumentException("Pay for single period can only be calculated for annually paid employees", nameof(value));

        return value.PayRate / payFrequency.GetStandardTaxPeriodCount();
    }

    /// <summary>
    /// Calculates the pay for the period based on the specified amount of time/unit. Currently only supports
    /// hourly pay.
    /// </summary>
    /// <param name="value"><see cref="IEmployeePayStructure"/> instance.</param>
    /// <param name="quantity">Amount of time/unit to calculate pay for.</param>
    /// <param name="units">Units for quantity.</param>
    /// <returns>Pay for the period.</returns>
    public static decimal GetPayForPeriod(this IEmployeePayStructure value, decimal quantity, PayRateUnits units)
    {
        if (value.PayRateType != PayRateType.HourlyPaid || units != PayRateUnits.PerHour)
            throw new ArgumentException("Pay rate type must be hourly paid and units specified must be per hour", nameof(units));

        return quantity * value.PayRate;
    }
}
