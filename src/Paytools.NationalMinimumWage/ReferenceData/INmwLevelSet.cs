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

namespace Paytools.NationalMinimumWage.ReferenceData;

/// <summary>
/// Interface for types that detail the various HMRC-published National Minimum/Living Wage (NMW/NLW) levels.
/// </summary>
public interface INmwLevelSet
{
    /// <summary>
    /// Gets the NMW level for apprentices under 19 or apprentices aged 19 and over in the first year of their
    /// apprenticeship.
    /// </summary>
    decimal ApprenticeLevel { get; }

    /// <summary>
    /// Gets the NMW level for employees under the age of 18 (but over the school leaving age).
    /// </summary>
    decimal Under18Level { get; }

    /// <summary>
    /// Gets the NMW level for employees aged between 18 and 20.
    /// </summary>
    decimal Age18To20Level { get; }

    /// <summary>
    /// Gets the NMW level for employees aged between 21 and 22.
    /// </summary>
    decimal Age21To22Level { get; }

    /// <summary>
    /// Gets the NLW (rather than NMW) level for employees aged 23 and over.
    /// </summary>
    decimal Age23AndAboveLevel { get; }
}