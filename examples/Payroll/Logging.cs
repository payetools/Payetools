// This example code may be freely used without restriction; it may be freely copied, adapted and
// used without attribution.

using Microsoft.Extensions.Logging;

namespace Payetools.Example;

public static class Logging
{
    public static ILogger<T> MakeLogger<T>() where T : class
    {
        var factory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        return factory.CreateLogger<T>();
    }
}