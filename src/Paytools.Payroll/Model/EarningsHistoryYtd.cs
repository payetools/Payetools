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
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paytools.Payroll.Model;

/// <summary>
/// Represents an employee's earnings history for the tax year to date.
/// </summary>
public record EarningsHistoryYtd : IEarningsHistoryYtd
{
    /// <summary>
    /// Gets the list of pay components for this employee for a given payrun.  May be empty but usually not.
    /// </summary>
    public ImmutableList<IEarningsEntry> Earnings { get; }

    /// <summary>
    /// Initialises a new empoty <see cref="EarningsHistoryYtd"/>.
    /// </summary>
    public EarningsHistoryYtd()
    {
        Earnings = ImmutableList<IEarningsEntry>.Empty;
    }
}
