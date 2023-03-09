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
/// Represents an element of an employee's pay.
/// </summary>
public interface IEarningsEntry
{
    /// <summary>
    /// Gets the type of deduction.
    /// </summary>
    IPayComponent EarningsType { get; }

    /// <summary>
    /// Gets the quantity of this earnings entry that when multiplied by the <see cref="ValuePerUnit"/> gives the total earnings.
    /// </summary>
    decimal? QuantityInUnits { get; }

    /// <summary>
    /// Gets the GBP value per unit that when multiplied by the <see cref="QuantityInUnits"/> gives the total earnings.
    /// </summary>
    decimal? ValuePerUnit { get; }

    /// <summary>
    /// Gets the total earnings to be applied.
    /// </summary>
    decimal TotalEarnings { get; }
}
