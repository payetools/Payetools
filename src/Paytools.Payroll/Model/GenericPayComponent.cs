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

using Paytools.Employment.Model;

namespace Paytools.Payroll.Model;

/// <summary>
/// Represents a generic pay component (e.g., salary, bonus, sick pay, etc.).
/// </summary>
public class GenericPayComponent : IPayComponent
{
    /// <summary>
    /// Gets or sets the unique ID for this pay component.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the short name for this pay component.
    /// </summary>
    public string ShortName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the full name of this pay component.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the units for this pay component, if applicable.  Null if not applicable.
    /// </summary>
    public PayRateUnits? Units { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pay component is subject to tax.
    /// </summary>
    public bool IsSubjectToTax { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pay component is subject to National Insurance.
    /// </summary>
    public bool IsSubjectToNi { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pay component should be included in the employee's
    /// pensionable salary.
    /// </summary>
    public bool IsPensionable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pay component refers to a net amount that should be
    /// "grossed up", ensuring the employee receives the net amount in their take-home pay.
    /// </summary>
    public bool IsNetToGross { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this pay component should be treated as overtime for the
    /// purposes of average overtime calculations.
    /// </summary>
    public bool IsTreatedAsOvertime { get; set; }
}
