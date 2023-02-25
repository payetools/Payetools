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

namespace Paytools.ReferenceData;

/// <summary>
/// Interface for reference data that indicates the period that a reference data applies for.
/// </summary>
public interface IApplicableBetween
{
    /// <summary>
    /// Gets the start date (i.e., the first full day) for applicability.
    /// </summary>
    public DateOnly ApplicableFrom { get; }

    /// <summary>
    /// Gets the end date (i.e., the last full day) for applicability.
    /// </summary>
    public DateOnly ApplicableTill { get; }
}
