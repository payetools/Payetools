// Copyright (c) 2023 Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
