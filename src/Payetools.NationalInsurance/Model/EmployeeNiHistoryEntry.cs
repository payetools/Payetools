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
    private readonly NiEarningsBreakdown _niEarningsBreakdown;

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
    public decimal EarningsUpToAndIncludingLEL => _niEarningsBreakdown.EarningsUpToAndIncludingLEL;

    /// <summary>
    /// Gets the earnings up above the Lower Earnings Limit and up to and including the Secondary Threshold
    /// for this record.
    /// </summary>
    public decimal EarningsAboveLELUpToAndIncludingST => _niEarningsBreakdown.EarningsAboveLELUpToAndIncludingST;

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Primary Threshold
    /// for this record.
    /// </summary>
    public decimal EarningsAboveSTUpToAndIncludingPT => _niEarningsBreakdown.EarningsAboveSTUpToAndIncludingPT;

    /// <summary>
    /// Gets the earnings up above the Primary Threshold and up to and including the Freeport Upper Secondary
    /// Threshold for this record.
    /// </summary>
    public decimal EarningsAbovePTUpToAndIncludingFUST => _niEarningsBreakdown.EarningsAbovePTUpToAndIncludingFUST;

    /// <summary>
    /// Gets the earnings up above the Freeport Upper Secondary Threshold and up to and including the Upper
    /// Earnings Limit for this record.
    /// </summary>
    public decimal EarningsAboveFUSTUpToAndIncludingUEL => _niEarningsBreakdown.EarningsAboveFUSTUpToAndIncludingUEL;

    /// <summary>
    /// Gets the earnings up above the Upper Earnings Limit for this record.
    /// </summary>
    public decimal EarningsAboveUEL => _niEarningsBreakdown.EarningsAboveUEL;

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Upper Earnings Limit
    /// for this record.
    /// </summary>
    public decimal EarningsAboveSTUpToAndIncludingUEL => _niEarningsBreakdown.EarningsAboveSTUpToAndIncludingUEL;

    /// <summary>
    /// Initialises a new instance of <see cref="EmployeeNiHistoryEntry"/>.
    /// </summary>
    /// <param name="niCategoryPertaining">NI category for this record.</param>
    /// <param name="niEarningsBreakdown">NI earnings breakdown for this record.</param>
    /// <param name="grossNicableEarnings">Gross Nicable earnings for this record.</param>
    /// <param name="employeeContribution">Employee contribution for this record.</param>
    /// <param name="employerContribution">Employer contribution for this record.</param>
    /// <param name="totalContribution">Total (i.e., employee + employer) contribution for this record.</param>
    public EmployeeNiHistoryEntry(
        NiCategory niCategoryPertaining,
        NiEarningsBreakdown niEarningsBreakdown,
        decimal grossNicableEarnings,
        decimal employeeContribution,
        decimal employerContribution,
        decimal totalContribution)
    {
        NiCategoryPertaining = niCategoryPertaining;
        _niEarningsBreakdown = niEarningsBreakdown;
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
        : this(result.NiCategory, result.EarningsBreakdown, result.NicablePay, result.EmployeeContribution,
              result.EmployerContribution, result.TotalContribution)
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

        return new EmployeeNiHistoryEntry(this.NiCategoryPertaining,
            this._niEarningsBreakdown.Add(result.EarningsBreakdown),
            GrossNicableEarnings + result.NicablePay,
            EmployeeContribution + result.EmployeeContribution,
            EmployerContribution + result.EmployerContribution,
            TotalContribution + result.TotalContribution);
    }
}