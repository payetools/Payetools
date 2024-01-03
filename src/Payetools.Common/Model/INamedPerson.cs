// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Common.Model;

/// <summary>
/// Represents a named individual, i.e., a (usually living) person.  This interface is provided for all the situations
/// where a contact person is required, but is also the base entity for employees.
/// </summary>
public interface INamedPerson
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
    /// Gets a value indicating whether the individual has supplied a middle name.
    /// </summary>
    bool HasMiddleName { get; }

    /// <summary>
    /// Gets any initials provided as a space separated string.  Will be null if a <see cref="FirstName"/>
    /// has been provided.
    /// </summary>
    string? InitialsAsString { get; init; }
}