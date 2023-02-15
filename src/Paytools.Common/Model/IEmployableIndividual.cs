// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
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

namespace Paytools.Common.Model;

/// <summary>
/// Represents an individual either in employment, about to be employed, or previously but no longer employed.
/// </summary>
public interface IEmployableIndividual : INamedIndividual
{
    /// <summary>
    /// Gets the individual's National Insurance number.
    /// </summary>
    public NiNumber NiNumber { get; init; }

    /// <summary>
    /// Gets the individual's date of birth.
    /// </summary>
    public DateOnly DateOfBirth { get; init; }

    /// <summary>
    /// Gets the individual's "official" gender as recognised by HMRC for payroll purposes.
    /// </summary>
    Gender Gender { get; init; }
}
