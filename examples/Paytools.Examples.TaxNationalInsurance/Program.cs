using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Paytools.ReferenceData;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

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

var factory = new HmrcReferenceDataProviderFactory(httpClientFactory, loggerFactory.CreateLogger<HmrcReferenceDataProviderFactory>());

var provider = await factory.CreateProviderAsync(new Uri("https://stellular-bombolone-34e67e.netlify.app/index.json"));

Console.WriteLine();



static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
{
    // Log the exception, display it, etc
    Console.WriteLine((e.ExceptionObject as Exception).Message);
}