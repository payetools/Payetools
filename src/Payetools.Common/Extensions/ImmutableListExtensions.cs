﻿// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Collections.Immutable;

namespace Payetools.Common.Extensions;

/// <summary>
/// Extension methods for <see cref="ImmutableList{T}"/>.
/// </summary>
public static class ImmutableListExtensions
{
    /// <summary>
    /// Replaces the last item in the list returning a new immutable list.
    /// </summary>
    /// <param name="list">List to be updated.</param>
    /// <param name="newValue">New value to inserted in place of the current last value.</param>
    /// <typeparam name="T">Type of object that the list contains.</typeparam>
    /// <returns>A new <see cref="ImmutableList{T}"/> with the last item in the list updated.</returns>
    public static ImmutableList<T> ReplaceLast<T>(this ImmutableList<T> list, T newValue) =>
        list
        .RemoveAt(list.Count - 1)
        .Add(newValue);
}
