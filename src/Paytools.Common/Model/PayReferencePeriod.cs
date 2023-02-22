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

namespace Paytools.Common.Model;

/// <summary>
/// Represents a pay reference period, i.e. the range of dates from the first day an individual is being paid for,
/// to the last day, inclusive.
/// </summary>
public readonly struct PayReferencePeriod
{
    /// <summary>
    /// Gets the first day of the pay period.
    /// </summary>
    public DateOnly StartOfPayPeriod { get; init; }

    /// <summary>
    /// Gets the last day of the pay period.
    /// </summary>
    public DateOnly EndOfPayPeriod { get; init; }

    /// <summary>
    /// Initialises a new instance of <see cref="PayReferencePeriod"/> with the supplied start and end dates.
    /// </summary>
    /// <param name="startOfPayPeriod">First day of pay period.</param>
    /// <param name="endOfPayPeriod">Last day of pay period.</param>
    public PayReferencePeriod(DateOnly startOfPayPeriod, DateOnly endOfPayPeriod)
    {
        StartOfPayPeriod = startOfPayPeriod;
        EndOfPayPeriod = endOfPayPeriod;
    }
}