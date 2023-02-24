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
/// Enum that represents the tax treatment to be applied to employee pension contributions.
/// </summary>
public enum PensionTaxTreatment
{
    /// <summary>Not specified.</summary>
    Unspecified,

    /// <summary>Relief at source.  Contributions are taken from post-tax salary and the pension
    /// provider claims back basic rate tax from HMRC.</summary>
    ReliefAtSource,

    /// <summary>
    /// Net pay arrangement.  Contributions are taken from salary before tax.
    /// </summary>
    NetPayArrangement
}