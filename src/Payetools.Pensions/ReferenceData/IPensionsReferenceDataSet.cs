// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
