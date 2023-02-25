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

using Paytools.NationalInsurance;
using Paytools.NationalInsurance.ReferenceData;
using System.Collections.Immutable;

namespace Paytools.ReferenceData.NationalInsurance;

/// <summary>
/// Represents a set of National Insurance thresholds as defined by HMRC for a given tax year or portion of a tax year.
/// </summary>
public class NiThresholdSet : INiThresholdSet
{
    private readonly ImmutableList<NiThresholdEntry> _niThresholds;

    /// <summary>
    /// Gets the number of threshold value this threshold set contains.
    /// </summary>
    public int Count => _niThresholds.Count;

    /// <summary>
    /// Gets the <see cref="INiThresholdEntry"/> at the specified index.
    /// </summary>
    /// <param name="index">Zero-based index into list.</param>
    /// <returns>The <see cref="INiThresholdEntry"/> for the specified index.</returns>
    public INiThresholdEntry this[int index]
    {
        get
        {
            if (index < 0 || index > _niThresholds.Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index value for retrieving NI threshold");

            return _niThresholds[index];
        }
    }

    /// <summary>
    /// Initialises a new instance of <see cref="NiThresholdSet"/>.
    /// </summary>
    /// <param name="niThresholds">Immutable input list of thresholds.</param>
    /// <exception cref="ArgumentException">Thrown if the number of thresholds supplied does not match the expected
    /// number of possible thresholds.</exception>
    public NiThresholdSet(ImmutableList<NiThresholdEntry> niThresholds)
    {
        int entryCount = (int)NiThresholdType.Count;

        if (niThresholds.Count != entryCount)
            throw new ArgumentException($"Expected {entryCount} threshold entries but only {niThresholds.Count} supplied in input list", nameof(niThresholds));

        _niThresholds = niThresholds;
    }
}
