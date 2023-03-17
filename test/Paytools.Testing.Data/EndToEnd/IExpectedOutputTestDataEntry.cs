// Copyright (c) 2023 Paytools Foundation.
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

using System;
using Paytools.Common.Model;

namespace Paytools.Testing.Data.EndToEnd;

public interface IExpectedOutputTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string TestReference { get; }
    decimal StatutoryMaternityPay { get; }
    decimal StatutoryPaternityPay { get; }
    decimal StatutoryAdoptionPay { get; }
    decimal SharedParentalPay { get; }
    decimal StatutoryParentalBereavementPay { get; }
    decimal GrossPay { get; }
    decimal TaxablePay { get; }
    decimal NicablePay { get; }
    decimal TaxPaid { get; }
    decimal EmployeeNiContribution { get; }
    decimal EmployerNiContribution { get; }
    decimal StudentLoanRepayments { get; }
    decimal GraduateLoanRepayments { get; }
    decimal PayrolledBenefits { get; }
    decimal TaxUnpaidDueToRegulatoryLimit { get; }
    decimal EmployerPensionContribution { get; }
    bool PensionUnderNpa { get; }
    decimal EmployeePensionContribution { get; }
    decimal OtherDeductions { get; }
    decimal NetPay { get; }
    decimal StatutoryMaternityPayYtd { get; }
    decimal StatutoryPaternityPayYtd { get; }
    decimal StatutoryAdoptionPayYtd { get; }
    decimal SharedParentalPayYtd { get; }
    decimal StatutoryParentalBereavementPayYtd { get; }
    decimal GrossPayYtd { get; }
    decimal TaxablePayYtd { get; }
    decimal NicablePayYtd { get; }
    decimal TaxPaidYtd { get; }
    decimal StudentLoanRepaymentsYtd { get; }
    decimal GraduateLoanRepaymentsYtd { get; }
    decimal PayrolledBenefitsYtd { get; }
    decimal TaxUnpaidDueToRegulatoryLimitYtd { get; }
    decimal EmployerPensionContributionYtd { get; }
    decimal EmployeePensionContributionsUnderNpaYtd { get; }
    decimal EmployeePensionContributionsUnderRasYtd { get; }
}
