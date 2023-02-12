// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
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

namespace Paytools.Common.Model;

/// <summary>
/// Represents a specific pay date for a specific pay frequency.
/// </summary>
public readonly struct PayDate
{
    /// <summary>
    /// Gets the date of this <see cref="PayDate"/>.
    /// </summary>
    public DateOnly Date { get; init; }

    /// <summary>
    /// Gets the <see cref="TaxYear"/> for this <see cref="PayDate"/>.
    /// </summary>

    public TaxYear TaxYear { get; init; }

    /// <summary>
    /// Gets the tax period for this <see cref="PayDate"/>, for example, a pay date of 20th May for a monthly
    /// payroll would be tax period 2.
    /// </summary>
    public int TaxPeriod { get; init; }

    /// <summary>
    /// Gets the equivalent <see cref="DateTime"/> for this paydate, with the time portion set to midday (12:00:00) UTC.
    /// </summary>
    /// <param name="payDate"><see cref="PayDate"/> to get the DateTime for.</param>
    public static implicit operator DateTime(PayDate payDate) => payDate.Date.ToMiddayUtcDateTime();

    /// <summary>
    /// Instantiates a new <see cref="PayDate"/> based on the supplied date and pay frequency.
    /// </summary>
    /// <param name="date">Pay date.</param>
    /// <param name="payFrequency">Pay frequency.</param>
    public PayDate(DateOnly date, PayFrequency payFrequency)
    {
        Date = date;
        TaxYear = new TaxYear(date);
        TaxPeriod = TaxYear.GetTaxPeriod(date, payFrequency);
    }

    /// <summary>
    /// Instantiates a new <see cref="PayDate"/> based on the supplied date and pay frequency.
    /// </summary>
    /// <param name="year">Year.</param>
    /// <param name="month">Month (1-12).</param>
    /// <param name="day">Day.</param>
    /// <param name="payFrequency">Pay frequency.</param>
    public PayDate(int year, int month, int day, PayFrequency payFrequency)
        : this(new DateOnly(year, month, day), payFrequency)
    {
    }
}