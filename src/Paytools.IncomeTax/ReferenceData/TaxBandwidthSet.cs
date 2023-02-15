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

namespace Paytools.IncomeTax.ReferenceData;

/// <summary>
/// Represents a set of tax bandwidths for a given tax regime for a given tax year.
/// </summary>
public record TaxBandwidthSet
{
    /// <summary>
    /// Gets the set of <see cref="TaxBandwidthEntry"/>'s for this TaxBandwidthSet as an array.
    /// </summary>
    public TaxBandwidthEntry[] TaxBandwidthEntries { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="TaxBandwidthSet"/>.
    /// </summary>
    /// <param name="taxBandwidthEntries">Array of <see cref="TaxBandwidthEntry"/>'s for this tax year/regime combination.</param>
    public TaxBandwidthSet(TaxBandwidthEntry[] taxBandwidthEntries)
    {
        TaxBandwidthEntries = taxBandwidthEntries;
    }
}