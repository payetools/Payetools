// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Paytools.Common.Model;

/// <summary>
/// Enum representing a given tax year based on the last day of the tax year (i.e., 5 April 20xx).
/// </summary>
/// <remarks>This enumeration is updated each tax year to provide access to the forthcoming tax year.</remarks>
public enum TaxYearEnding
{
    /// <summary>No tax year specified</summary>
    Unspecified,

    /// <summary>2018-2019</summary>
    Apr5_2019 = 2019,

    /// <summary>2019-2020</summary>
    Apr5_2020 = 2020,

    /// <summary>2020-2021</summary>
    Apr5_2021 = 2021,

    /// <summary>2021-2022</summary>
    Apr5_2022 = 2022,

    /// <summary>2022-2023</summary>
    Apr5_2023 = 2023,

    /// <summary>2023-2024</summary>
    Apr5_2024 = 2024,

    /// <summary>Minimum value supported for TaxYearEnding</summary>
    MinValue = 2019,

    /// <summary>Maximum value supported for TaxYearEnding</summary>
    MaxValue = 2024
}

/// <summary>
/// Extension methods for TaxYearEnding enum.
/// </summary>
public static class TaxYearEndingExtensions
{
    /// <summary>
    /// Converts a TaxYearEnding enumerated value into a string.
    /// </summary>
    /// <param name="value">An instance of TaxYearEnding.</param>
    /// <returns>Year as string, e.g., "2020", indicating the year that the tax year ends in.</returns>
    public static string YearAsString(this TaxYearEnding value) =>
        $"{(int)value:0000}";
}