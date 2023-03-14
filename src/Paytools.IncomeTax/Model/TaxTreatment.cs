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

namespace Paytools.IncomeTax.Model;

/// <summary>
/// Enum representing the tax treatment aspect of the tax code.
/// </summary>
public enum TaxTreatment
{
    /// <summary>Not specified.</summary>
    Unspecified,

    /// <summary>NT - no tax payable.</summary>
    NT,

    /// <summary>BR - basic rate tax to be applied to all taxable earnings.</summary>
    BR,

    /// <summary>D0 - next highest rate above BR to be applied to all taxable earnings.</summary>
    D0,

    /// <summary>D1 - next highest rate above D0 to be applied to all taxable earnings.</summary>
    D1,

    /// <summary>D2 - next highest rate above D1 to be applied to all taxable earnings.</summary>
    D2,

    /// <summary>Zero personal allowance to be applied.</summary>
    _0T,

    /// <summary>Additional notional taxable income must be applied to existing taxable earnings.</summary>
    K,

    /// <summary>Tax payer is entitled to the standard Personal Allowance.</summary>
    L,

    /// <summary>Married Allowance where individual has received a transfer of 10% of their partner’s Personal Allowance.</summary>
    M,

    /// <summary>Married Allowance where individual has transferred 10% of their Personal Allowance to their partner.</summary>
    N
}

/// <summary>
/// Extension methods for <see cref="TaxTreatment"/>.
/// </summary>
public static class TaxTreatmentExtensions
{
    /// <summary>
    /// Gets the zero-based index of the band that a given tax code (BR, D0, D1, D2) applies to.
    /// </summary>
    /// <param name="taxTreatment">Tax treatment to determine band index for.</param>
    /// <returns>Band index for supplied tax treatment.</returns>
    /// <exception cref="ArgumentException">Thrown if it is not possible to retrieve a band index for ththe supplied tax treatment.</exception>
    public static int GetBandIndex(this TaxTreatment taxTreatment) =>
        taxTreatment switch
        {
            TaxTreatment.BR => 0,
            TaxTreatment.D0 => 1,
            TaxTreatment.D1 => 2,
            TaxTreatment.D2 => 3,
            _ => throw new ArgumentException($"Band index not valid for tax treatment {taxTreatment}", nameof(taxTreatment))
        };
}