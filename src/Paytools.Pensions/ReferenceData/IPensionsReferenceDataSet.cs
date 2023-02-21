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

namespace Paytools.Pensions.ReferenceData;

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
