// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Diagnostics;
using Xunit.Abstractions;

namespace Payetools.IncomeTax.Tests;

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
