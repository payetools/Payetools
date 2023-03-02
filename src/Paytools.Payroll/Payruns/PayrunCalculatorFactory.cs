// Copyright (c) 2023 Paytools Foundation.
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

using Paytools.Common.Model;
using Paytools.IncomeTax;
using Paytools.NationalInsurance;
using Paytools.Pensions;
using Paytools.ReferenceData;
using Paytools.StudentLoans;

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Represents a factory object that creates payrun calculator instances that implement <see cref="IPayrunCalculator"/>.
/// </summary>
public class PayrunCalculatorFactory : IPayrunCalculatorFactory
{
    /// <summary>
    /// Gets a calculator for specified pay date.
    /// </summary>
    /// <param name="payDate">Applicable pay date for the required calculator.</param>
    /// <returns>An implementation of <see cref="IPayrunCalculator"/> for the specified pay date.</returns>
    public Task<IPayrunCalculator> GetCalculatorAsync(PayDate payDate)
    {
        throw new NotImplementedException();
    }

    internal class FactorySet
    {
        public IHmrcReferenceDataProvider HmrcReferenceDataProvider { get; init; } = default!;

        public ITaxCalculatorFactory TaxCalculatorFactory { get; init; } = default!;

        public INiCalculatorFactory NiCalculatorFactory { get; init; } = default!;

        public IStudentLoanCalculatorFactory StudentLoanCalculatorFactory { get; init; } = default!;

        public IPensionContributionCalculatorFactory PensionContributionCalculatorFactory { get; init; } = default!;
    }

    /*
    private readonly HmrcReferenceDataProviderFactory _hmrcReferenceDataProviderFactory;
    private readonly Uri _referenceDataEndpoint;
    private IHmrcReferenceDataProvider _hmrcReferenceDataProvider;

    /// <summary>
    /// Initialises a new instance of <see cref="PayrunCalculatorFactory"/>.
    /// </summary>
    /// <param name="hmrcReferenceDataProviderFactory">HMRC reference data provider factory.</param>
    /// <param name="referenceDataEndpoint">HTTP(S) endpoint to retrieve reference data from.</param>
    public PayrunCalculatorFactory(IHmrcReferenceDataProviderFactory hmrcReferenceDataProviderFactory,
        Uri referenceDataEndpoint,
        ITaxCalculatorFactory taxCalculatorFactory,
        INiCalculatorFactory niCalculatorFactory,
        IStudentLoanCalculatorFactory studentLoanCalculatorFactory,
        IPensionContributionCalculatorFactory pensionContributionCalculatorFactory)
    {
        _hmrcReferenceDataProviderFactory = hmrcReferenceDataProviderFactory;
        _referenceDataEndpoint = referenceDataEndpoint;
        _taxCalculatorFactory = taxCalculatorFactory;
        _niCalculatorFactory = niCalculatorFactory;
        _studentLoanCalculatorFactory = studentLoanCalculatorFactory;
        _pensionContributionCalculatorFactory = pensionContributionCalculatorFactory;
    }

    /// <summary>
    /// Gets a calculator for specified pay date.
    /// </summary>
    /// <param name="payDate">Applicable pay date for the required calculator.</param>
    /// <returns>An implementation of <see cref="IPayrunCalculator"/> for the specified pay date.</returns>
    public async Task<IPayrunCalculator> GetCalculatorAsync(PayDate payDate)
    {
        IHmrcReferenceDataProvider referenceDataProvider = await _hmrcReferenceDataProviderFactory.CreateProviderAsync(_referenceDataEndpoint);

        ITaxCalculatorFactory taxCalculatorFactory
        INiCalculatorFactory niCalculatorFactory,
        IStudentLoanCalculatorFactory studentLoanCalculatorFactory,
        IPensionContributionCalculatorFactory pensionContributionCalculatorFactory
    }

    private static async Task<FactorySet> GetFactories(IHmrcReferenceDataProviderFactory hmrcReferenceDataProviderFactory)
    {
        IHmrcReferenceDataProvider referenceDataProvider = await hmrcReferenceDataProviderFactory.CreateProviderAsync(_referenceDataEndpoint);

    }
    */
}
