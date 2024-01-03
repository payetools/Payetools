// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions.Execution;

namespace Payetools.Common.Tests;

public static class FluentAssertionsExtensions
{
    public static void ShouldHaveDefaultValue<T>(this T value)
    {
        if (!EqualityComparer<T>.Default.Equals(value, default(T)))
            throw new AssertionFailedException("Must have default value.");
    }

    public static void ShouldNotHaveDefaultValue<T>(this T value)
    {
        if (EqualityComparer<T>.Default.Equals(value, default(T)))
            throw new AssertionFailedException("Must not have default value.");
    }
}
