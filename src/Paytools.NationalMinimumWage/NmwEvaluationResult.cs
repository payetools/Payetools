// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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