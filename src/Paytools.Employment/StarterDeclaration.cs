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

namespace Paytools.Employment;

/// <summary>
/// Represents a new employee's starter declaration, if provided.  In the case of high earners, the
/// starter declaration may be omitted and the employee put on a 0T tax code rather than a BR or
/// "emergency tax code", in order to avoid paying insufficient tax.
/// </summary>
public enum StarterDeclaration
{
    /// <summary>
    /// HMRC: "This is my first job since 6 April and since the 6 April I have not received
    /// payments from any of the following:
    /// <list type="bullet">
    /// <item>Jobseeker's Allowance</item>
    /// <item>Employment and Support Allowance</item>
    /// <item>Incapacity Benefit</item>
    /// </list>".
    /// </summary>
    A,

    /// <summary>
    /// HMRC: "Since 6 April I have had another job but I do not have a P45. And/or since
    /// the 6 April I have received payments from any of the following:
    /// <list type="bullet">
    /// <item>Jobseeker's Allowance</item>
    /// <item>Employment and Support Allowance</item>
    /// <item>Incapacity Benefit</item>
    /// </list>".
    /// </summary>
    B,

    /// <summary>
    ///  have another job and/or I am in receipt of a State, workplace or private pension.
    /// </summary>
    C,

    /// <summary>
    /// No Starter Declaration captured as the employee is a high earner.
    /// </summary>
    HighEarnerNotRecorded
}
