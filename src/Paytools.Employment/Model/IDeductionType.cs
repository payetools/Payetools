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

namespace Paytools.Employment.Model;

/// <summary>
/// Interface that represents the various types of deduction that can be made from payroll.
/// </summary>
public interface IDeductionType
{
    /// <summary>
    /// Gets the short name for this type of deduction.
    /// </summary>
    string ShortName { get; }

    /// <summary>
    /// Gets the full name of this type of deduction.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the units for this deduction type, if applicable.  Null if not applicable.
    /// </summary>
    PayRateUnits? Units { get; }

    /// <summary>
    /// Gets a value indicating whether this type of deduction is applied before or after tax.
    /// </summary>
    bool ApplyBeforeTax { get; }
}