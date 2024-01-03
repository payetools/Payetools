// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Common.Extensions;

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