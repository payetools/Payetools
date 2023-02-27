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

namespace Paytools.Employment.Model;

/// <summary>
/// Interface that represents an employee's pay structure.
/// </summary>
public interface IEmployeePayStructure
{
    /// <summary>
    /// Gets the unique ID for this pay structure.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the rate of pay.  The type of this rate of pay is given by <see cref="PayRateType"/>.
    /// </summary>
    decimal PayRate { get; }

    /// <summary>
    /// Gets the type of pay that <see cref="PayRate"/> represents.
    /// </summary>
    PayRateType PayRateType { get; }

    /// <summary>
    /// Gets the pay component that this pay structure is based on.
    /// </summary>
    IPayComponent PayComponent { get; }
}
