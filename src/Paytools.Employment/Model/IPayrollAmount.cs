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
/// Interface that types should implement to communicate an amount that is either fixed or calculated
/// as a product of quantity in units and per unit rate.
/// </summary>
public interface IPayrollAmount
{
    /// <summary>
    /// Gets the quantity of this deduction that when multiplied by the <see cref="ValuePerUnit"/> gives the total amount.
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
    /// Gets the total amount to be applied (default implementation).
    /// </summary>
    public decimal TotalDeduction => FixedAmount ?? QuantityInUnits * ValuePerUnit ?? 0.0m;
}