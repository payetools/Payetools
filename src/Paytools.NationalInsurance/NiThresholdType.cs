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

namespace Paytools.NationalInsurance;

/// <summary>
/// Enum enumerating the various National Insurance threshold levels.
/// </summary>
public enum NiThresholdType
{
    /// <summary>Lower earnings limit</summary>
    LEL,

    /// <summary>Primary threshold</summary>
    PT,

    /// <summary>Secondary threshold</summary>
    ST,

    /// <summary>Freeport upper secondary threshold</summary>
    FUST,

    /// <summary>Upper secondary threshold</summary>
    UST,

    /// <summary>Apprentice upper secondary threshold</summary>
    AUST,

    /// <summary>Veterans upper secondary threshold</summary>
    VUST,

    /// <summary>Upper earnings limit</summary>
    UEL,

    /// <summary>Number of elements in enum</summary>
    Count = 8
}

public static class NationalInsuranceThresholdExtensions
{
    public static int GetIndex(this NiThresholdType threshold) =>
        (int)threshold;
}