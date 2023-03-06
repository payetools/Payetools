//
// ##################### PAYTOOLS EXAMPLE SOURCE CODE ##########################
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this example code (the "Example"), to deal in the Example without restriction,
// including without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Example, and to permit persons
// to whom the Example is furnished to do so without constraint.

// THE EXAMPLE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
// ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE EXAMPLE OR THE USE OR OTHER DEALINGS IN THE EXAMPLE.

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.PlatformAbstractions;
using Paytools.Common.Model;
using Paytools.Documents.Rendering;
using Paytools.Employment.Model;
using Paytools.IncomeTax;
using Paytools.NationalInsurance;
using Paytools.Payroll.Model;
using Paytools.Payroll.Payruns;
using Paytools.Pensions.Model;
using Paytools.ReferenceData;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;

Console.WriteLine("Hello, World!");

AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

var serviceProvider = new ServiceCollection()
    .AddHttpClient()
    .AddLogging(builder => builder.AddConsole())
    .BuildServiceProvider();

var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>() ??
    throw new InvalidOperationException("Unable to create HttpClientfactory");

var loggerFactory = serviceProvider.GetService<ILoggerFactory>() ??
    throw new InvalidOperationException("Unable to create ILoggerFactory");

var factory = new HmrcReferenceDataProviderFactory(httpClientFactory,
    loggerFactory.CreateLogger<HmrcReferenceDataProviderFactory>());

IPayrunProcessorFactory payrunProcessorFactory = new PayrunProcessorFactory(factory, new Uri("https://stellular-bombolone-34e67e.netlify.app/index.json"));

PayDate payDate = new PayDate(2022, 8, 20, PayFrequency.Monthly);
PayReferencePeriod payPeriod = new PayReferencePeriod(new DateOnly(), new DateOnly());

IEmployer employer = new Employer();

var processor = await payrunProcessorFactory.GetProcessorAsync(employer, payDate, payPeriod);

var entry = GetAugustEntry(employer);

List<IEmployeePayrunInputEntry> entries = new List<IEmployeePayrunInputEntry>();


entries.Add(entry);
processor.Process(entries, out var result);

Console.WriteLine(result.EmployeePayrunEntries[0].NiCalculationResult.ToString());
Console.WriteLine();



static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
{
    // Log the exception, display it, etc
    Console.WriteLine((e.ExceptionObject as Exception).Message);
}

static EmployeePayrunInputEntry GetAugustEntry(IEmployer employer)
{
    var employee = new Employee()
    { };

    var niEntries = ImmutableList<EmployeeNiHistoryEntry>.Empty;

    niEntries = niEntries.Add(new EmployeeNiHistoryEntry(NiCategory.A, new NiEarningsBreakdown(), 28333.32m - 1841.69m, 2070.55m, 3530.64m, 2070.55m + 3530.64m));

    IEmployeePayrollHistoryYtd history = new EmployeePayrollHistoryYtd(niEntries)
    {
        TaxablePayYtd = 28333.32m - 1841.69m + 450.12m,
        NicablePayYtd = 28333.32m - 1841.69m,
        TaxPaidYtd = 6533.86m
    };

    TaxCode.TryParse("1296L", out var taxCode);

    var employment = new Employment(ref history)
    {
        TaxCode = taxCode,
        NiCategory = NiCategory.A,
        PensionScheme = new PensionScheme()
        {
            EarningsBasis = EarningsBasis.PensionablePaySet1,
            TaxTreatment = PensionTaxTreatment.ReliefAtSource
        },
        IsDirector = true,
        DirectorsNiCalculationMethod = DirectorsNiCalculationMethod.AlternativeMethod
    };

    var earnings = ImmutableList<EarningsEntry>.Empty;
    var deductions = ImmutableList<DeductionEntry>.Empty;
    var payrolledBenefits = ImmutableList<IPayrolledBenefitForPeriod>.Empty;

    earnings = earnings.Add(new EarningsEntry()
    {
        EarningsType = new GenericPayComponent()
        {
            IsSubjectToTax = true,
            IsSubjectToNi = true,
            IsPensionable = true,
            IsNetToGross = false
        },
        FixedAmount = 6083.33m
    });

    earnings = earnings.Add(new EarningsEntry()
    {
        EarningsType = new GenericPayComponent()
        {
            IsSubjectToTax = true,
            IsSubjectToNi = true,
            IsPensionable = true,
            IsNetToGross = false
        },
        FixedAmount = 1000.0m
    });

    payrolledBenefits = payrolledBenefits.Add(new PayrolledBenefitForPeriod(150.05m));

    var pensionContributionLevels = new PensionContributionLevels()
    {
        EmployeeContribution = 495.84m,
        EmployeeContributionIsFixedAmount = true,
        EmployerContributionPercentage = 3,
        EmployersNiReinvestmentPercentage = 100,
        SalaryExchangeApplied = true
    };

    return new EmployeePayrunInputEntry(employee,
        employment,
        earnings,
        deductions,
        payrolledBenefits,
        pensionContributionLevels);
}

static EmployeePayrunInputEntry GetNovemberEntry(IEmployer employer)
{
    var employee = new Employee()
    { };

    var niEntries = ImmutableList<EmployeeNiHistoryEntry>.Empty;

    niEntries = niEntries.Add(new EmployeeNiHistoryEntry(NiCategory.A, new NiEarningsBreakdown(), 0.0m, 2070.55m, 3530.64m, 0.0m));

    IEmployeePayrollHistoryYtd history = new EmployeePayrollHistoryYtd(niEntries)
    {
        TaxablePayYtd = 28333.32m - 1841.69m + 450.12m,
        NicablePayYtd = 28333.32m - 1841.69m,
        TaxPaidYtd = 6533.86m
    };

    TaxCode.TryParse("1296L", out var taxCode);

    var employment = new Employment(ref history)
    {
        TaxCode = taxCode,
        NiCategory = NiCategory.A,
        PensionScheme = new PensionScheme()
        {
            EarningsBasis = EarningsBasis.PensionablePaySet1,
            TaxTreatment = PensionTaxTreatment.ReliefAtSource
        },
        IsDirector = true,
        DirectorsNiCalculationMethod = DirectorsNiCalculationMethod.StandardAnnualisedEarningsMethod
    };

    var earnings = ImmutableList<EarningsEntry>.Empty;
    var deductions = ImmutableList<DeductionEntry>.Empty;
    var payrolledBenefits = ImmutableList<IPayrolledBenefitForPeriod>.Empty;

    earnings = earnings.Add(new EarningsEntry()
    {
        EarningsType = new GenericPayComponent()
        {
            IsSubjectToTax = true,
            IsSubjectToNi = true,
            IsPensionable = true,
            IsNetToGross = false
        },
        FixedAmount = 6083.33m
    });

    earnings = earnings.Add(new EarningsEntry()
    {
        EarningsType = new GenericPayComponent()
        {
            IsSubjectToTax = true,
            IsSubjectToNi = true,
            IsPensionable = true,
            IsNetToGross = false
        },
        FixedAmount = 1000.0m
    });

    payrolledBenefits = payrolledBenefits.Add(new PayrolledBenefitForPeriod(150.05m));

    var pensionContributionLevels = new PensionContributionLevels()
    {
        EmployeeContribution = 495.64m,
        EmployeeContributionIsFixedAmount = true,
        EmployerContributionPercentage = 3,
        EmployersNiReinvestmentPercentage = 100,
        SalaryExchangeApplied = true
    };

    return new EmployeePayrunInputEntry(employee,
        employment,
        earnings,
        deductions,
        payrolledBenefits,
        pensionContributionLevels);
}

static RazorViewToStringRenderer BuildServiceProvider()
{
    var services = new ServiceCollection();
    var applicationEnvironment = PlatformServices.Default.Application;
    services.AddSingleton(applicationEnvironment);

    var appDirectory = Directory.GetCurrentDirectory();

    var environment = new HostingEnvironment
    {
        ApplicationName = Assembly.GetEntryAssembly().GetName().Name
    };
    services.AddSingleton<IHostingEnvironment>(environment);

    //services.Configure<RazorViewEngineOptions>(options =>
    //{
    //    options.FileProviders.Clear();
    //    options.FileProviders.Add(new PhysicalFileProvider(appDirectory));
    //});

    services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

    var diagnosticSource = new DiagnosticListener("Microsoft.AspNetCore");
    services.AddSingleton<DiagnosticSource>(diagnosticSource);

    services.AddLogging();
    services.AddMvc();
    services.AddSingleton<RazorViewToStringRenderer>();
    var provider = services.BuildServiceProvider();
    return provider.GetRequiredService<RazorViewToStringRenderer>();
}