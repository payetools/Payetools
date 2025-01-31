// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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
    public static IEnumerable<(T Value, int Index)> WithIndex<T>(/* in */ this IEnumerable<T> source)
        => source.Select((item, index) => (Item: item, Index: index));
}