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

using System.Collections.Immutable;

namespace Paytools.Common.Extensions;

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
