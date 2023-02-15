// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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

namespace Paytools.Common.Extensions;

/// <summary>
/// Extension methods for instances of <see cref="IEnumerable{T}"/>.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Provides a new IEnumerable of tuples, where the first element of the tuple is the original item and the second
    /// element is the numeric index of the item, zero-based.
    /// </summary>
    /// <typeparam name="T">Type of item in original IEnumerable.</typeparam>
    /// <param name="source">Source IEnumerable.</param>
    /// <returns>New IEnumerable of tuples as described above.</returns>
    public static IEnumerable<(T Value, int Index)> WithIndex<T>(this IEnumerable<T> source)
        => source.Select((item, index) => (Item: item, Index: index));
}