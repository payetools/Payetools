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

namespace Paytools.Pensions;

/// <summary>
/// Represents the earnings basis which is used to calculate pension contributions.
/// </summary>
public enum EarningsBasis
{
    /// <summary>Not specified.</summary>
    Unspecified,

    /// <summary>Qualifying Earnings includes salary, wages, commission, bonuses, overtime, statutory sick pay, statutory maternity pay,
    /// ordinary or additional statutory paternity pay and statutory adoption pay.
    ///
    /// Qualifying earnings is calculated from pensionable pay deducting a lower threshold and capping at an upper threshold, the
    /// thresholds being decided by Government each year.
    /// </summary>
    QualifyingEarnings,

    /// <summary>Pensionable Pay Set 1 contributions are worked out on at least basic pay. Basic pay includes at a minimum, earnings
    /// before deductions, holiday pay and statutory benefits such as maternity, paternity, adoption and sick pay delivered through
    /// payroll.</summary>
    PensionablePaySet1,

    /// <summary>Pensionable Pay Set 2 contributions are worked out on at least basic pay, but the key difference between this and
    /// Set 1 is that basic pay must make up at least 85% of total earnings. The employer must monitor this.</summary>
    PensionablePaySet2,

    /// <summary>Pensionable Pay Set 2 contributions includes salary, wages, commission, bonuses, overtime, statutory sick pay,
    /// statutory maternity pay, ordinary or additional statutory paternity pay and statutory adoption pay.</summary>
    PensionablePaySet3
}