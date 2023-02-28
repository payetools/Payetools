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

using Paytools.NationalMinimumWage.ReferenceData;

namespace Paytools.ReferenceData.NationalMinimumWage;

/// <summary>
/// Represents the set of NMW/NLW levels for a given tax year (and potentially pay frequency/pay period combination.
/// </summary>
public class NmwReferenceDataEntry : INmwLevelSet, IApplicableFromTill
{
    /// <summary>
    /// Gets the start date (i.e., the first full day) for applicability.
    /// </summary>
    public DateOnly ApplicableFrom { get; init; }

    /// <summary>
    /// Gets the end date (i.e., the last full day) for applicability.
    /// </summary>
    public DateOnly ApplicableTill { get; init; }

    /// <summary>
    /// Gets the NMW level for apprentices under 19 or apprentices aged 19 and over in the first year of their
    /// apprenticeship.
    /// </summary>
    public decimal ApprenticeLevel { get; init; }

    /// <summary>
    /// Gets the NMW level for employees under the age of 18 (but over the school leaving age).
    /// </summary>
    public decimal Under18Level { get; init; }

    /// <summary>
    /// Gets the NMW level for employees aged between 18 and 20.
    /// </summary>
    public decimal Age18To20Level { get; init; }

    /// <summary>
    /// Gets the NMW level for employees aged between 21 and 22.
    /// </summary>
    public decimal Age21To22Level { get; init; }

    /// <summary>
    /// Gets the NLW (rather than NMW) level for employees aged 23 and over.
    /// </summary>
    public decimal Age23AndAboveLevel { get; init; }
}
