// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.NationalInsurance.Model;

/// <summary>
/// Represents once element of the employee's NI history during the current tax year.  For employees that have only
/// have one NI category throughout the tax year, they will have just one instance of <see cref="EmployeeNiHistoryEntry"/>
/// applicable.  But it is of course possible for an employee's NI category to change throughout the tax year (for example
/// because they turned 21 years of age), and in this case, multiple records must be held.
/// </summary>
public readonly struct EmployeeNiHistoryEntry : IEmployeeNiHistoryEntry
{
    /// <summary>
    /// Gets the National Insurance category letter pertaining to this record.
    /// </summary>
    public NiCategory NiCategoryPertaining { get; }

    /// <summary>
    /// Gets the gross NI-able earnings applicable for the duration of this record.
    /// </summary>
    public decimal GrossNicableEarnings { get; }

    /// <summary>
    /// Gets the total employee contribution made during the duration of this record.
    /// </summary>
    public decimal EmployeeContribution { get; }

    /// <summary>
    /// Gets the total employer contribution made during the duration of this record.
    /// </summary>
    public decimal EmployerContribution { get; }

    /// <summary>
    /// Gets the total contribution (employee + employer) made during the duration of this record.
    /// </summary>
    public decimal TotalContribution { get; }

    /// <summary>
    /// Gets the earnings up to and including the Lower Earnings Limit for this record.
    /// </summary>
    public decimal EarningsAtLEL { get; init; } = default;

    /// <summary>
    /// Gets the earnings up above the Lower Earnings Limit and up to and including the Secondary Threshold
    /// for this record.
    /// </summary>
    public decimal EarningsAboveLELUpToAndIncludingST { get; init; } = default;

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Primary Threshold
    /// for this record.
    /// </summary>
    public decimal EarningsAboveSTUpToAndIncludingPT { get; init; } = default;

    /// <summary>
    /// Gets the earnings up above the Primary Threshold and up to and including the Freeport Upper Secondary
    /// Threshold for this record.
    /// </summary>
    public decimal EarningsAbovePTUpToAndIncludingFUST { get; init; } = default;

    /// <summary>
    /// Gets the earnings up above the Freeport Upper Secondary Threshold and up to and including the Upper
    /// Earnings Limit for this record.
    /// </summary>
    public decimal EarningsAboveFUSTUpToAndIncludingUEL { get; init; } = default;

    /// <summary>
    /// Gets the earnings up above the Upper Earnings Limit for this record.
    /// </summary>
    public decimal EarningsAboveUEL { get; init; } = default;

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Upper Earnings Limit
    /// for this record.
    /// </summary>
    public decimal EarningsAboveSTUpToAndIncludingUEL { get; init; } = default;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeNiHistoryEntry"/> struct with the supplied
    /// data.
    /// </summary>
    /// <param name="niCategoryPertaining">National Insurance category that this history entry relates to.</param>
    /// <param name="grossNicableEarnings">Gross NICable earnings year-to-date for this NI category.</param>
    /// <param name="employeeContribution">Total employee NI contribution year-to-date for this NI category.</param>
    /// <param name="employerContribution">Total employer NI contribution year-to-date for this NI category.</param>
    /// <param name="totalContribution">Total NI contributions (employee+employer) year-to-date for this NI category.</param>
    /// <param name="niEarningsBreakdown">National Insurance earnings breakdown by threshold.</param>
    /// <remarks>When working with the output of a pay run, use the single-parameter constructor that
    /// takes a <see cref="INiCalculationResult"/> instead of this constructor.  This constructor is
    /// intended to allow external code to generate an <see cref="EmployeeNiHistoryEntry"/> from
    /// scratch outside of a payroll pay run, for example, when hydrating an instance of this type
    /// from a database.</remarks>
    public EmployeeNiHistoryEntry(
        NiCategory niCategoryPertaining,
        decimal grossNicableEarnings,
        decimal employeeContribution,
        decimal employerContribution,
        decimal totalContribution,
        NiEarningsBreakdown niEarningsBreakdown)
        : this(niCategoryPertaining, grossNicableEarnings, employeeContribution, employerContribution, totalContribution)
    {
        EarningsAtLEL = niEarningsBreakdown.EarningsAtLEL;
        EarningsAboveLELUpToAndIncludingST = niEarningsBreakdown.EarningsAboveLELUpToAndIncludingST;
        EarningsAboveSTUpToAndIncludingPT = niEarningsBreakdown.EarningsAboveSTUpToAndIncludingPT;
        EarningsAbovePTUpToAndIncludingFUST = niEarningsBreakdown.EarningsAbovePTUpToAndIncludingFUST;
        EarningsAboveFUSTUpToAndIncludingUEL = niEarningsBreakdown.EarningsAboveFUSTUpToAndIncludingUEL;
        EarningsAboveUEL = niEarningsBreakdown.EarningsAboveUEL;
        EarningsAboveSTUpToAndIncludingUEL = niEarningsBreakdown.EarningsAboveSTUpToAndIncludingUEL;
    }

    private EmployeeNiHistoryEntry(
        NiCategory niCategoryPertaining,
        decimal grossNicableEarnings,
        decimal employeeContribution,
        decimal employerContribution,
        decimal totalContribution)
    {
        NiCategoryPertaining = niCategoryPertaining;
        GrossNicableEarnings = grossNicableEarnings;
        EmployeeContribution = employeeContribution;
        EmployerContribution = employerContribution;
        TotalContribution = totalContribution;
    }

    /// <summary>
    /// Initialises a new <see cref="EmployeeNiHistoryEntry"/> from the supplied calculation result.
    /// </summary>
    /// <param name="result">NI calculation result.</param>
    public EmployeeNiHistoryEntry(in INiCalculationResult result)
        : this(result.NiCategory, result.NicablePay, result.EmployeeContribution,
              result.EmployerContribution, result.TotalContribution, result.EarningsBreakdown)
    {
    }

    /// <summary>
    /// Adds the results of an NI calculation to the current history and returns a new instance of <see cref="IEmployeeNiHistoryEntry"/>
    /// with the results applied.
    /// </summary>
    /// <param name="result">NI calculation results to add to this history entry.</param>
    /// <returns>New instance of <see cref="IEmployeeNiHistoryEntry"/> with the NI calculation result applied.</returns>
    public IEmployeeNiHistoryEntry Add(in INiCalculationResult result)
    {
        if (NiCategoryPertaining != result.NiCategory)
            throw new ArgumentException($"NI calculation result applies to category {result.NiCategory} which is different to previous entry {NiCategoryPertaining}", nameof(result));

        var breakdown = result.EarningsBreakdown;

        var entry = new EmployeeNiHistoryEntry(this.NiCategoryPertaining,
            GrossNicableEarnings + result.NicablePay,
            EmployeeContribution + result.EmployeeContribution,
            EmployerContribution + result.EmployerContribution,
            TotalContribution + result.TotalContribution)
        {
            EarningsAtLEL = EarningsAtLEL + breakdown.EarningsAtLEL,
            EarningsAboveLELUpToAndIncludingST = EarningsAboveLELUpToAndIncludingST + breakdown.EarningsAboveLELUpToAndIncludingST,
            EarningsAboveSTUpToAndIncludingPT = EarningsAboveSTUpToAndIncludingPT + breakdown.EarningsAboveSTUpToAndIncludingPT,
            EarningsAbovePTUpToAndIncludingFUST = EarningsAbovePTUpToAndIncludingFUST + breakdown.EarningsAbovePTUpToAndIncludingFUST,
            EarningsAboveFUSTUpToAndIncludingUEL = EarningsAboveFUSTUpToAndIncludingUEL + breakdown.EarningsAboveFUSTUpToAndIncludingUEL,
            EarningsAboveUEL = EarningsAboveUEL + breakdown.EarningsAboveUEL,
            EarningsAboveSTUpToAndIncludingUEL = EarningsAboveSTUpToAndIncludingUEL + breakdown.EarningsAboveSTUpToAndIncludingUEL
        };

        return entry;
    }
}