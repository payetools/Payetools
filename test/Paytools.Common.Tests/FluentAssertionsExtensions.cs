// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions.Execution;

namespace Paytools.Common.Tests;

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
