// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.NationalInsurance.ReferenceData;

/// <summary>
/// Interface for types that detail the various HMRC-published National Insurance threshold levels.
/// </summary>
public interface INiCategoryRatesEntry
{
    /// <summary>Gets the applicable National Insurance Category.</summary>
    NiCategory Category { get; }

    /// <summary>Gets the employee rate for earnings at or above lower earnings limit up to and including primary threshold.</summary>
    decimal EmployeeRateToPT { get; }

    /// <summary>Gets the employee rate for earnings above the primary threshold up to and including upper earnings limit.</summary>
    decimal EmployeeRatePTToUEL { get; }

    /// <summary>Gets the employee rate for balance of earnings above upper earnings limit.</summary>
    decimal EmployeeRateAboveUEL { get; }

    /// <summary>Gets the employer rate for earnings at or above lower earnings limit up to and including secondary threshold.</summary>
    decimal EmployerRateLELtoST { get; }

    /// <summary>Gets the employer rate for earnings above secondary threshold up to and including Freeport upper secondary threshold.</summary>
    decimal EmployerRateSTtoFUST { get; }

    /// <summary>Gets the employer rate for earnings above Freeport upper secondary threshold up to and including upper earnings limit, upper
    /// secondary thresholds for under 21s, apprentices and veterans.</summary>
    decimal EmployerRateFUSTtoUEL { get; }

    /// <summary>Gets the employer rate for balance of earnings above upper earnings limit, upper secondary thresholds for under 21s, apprentices
    /// and veterans.</summary>
    decimal EmployerRateAboveUEL { get; }
}