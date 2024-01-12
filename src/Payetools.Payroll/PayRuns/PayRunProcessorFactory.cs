// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.IncomeTax;
using Payetools.NationalInsurance;
using Payetools.Payroll.Model;
using Payetools.Pensions;
using Payetools.ReferenceData;
using Payetools.StudentLoans;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Represents a factory object that creates payrun calculator instances that implement <see cref="IPayRunEntryProcessor"/>.
/// </summary>
public class PayRunProcessorFactory : IPayRunProcessorFactory
{
    internal class FactorySet
    {
        public IHmrcReferenceDataProvider HmrcReferenceDataProvider { get; init; } = default!;

        public ITaxCalculatorFactory TaxCalculatorFactory { get; init; } = default!;

        public INiCalculatorFactory NiCalculatorFactory { get; init; } = default!;

        public IStudentLoanCalculatorFactory StudentLoanCalculatorFactory { get; init; } = default!;

        public IPensionContributionCalculatorFactory PensionContributionCalculatorFactory { get; init; } = default!;
    }

    private readonly IHmrcReferenceDataProviderFactory? _hmrcReferenceDataProviderFactory;
    private readonly IHmrcReferenceDataProvider? _hmrcReferenceDataProvider;
    private readonly Uri? _referenceDataEndpoint;

    /// <summary>
    /// Initialises a new instance of <see cref="PayRunProcessorFactory"/>.
    /// </summary>
    /// <param name="hmrcReferenceDataProvider">HMRC reference data provider.</param>
    public PayRunProcessorFactory(in IHmrcReferenceDataProvider hmrcReferenceDataProvider)
    {
        _hmrcReferenceDataProvider = hmrcReferenceDataProvider;
    }

    /// <summary>
    /// Initialises a new instance of <see cref="PayRunProcessorFactory"/>.
    /// </summary>
    /// <param name="hmrcReferenceDataProviderFactory">HMRC reference data provider factory.</param>
    /// <param name="referenceDataEndpoint">HTTP(S) endpoint to retrieve reference data from.</param>
    public PayRunProcessorFactory(
        in IHmrcReferenceDataProviderFactory hmrcReferenceDataProviderFactory,
        in Uri referenceDataEndpoint)
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
    /// <returns>An implementation of <see cref="IPayRunProcessor"/> for the specified pay date
    /// and pay period.</returns>
    public IPayRunProcessor GetProcessor(IEmployer employer, PayDate payDate, DateRange payPeriod)
    {
        var factories = GetFactories(_hmrcReferenceDataProvider ??
                throw new InvalidOperationException("An valid HMRC reference data provider must be provided"));

        var calculator = new PayRunEntryProcessor(factories.TaxCalculatorFactory, factories.NiCalculatorFactory,
            factories.PensionContributionCalculatorFactory, factories.StudentLoanCalculatorFactory,
            payDate, payPeriod);

        return new PayRunProcessor(calculator, employer);
    }

    // Implementation note: Currently no effort is made to cache any of the factory types or the reference data
    // provider, on the basis that payruns are not created frequently.  However, in a large scale SaaS implementation,
    // we probably need to do something more sophisticated.  One advantage of the current approach is that reference
    // data is refreshed every time a payrun calculator is created; a mechanism to declare the data stale and
    // refresh it is probably needed in the long run.
    private static FactorySet GetFactories(in IHmrcReferenceDataProvider referenceDataProvider) =>
        new FactorySet()
        {
            HmrcReferenceDataProvider = referenceDataProvider,
            TaxCalculatorFactory = new TaxCalculatorFactory(referenceDataProvider),
            NiCalculatorFactory = new NiCalculatorFactory(referenceDataProvider),
            PensionContributionCalculatorFactory = new PensionContributionCalculatorFactory(referenceDataProvider),
            StudentLoanCalculatorFactory = new StudentLoanCalculatorFactory(referenceDataProvider)
        };
}