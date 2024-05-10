// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Payroll.Hmrc;
using Payetools.Payroll.Model;
using Payetools.ReferenceData.Employer;

namespace Payetools.Payroll.Tests;

public class EmployerReclaimCalculationTests
{
    [Fact]
    public void TestEmployerEligibleForSER()
    {
        var input = MakeHistoryEntry();
        var employer = MakeEmployer(true);

        var calculator = new StatutoryPaymentReclaimCalculator(MakeReclaimInfo());

        calculator.Calculate(employer, input, out var reclaim);

        reclaim.Should().NotBeNull();
        reclaim.ReclaimableStatutoryMaternityPay.Should().Be(input.TotalStatutoryMaternityPay);
        reclaim.ReclaimableStatutoryAdoptionPay.Should().Be(input.TotalStatutoryAdoptionPay);
        reclaim.ReclaimableStatutoryPaternityPay.Should().Be(input.TotalStatutoryPaternityPay);
        reclaim.ReclaimableStatutorySharedParentalPay.Should().Be(input.TotalStatutorySharedParentalPay);
        reclaim.ReclaimableStatutoryParentalBereavementPay.Should().Be(input.TotalStatutoryParentalBereavementPay);
        reclaim.AdditionalNiCompensationOnSMP.Should().Be(27.83m);
        reclaim.AdditionalNiCompensationOnSAP.Should().Be(18.39m);
        reclaim.AdditionalNiCompensationOnSPP.Should().Be(3.63m);
        reclaim.AdditionalNiCompensationOnSShPP.Should().Be(2.02m);
        reclaim.AdditionalNiCompensationOnSPBP.Should().Be(6.04m);
    }

    [Fact]
    public void TestEmployerIneligibleForSER()
    {
        var input = MakeHistoryEntry();
        var employer = MakeEmployer(false);

        var calculator = new StatutoryPaymentReclaimCalculator(MakeReclaimInfo());

        calculator.Calculate(employer, input, out var reclaim);

        reclaim.Should().NotBeNull();
        reclaim.ReclaimableStatutoryMaternityPay.Should().Be(853.36m);
        reclaim.ReclaimableStatutoryAdoptionPay.Should().Be(563.74m);
        reclaim.ReclaimableStatutoryPaternityPay.Should().Be(111.10m);
        reclaim.ReclaimableStatutorySharedParentalPay.Should().Be(61.84m);
        reclaim.ReclaimableStatutoryParentalBereavementPay.Should().Be(185.23m);
        reclaim.AdditionalNiCompensationOnSMP.Should().Be(0.0m);
        reclaim.AdditionalNiCompensationOnSAP.Should().Be(0.0m);
        reclaim.AdditionalNiCompensationOnSPP.Should().Be(0.0m);
        reclaim.AdditionalNiCompensationOnSShPP.Should().Be(0.0m);
        reclaim.AdditionalNiCompensationOnSPBP.Should().Be(0.0m);
    }

    private static IEmployerHistoryEntry MakeHistoryEntry() =>
        new EmployerHistoryEntry
        {
            TotalStatutoryMaternityPay = 927.56m,
            TotalStatutoryAdoptionPay = 612.76m,
            TotalStatutoryPaternityPay = 120.76m,
            TotalStatutorySharedParentalPay = 67.21m,
            TotalStatutoryParentalBereavementPay = 201.33m
        };

    private static StatutoryPaymentReclaimInfo MakeReclaimInfo() =>
        new StatutoryPaymentReclaimInfo 
        {
            DefaultReclaimRate = 0.92m,
            SmallEmployersReclaimRate = 1.03m
        };

    private IEmployer MakeEmployer(bool isEligibleForEmploymentAllowance) =>
        new Employer(null, string.Empty, null, null, null, false, null, isEligibleForEmploymentAllowance);
}
