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

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Paytools.Common.Model;

// Use a single text input and get the user to enter the reference formatted as:
//   * 3 numbers
//   * a forward slash(/)
//   * between 1 and 10 characters, which can be letters and numbers
// (from https://design.tax.service.gov.uk/hmrc-design-patterns/employer-paye-reference/)
public struct HmrcPayeReference
{
    private static readonly Regex _payeRefRegex = new Regex(@"^[0-9]{3}/[A-Z0-9]{1,10}$");

    public int HmrcOfficeNumber { get; set; }
    public string EmployerPayeReference { get; set; }

    public static implicit operator string(HmrcPayeReference r) => $"{r.HmrcOfficeNumber}/{r.EmployerPayeReference}";


    public HmrcPayeReference(int hmrcOfficeNumber, string employerPayReference)
    {
        HmrcOfficeNumber = hmrcOfficeNumber;
        EmployerPayeReference = employerPayReference;
    }

    public static bool TryParse([NotNullWhen(true)] string? input, out HmrcPayeReference payeReference)
    {
        if (IsValid(input))
        {
            var tokens = string.IsNullOrEmpty(input) ? null :
                input.Split('/', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (tokens != null && tokens.Length == 2 &&
                int.TryParse(tokens[0], out var officeNumber) && tokens[1].Length <= 10)
            {
                payeReference = new HmrcPayeReference(officeNumber, tokens[1]);

                return true;
            }
        }

        payeReference = default;

        return false;
    }

    public static bool IsValid(string? value) => value != null && _payeRefRegex.IsMatch(value);

    public override string ToString() => $"{HmrcOfficeNumber:000}/{EmployerPayeReference}";
}
