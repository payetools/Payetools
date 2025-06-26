// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Shouldly;

namespace Payetools.Common.Tests;

public static class ShouldlyExtensions
{
    public static void ShouldHaveDefaultValue<T>(this T value, string? message = null)
    {
        if (!EqualityComparer<T>.Default.Equals(value, default(T)))
            throw new ShouldAssertException($"Must have default value. {message}");
    }

    public static void ShouldNotHaveDefaultValue<T>(this T value, string? message = null)
    {
        if (EqualityComparer<T>.Default.Equals(value, default(T)))
            throw new ShouldAssertException($"Must not have default value. {message}");
    }
}
