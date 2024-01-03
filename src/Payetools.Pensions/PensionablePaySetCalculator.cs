// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.Pensions;

/// <summary>
/// Represents a pension contribution calculator for Pensionable Pay Set 1, 2 and 3.
/// </summary>
public class PensionablePaySetCalculator : PensionContributionCalculator
{
    /// <inheritdoc/>
    public override PensionsEarningsBasis EarningsBasis { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="PensionablePaySetCalculator"/> for the specified earnings basic and tax treatment.
    /// </summary>
    /// <param name="earningsBasis">Sepcifies the earnings basis for this calculator.  Must be one of Pensionable Pay Set 1,
    /// Pensionable Pay Set 2 or Pensionable Pay Set 3.</param>
    /// <param name="taxTreatment">Tax treatment for the target pension, i.e., net pay arrangement vs relief at source.</param>
    /// <param name="basicRateOfTax">Basic rate of tax to use for relief at source pensions.</param>
    /// <exception cref="ArgumentException">Thrown if an invalid earnings basis is supplied.</exception>
    public PensionablePaySetCalculator(PensionsEarningsBasis earningsBasis, PensionTaxTreatment taxTreatment, decimal? basicRateOfTax = null)
        : base(taxTreatment, basicRateOfTax)
    {
        if (earningsBasis != PensionsEarningsBasis.PensionablePaySet1 &&
            earningsBasis != PensionsEarningsBasis.PensionablePaySet2 &&
            earningsBasis != PensionsEarningsBasis.PensionablePaySet3)
            throw new ArgumentException("Earnings basis must be one of PensionablePaySet1, PensionablePaySet2 or PensionablePaySet3", nameof(earningsBasis));

        EarningsBasis = earningsBasis;
    }

    /// <inheritdoc/>
    protected override (decimal earningsForPensionCalculation, decimal employerContribution, decimal employeeContribution) CalculateContributions(decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false,
        decimal? salaryForMaternityPurposes = null)
    {
        return (pensionableSalary,
            decimal.Round(pensionableSalary * employerContributionPercentage / 100.0m, 2, MidpointRounding.AwayFromZero),
            employeeContributionIsFixedAmount ?
            employeeContribution :
            decimal.Round(pensionableSalary * employeeContribution / 100.0m, 2, MidpointRounding.AwayFromZero));
    }

    /// <inheritdoc/>
    protected override decimal GetEarningsForPensionCalculation(decimal pensionableSalary) => pensionableSalary;
}