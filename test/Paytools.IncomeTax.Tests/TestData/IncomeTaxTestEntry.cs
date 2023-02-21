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

using Paytools.Common.Model;

namespace Paytools.IncomeTax.Tests.TestData;

public class IncomeTaxTestEntry
{
    public string Description { get; set; } = default!;
    public decimal GrossPay { get; set; }
    public decimal TaxablePayToDate { get; set; }
    public string TaxCode { get; set; } = default!;
    public string Wk1 { get; set; } = default!;
    public int Period { get; set; }
    public decimal TaxDue { get; set; }
    public decimal TaxDueToDate { get; set; }
    public PayFrequency PayFrequency { get; set; }

    public bool IsNonCumulative => Wk1 == "WM1";
}
