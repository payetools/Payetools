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

namespace Paytools.Pensions.Model;

/// <summary>
/// Interface for types that represent pension schemes.
/// </summary>
/// <remarks>A <see cref="IPensionScheme"/> instance refers to a pension scheme with a specific provider, with
/// specific tax treatment and earnings basis.  Whilst it is not common for pension schemes to change
/// tax treatment, it is quite possible for an employer to operate more than one type of earnings basis
/// across its employee base.  In this case, two (or more) instances of this type would be required, one
/// for each earnings basis in use, even though all contributions might be flowing to a single scheme
/// with the same provider.</remarks>
public interface IPensionScheme
{
    /// <summary>
    /// Gets the earnings basis for this pension scheme.
    /// </summary>
    EarningsBasis EarningsBasis { get; }

    /// <summary>
    /// Gets the tax treatment for this pension scheme.
    /// </summary>
    PensionTaxTreatment TaxTreatment { get; }
}
