// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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

using Paytools.Common.Model;
using Paytools.NationalInsurance.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paytools.NationalInsurance;

/// <summary>
/// Factory to generate <see cref="ITaxCalculator"/> implementations that are for a given pay date and specific tax regime.
/// </summary>
public class NiCalculatorFactory : INiCalculatorFactory
{
    private INiReferenceDataProvider _niReferenceDataProvider;

    public NiCalculatorFactory(INiReferenceDataProvider niReferenceDataProvider)
    {
        _niReferenceDataProvider = niReferenceDataProvider;
    }

    public INiCalculator GetCalculator(PayDate payDate, int numberOfTaxPeriods = 1) =>
        GetCalculator(payDate.TaxYear, payDate.TaxPeriod, payDate.PayFrequency, numberOfTaxPeriods);

    public INiCalculator GetCalculator(TaxYear taxYear, int taxPeriod, PayFrequency payFrequency, int numberOfTaxPeriods = 1) =>
        new NiCalculator(
            _niReferenceDataProvider.GetPeriodThresholdsForTaxYearAndPeriod(taxYear, taxPeriod, payFrequency, numberOfTaxPeriods),
            _niReferenceDataProvider.GetRatesForTaxYearAndPeriod(taxYear, taxPeriod));
}
