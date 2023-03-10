// Copyright (c) 2023 Paytools Foundation.
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
/// Represents the set of National Insurance category letters assigned by HMRC.  Taken from
/// https://www.gov.uk/national-insurance-rates-letters/category-letters (retrieved 5-Dec-2022).
/// </summary>
public enum NiCategory
{
    /// <summary>Not specified.</summary>
    Unspecified,

    /// <summary>All employees apart from those in groups B, C, H, J, M, V and Z, and those that work in
    /// freeports (F, I, L and S) and those that are exempt (X).</summary>
    A,

    /// <summary>Married women and widows entitled to pay reduced National Insurance.</summary>
    B,

    /// <summary>Employees over the State Pension age.</summary>
    C,

    /// <summary>All employees who work in freeports, apart from those in categories I, L, and S.</summary>
    F,

    /// <summary>Apprentices under 25.</summary>
    H,

    /// <summary>Married women and widows who work in freeports and are entitled to pay reduced National Insurance.</summary>
    I,

    /// <summary>Employees who can defer National Insurance because they’re already paying it in another job.</summary>
    J,

    /// <summary>Employees who work in freeports and can defer National Insurance because they’re already paying it in another job.</summary>
    L,

    /// <summary>Employees under 21.</summary>
    M,

    /// <summary>Employees who work in freeports and are over the State Pension age.</summary>
    S,

    /// <summary>Employees who are working in their first job since leaving the armed forces (veterans).</summary>
    V,

    /// <summary>Employees who do not have to pay National Insurance, for example because they’re under 16.</summary>
    X,

    /// <summary>Employees under 21 who can defer National Insurance because they’re already paying it in another job.</summary>
    Z
}