// Copyright (c) 2023 Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Common.Model;

/// <summary>
/// Interface for reference data that indicates the period that a reference data applies for.
/// </summary>
public interface IApplicableFromTill
{
    /// <summary>
    /// Gets the start date (i.e., the first full day) for applicability.  Use DateOnly.MinValue to
    /// indicate there is no effective start date.
    /// </summary>
    public DateOnly ApplicableFrom { get; }

    /// <summary>
    /// Gets the end date (i.e., the last full day) for applicability.  Use DateOnly.MaxValue to
    /// indicate there is no effective end date.
    /// </summary>
    public DateOnly ApplicableTill { get; }
}
