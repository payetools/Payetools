// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.NationalInsurance.Extensions;

/// <summary>
/// Extension methods for <see cref="decimal"/> instances to provide specialised rounding as per HMRC documentation.
/// </summary>
public static class DecimalNiRoundingExtensions
{
    /// <summary>
    /// Rounds a decimal value based on the value of the third digit after the decimal point.  If the value of the third digit is 6 or above,
    /// the number is rounded up to the nearest whole pence; if the value of the third digit is 5 or below, the number is rounded down.
    /// </summary>
    /// <param name="value"><see cref="decimal"/> value to be rounded.</param>
    /// <returns>Rounded decimal value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if a negative number is supplied for value.</exception>
    /// <remarks>This extension method is included to support the specific rounding required for NI calculations as set out
    /// in the 'National Insurance contributions (NICs) guidance for software developers' document published by HMRC.</remarks>
    public static decimal NiRound(this decimal value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "decimal.NiRound() only supports rounding of positive numbers");

        var truncatedValue = decimal.Round(value, 2, MidpointRounding.ToZero);
        var fractionsOfPence = decimal.Round(value, 3, MidpointRounding.ToZero) - truncatedValue;

        return fractionsOfPence <= 0.005m ? truncatedValue : decimal.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}