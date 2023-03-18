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

public class PreviousYtdTestDataEntry : IPreviousYtdTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string TestReference { get; set; } = string.Empty;
    public decimal StatutoryMaternityPayYtd { get; set; }
    public decimal StatutoryPaternityPayYtd { get; set; }
    public decimal StatutoryAdoptionPayYtd { get; set; }
    public decimal SharedParentalPayYtd { get; set; }
    public decimal StatutoryParentalBereavementPayYtd { get; set; }
    public decimal GrossPayYtd { get; set; }
    public decimal TaxablePayYtd { get; set; }
    public decimal NicablePayYtd { get; set; }
    public decimal TaxPaidYtd { get; set; }
    public decimal StudentLoanRepaymentsYtd { get; set; }
    public decimal GraduateLoanRepaymentsYtd { get; set; }
    public decimal PayrolledBenefitsYtd { get; set; }
    public decimal TaxUnpaidDueToRegulatoryLimit { get; set; }
    public decimal EmployerPensionContributionsYtd { get; set; }
    public decimal EmployeePensionContributionsUnderNpaYtd { get; set; }
    public decimal EmployeePensionContributionsUnderRasYtd { get; set; }
}
