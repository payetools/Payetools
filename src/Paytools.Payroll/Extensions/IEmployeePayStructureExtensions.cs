// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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