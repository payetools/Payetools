﻿// Copyright (c) 2023 Paytools Foundation.
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

namespace Paytools.NationalInsurance.Model;

/// <summary>
/// Represents the method of director's NI calculation to be applied when calculating
/// National Insurance contributions for a director.
/// </summary>
public enum DirectorsNiCalculationMethod
{
    /// <summary>
    /// Standard annualised earnings method; common for directors who are paid irregularly.
    /// </summary>
    StandardAnnualisedEarningsMethod,

    /// <summary>
    /// Alternativee method; common for directors who are paid regularly.
    /// </summary>
    AlternativeMethod
}