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
/// Represents a deduction from payroll.
/// </summary>
public record DeductionEntry
{
    /// <summary>
    /// Gets the type of deduction.
    /// </summary>
    public IDeductionType DeductionType { get; init; } = default!;

    /// <summary>
    /// Gets the quantity of this deduction that when multiplied by the <see cref="ValuePerUnit"/> gives the total deduction.
    /// </summary>
    public decimal? QuantityInUnits { get; init; }

    /// <summary>
    /// Gets the GBP value per unit that when multiplied by the <see cref="QuantityInUnits"/> gives the total deduction.
    /// </summary>
    public decimal? ValuePerUnit { get; init; }

    /// <summary>
    /// Gets the fixed amount of the deduction, if that is specified in place of quantity and value per unit.  Used for absolute
    /// amounts.
    /// </summary>
    public decimal? FixedAmount { get; init; }

    /// <summary>
    /// Gets the total deduction to be applied.
    /// </summary>
    public decimal TotalDeduction => FixedAmount ?? QuantityInUnits * ValuePerUnit ?? 0.0m;
}
