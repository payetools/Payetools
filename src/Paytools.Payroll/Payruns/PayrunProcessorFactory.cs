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
using Paytools.Employment.Model;
using Paytools.IncomeTax;
using Paytools.NationalInsurance;
using Paytools.Pensions;
using Paytools.ReferenceData;
using Paytools.StudentLoans;

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Represents a factory object that creates payrun calculator instances that implement <see cref="IPayrunCalculator"/>.
/// </summary>
public class PayrunProcessorFactory : IPayrunProcessorFactory
{
    internal class FactorySet
    {
        public IHmrcReferenceDataProvider HmrcReferenceDataProvider { get; init; } = default!;

        public ITaxCalculatorFactory TaxCalculatorFactory { get; init; } = default!;

        public INiCalculatorFactory NiCalculatorFactory { get; init; } = default!;

        public IStudentLoanCalculatorFactory StudentLoanCalculatorFactory { get; init; } = default!;

        public IPensionContributionCalculatorFactory PensionContributionCalculatorFactory { get; init; } = default!;
    }

    private readonly IHmrcReferenceDataProviderFactory _hmrcReferenceDataProviderFactory;
    private readonly Uri _referenceDataEndpoint;

    /// <summary>
    /// Initialises a new instance of <see cref="PayrunProcessorFactory"/>.
    /// </summary>
    /// <param name="hmrcReferenceDataProviderFactory">HMRC reference data provider factory.</param>
    /// <param name="referenceDataEndpoint">HTTP(S) endpoint to retrieve reference data from.</param>
    public PayrunProcessorFactory(
        IHmrcReferenceDataProviderFactory hmrcReferenceDataProviderFactory,
        Uri referenceDataEndpoint)
    {
        _hmrcReferenceDataProviderFactory = hmrcReferenceDataProviderFactory;
        _referenceDataEndpoint = referenceDataEndpoint;
    }

    /// <summary>
    /// Gets a payrun processor for specified pay date and pay period.
    /// </summary>
    /// <param name="employer">Employer for this payrun processor.</param>
    /// <param name="payDate">Applicable pay date for the required payrun processor.</param>
    /// <param name="payPeriod">Applicable pay period for required payrun processor.</param>
    /// <returns>An implementation of <see cref="IPayrunProcessor"/> for the specified pay date
    /// and pay period.</returns>
    public async Task<IPayrunProcessor> GetProcessorAsync(IEmployer employer, PayDate payDate, PayReferencePeriod payPeriod)
    {
        var factories = await GetFactories(_hmrcReferenceDataProviderFactory, _referenceDataEndpoint);

        var calculator = new PayrunCalculator(factories.TaxCalculatorFactory, factories.NiCalculatorFactory,
            factories.PensionContributionCalculatorFactory, factories.StudentLoanCalculatorFactory,
            payDate, payPeriod);

        return new PayrunProcessor(calculator, employer);
    }

    // Implementation note: Currently no effort is made to cache any of the factory types or the reference data
    // provider, on the basis that payruns are not created frequently.  However, in a large scale SaaS implementation,
    // we probably need to do something more sophisticated.  One advantage of the current approach is that reference
    // data is refreshed every time a payrun calculator is created; a mechanism to declare the data stale and
    // refresh it is probably needed in the long run.
    private static async Task<FactorySet> GetFactories(IHmrcReferenceDataProviderFactory hmrcReferenceDataProviderFactory, Uri referenceDataEndpoint)
    {
        IHmrcReferenceDataProvider referenceDataProvider = await hmrcReferenceDataProviderFactory.CreateProviderAsync(referenceDataEndpoint);

        return new FactorySet()
        {
            HmrcReferenceDataProvider = referenceDataProvider,
            TaxCalculatorFactory = new TaxCalculatorFactory(referenceDataProvider),
            NiCalculatorFactory = new NiCalculatorFactory(referenceDataProvider),
            PensionContributionCalculatorFactory = new PensionContributionCalculatorFactory(referenceDataProvider),
            StudentLoanCalculatorFactory = new StudentLoanCalculatorFactory(referenceDataProvider)
        };
    }
}