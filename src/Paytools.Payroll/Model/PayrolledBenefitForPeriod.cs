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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paytools.Payroll.Model;

/// <summary>
/// Represents a payrolled benefit as applicable to one payroll period.
/// </summary>
public record PayrolledBenefitForPeriod : IPayrolledBenefitForPeriod
{
    /// <summary>
    /// Gets the amount of benefit to apply for the period.
    /// </summary>
    public decimal AmountForPeriod { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="PayrolledBenefitForPeriod"/>.
    /// </summary>
    /// <param name="amountForPeriod">Amount of the benefit for the period.</param>
    public PayrolledBenefitForPeriod(decimal amountForPeriod)
    {
        AmountForPeriod = amountForPeriod;
    }
}
