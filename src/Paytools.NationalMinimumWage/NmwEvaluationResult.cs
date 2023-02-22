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

namespace Paytools.NationalMinimumWage;

/// <summary>
/// Represents the result of an NMW/NLW evaluation for a particular individual for particular pay period.
/// </summary>
public readonly struct NmwEvaluationResult
{
    /// <summary>
    /// Initialises a new instance of <see cref="NmwEvaluationResult"/>.
    /// </summary>
    /// <param name="isCompliant">True if the pay is compliant with the regulations; false otherwise.</param>
    /// <param name="nmwLevelApplied">NMW/NLW level used for compliance checking.</param>
    /// <param name="ageAtStartOfPayPeriod">Age at the start of the pay period (in whole years).</param>
    /// <param name="commentary">Human-readable commentary on the evaluation.</param>
    public NmwEvaluationResult(bool isCompliant, decimal? nmwLevelApplied, int ageAtStartOfPayPeriod, string commentary)
    {
        IsCompliant = isCompliant;
        NmwLevelApplied = nmwLevelApplied;
        AgeAtStartOfPayPeriod = ageAtStartOfPayPeriod;
        Commentary = commentary;
    }

    /// <summary>
    /// Gets a value indicating whether an individual's pay is compliant with the NMW/NLW regulations.
    /// </summary>
    public bool IsCompliant { get; }

    /// <summary>
    /// Gets the level (hourly rate) used to verify the employee's compliance with the regulations.  Null
    /// if no level applies.
    /// </summary>
    public decimal? NmwLevelApplied { get; }

    /// <summary>
    /// Gets the age of the individual at the start of the pay period.
    /// </summary>
    public int AgeAtStartOfPayPeriod { get; }

    /// <summary>
    /// Gets the human-readable commentary relating to this evaluation.  May be empty.
    /// </summary>
    public string Commentary { get; }
}