// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
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

namespace Paytools.Common.Model;

/// <summary>
/// Represents a named individual, i.e., a (usually living) person.  This interface is provided for all the situations
/// where a contact person is required, but is also the base entity for employees via <see cref="IEmployableIndividual"/>.
/// </summary>
public interface INamedIndividual
{
    /// <summary>
    /// Gets the individual's title, e.g., Mr, Mrs, Miss, Dr., etc.
    /// </summary>
    Title? Title { get; init; }

    /// <summary>
    /// Gets the individual's first (also known as "given" or "Christian") name.
    /// </summary>
    string? FirstName { get; init; }

    /// <summary>
    /// Gets a list of the individual's initials as an array.  Note that this property is only used if the individual's
    /// first name is not known, and its use is mutually exclusive with <see cref="FirstName"/> and <see cref="MiddleNames"/>.
    /// </summary>
    string[]? Initials { get; init; }

    /// <summary>
    /// Gets the middle names of the individual, space separated.  Note that this property is optional, as some people do
    /// not have middle names, or they choose not to disclose them.
    /// </summary>
    string? MiddleNames { get; init; }

    /// <summary>
    /// Gets the individual's last (also known as "family") name.
    /// </summary>
    string LastName { get; init; }

    /// <summary>
    /// Helper property to indicate whether the individual has supplied a middle name.
    /// </summary>
    bool HasMiddleName { get; init; }

    /// <summary>
    /// Helper property to provide any initials provided as a space separated string.  Will be null if a <see cref="FirstName"/>
    /// has been provided.
    /// </summary>
    string? InitialsAsString { get; init; }
}