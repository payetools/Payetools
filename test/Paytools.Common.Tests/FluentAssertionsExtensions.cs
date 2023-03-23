// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
