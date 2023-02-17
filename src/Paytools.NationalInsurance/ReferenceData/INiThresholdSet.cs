// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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

namespace Paytools.NationalInsurance.ReferenceData;

/// <summary>
/// Interface for types that represent a list of National Insurance thresholds as defined by HMRC for a given
/// tax year or portion of a tax year.  The list isn't specifically ordered; the items within the list provide
/// their own mapping to <see cref="NiThresholdType"/> values.
/// </summary>
public interface INiThresholdSet
{
    /// <summary>
    /// Gets the <see cref="INiThresholdEntry"/> at the specified index.
    /// </summary>
    /// <param name="index">Zero-based index into list.</param>
    /// <returns>The <see cref="INiThresholdEntry"/> for the specified index.</returns>
    INiThresholdEntry this[int index] { get; }

    /// <summary>
    /// Gets the number of threshold value this threshold set contains.
    /// </summary>
    int Count { get; }
}
