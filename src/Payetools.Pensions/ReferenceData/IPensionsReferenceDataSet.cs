// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Pensions.ReferenceData;

/// <summary>
/// Interface that classes must implement to provide access to pensions reference data, for example, lower and upper thesholds
/// for Qualifying Earnings.
/// </summary>
public interface IPensionsReferenceDataSet
{
    /// <summary>
    /// Gets the lower level for Qualifying Earnings for each pay frequency.
    /// </summary>
    IPensionsThresholdEntry LowerLevelForQualifyingEarnings { get; }

    /// <summary>
    /// Gets the earnings trigger for Auto-Enrolment for each pay frequency.
    /// </summary>
    IPensionsThresholdEntry EarningsTriggerForAutoEnrolment { get; }

    /// <summary>
    /// Gets the upper level for Qualifying Earnings for each pay frequency.
    /// </summary>
    IPensionsThresholdEntry UpperLevelForQualifyingEarnings { get; }
}