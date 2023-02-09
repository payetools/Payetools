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

using System.Text.RegularExpressions;

namespace Paytools.Common.Model;



/// <summary>
/// Represents an HMRC Accounts Office Reference  
/// The Accounts Office reference is formatted as follows:
///    * 3 numbers
///    * ‘P’
///    * a letter
///    * 8 numbers or 7 numbers and an ‘X’
///  (from https://design.tax.service.gov.uk/hmrc-design-patterns/accounts-office-reference/)
/// </summary>
public record HmrcAccountsOfficeReference
{
    private static readonly Regex _validationRegex = 
        new Regex(@"^[0-9]{3}P[A-Z]\d{7}[0-9X]$");

    private readonly string _accountsOfficeReference;

    public static implicit operator string(HmrcAccountsOfficeReference r) => r.ToString();

    public HmrcAccountsOfficeReference(string accountsOfficeReference)
    {
        var aor = accountsOfficeReference.ToUpper();

        if (!IsValid(aor))
            throw new ArgumentException("Argument is not a valid Accounts Office Reference", nameof(accountsOfficeReference));

        _accountsOfficeReference = aor;
    }

    public static bool IsValid(string value) => _validationRegex.IsMatch(value);

    public override string ToString() => _accountsOfficeReference;
}