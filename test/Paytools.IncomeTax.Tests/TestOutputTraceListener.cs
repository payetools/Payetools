using System.Diagnostics;
using Xunit.Abstractions;

namespace Paytools.IncomeTax.Tests;

internal class TestOutputTraceListener : TraceListener
{
    private readonly ITestOutputHelper Output;
    public static bool Enabled { get; set; }

    public TestOutputTraceListener(ITestOutputHelper output)
    {
        Output = output;
    }

    public override void Write(string? message)
    {
        throw new NotImplementedException();
    }

    public override void WriteLine(string? message)
    {
        if (Enabled)
            Output.WriteLine(message);
    }

    public override bool Equals(object? obj)
    {
        return obj is TestOutputTraceListener;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
