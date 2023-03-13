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

using Paytools.Common.Extensions;
using Paytools.Common.Model;
using Paytools.NationalMinimumWage;

namespace Paytools.Employment.Model;

/// <summary>
/// Represents an hourly paid pay structure that tracks National Minimum/Living Wage
/// levels.
/// </summary>
public record NmwHourlyPaidPayStructure : IEmployeePayStructure
{
    /// <inheritdoc/>>
    public Guid Id { get; init; }

    /// <inheritdoc/>>
    public decimal PayRate { get; private set; }

    /// <inheritdoc/>>
    public PayRateType PayRateType => PayRateType.HourlyPaid;

    /// <inheritdoc/>>
    public IEarningsDetails PayComponent { get; init; } = default!;

    /// <summary>
    /// Updates the <see cref="PayRate"/> based on the applicable NMW/NLW wage rate for the employee
    /// using their age at the start of the pay period.
    /// </summary>
    /// <param name="nmwEvaluator">Instance of <see cref="INmwEvaluator"/> used to obtain the appropriate rate.</param>
    /// <param name="payPeriod">Pay period pertaining.</param>
    /// <param name="dateOfBirth">Employee's date of birth.</param>
    public void UpdateNmw(INmwEvaluator nmwEvaluator, PayReferencePeriod payPeriod, DateOnly dateOfBirth)
    {
        var age = dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod);

        PayRate = nmwEvaluator.GetNmwHourlyRateForEmployee(age);
    }
}