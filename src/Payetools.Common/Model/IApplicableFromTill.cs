// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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