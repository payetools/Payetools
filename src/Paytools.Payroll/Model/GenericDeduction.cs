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
/// Represents the various types of deduction that can be made from payroll.
/// </summary>
public record GenericDeduction : IDeduction
{
    /// <summary>
    /// Gets or sets the short name for this type of deduction.
    /// </summary>
    public string ShortName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the full name of this type of deduction.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the units for this deduction type, if applicable.  Null if not applicable.
    /// </summary>
    public PayRateUnits? Units { get; set; }

    /// <summary>
    /// Gets a value indicating whether this type of deduction reduces the gross pay figure used
    /// to calculate take-home .
    /// </summary>
    public bool ReducesGrossPay { get; }

    /// <summary>
    /// Gets or sets a value indicating whether this type of deduction is applied before or after tax.
    /// </summary>
    public bool ReducesTaxablePay { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this type of deduction affects pay for National Insurance
    /// purposes.
    /// </summary>
    public bool ReducesNicablePay { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this type of deduction affects pay for pension
    /// purposes.
    /// </summary>
    public bool ReducesPensionablePay { get; set; }
}
