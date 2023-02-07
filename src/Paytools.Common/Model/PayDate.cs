// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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
using System.Runtime.CompilerServices;

namespace Paytools.Common.Model;

public readonly struct PayDate
{
    public DateOnly Date { get; init; }
    public TaxYear TaxYear { get; init; }
    public int TaxPeriod { get; init; }

    public static implicit operator DateTime(PayDate payDate) => payDate.Date.ToMiddayUtcDateTime();

    public PayDate(DateOnly date, PayFrequency payFrequency)
    {
        Date = date;
        TaxYear = new TaxYear(date);
        TaxPeriod = TaxYear.GetTaxPeriod(date, payFrequency);
    }

    public PayDate(int year, int month, int day, PayFrequency payFrequency)
        : this(new DateOnly(year,month,day), payFrequency)
    {
    }
}