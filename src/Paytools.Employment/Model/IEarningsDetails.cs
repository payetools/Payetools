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
/// Interface that represents a pay component (e.g., salary, bonus, sick pay, etc.).
/// </summary>
public interface IEarningsDetails
{
    /// <summary>
    /// Gets the unique ID for this pay component.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the short name for this pay component.
    /// </summary>
    string ShortName { get; }

    /// <summary>
    /// Gets the full name of this pay component.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the units for this pay component, if applicable.  Null if not applicable.
    /// </summary>
    PayRateUnits? Units { get; }

    /// <summary>
    /// Gets a value indicating whether this pay component is subject to tax.
    /// </summary>
    bool IsSubjectToTax { get; }

    /// <summary>
    /// Gets a value indicating whether this pay component is subject to National Insurance.
    /// </summary>
    bool IsSubjectToNi { get; }

    /// <summary>
    /// Gets a value indicating whether this pay component should be included in the employee's
    /// pensionable salary.
    /// </summary>
    bool IsPensionable { get; }

    /// <summary>
    /// Gets a value indicating whether this pay component refers to a net amount that should be
    /// "grossed up", ensuring the employee receives the net amount in their take-home pay.
    /// </summary>
    bool IsNetToGross { get; }

    /// <summary>
    /// Gets a value indicating whether this pay component should be treated as overtime for the
    /// purposes of average overtime calculations.
    /// </summary>
    bool IsTreatedAsOvertime { get; }
}
