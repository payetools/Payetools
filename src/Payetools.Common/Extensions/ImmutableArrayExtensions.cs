// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Collections.Immutable;

namespace Payetools.Common.Extensions;

/// <summary>
/// Extension methods for <see cref="ImmutableArray{T}"/>.
/// </summary>
public static class ImmutableArrayExtensions
{
    /// <summary>
    /// Replaces the last item in the array returning a new immutable array.
    /// </summary>
    /// <param name="array">List to be updated.</param>
    /// <param name="newValue">New value to inserted in place of the current last value.</param>
    /// <typeparam name="T">Type of object that the list contains.</typeparam>
    /// <returns>A new <see cref="ImmutableArray{T}"/> with the last item in the array updated.</returns>
    public static ImmutableArray<T> ReplaceLast<T>(this ImmutableArray<T> array, T newValue) =>
        array
        .RemoveAt(array.Length - 1)
        .Add(newValue);
}