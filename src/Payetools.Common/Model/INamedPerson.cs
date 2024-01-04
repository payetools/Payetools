// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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
    Title? Title { get; }

    /// <summary>
    /// Gets the individual's first (also known as "given" or "Christian") name.
    /// </summary>
    string? FirstName { get; }

    /// <summary>
    /// Gets a list of the individual's initials as an array.  Note that this property is only used if the individual's
    /// first name is not known, and its use is mutually exclusive with <see cref="FirstName"/> and <see cref="MiddleNames"/>.
    /// </summary>
    char[]? Initials { get; }

    /// <summary>
    /// Gets the middle names of the individual, space separated.  Note that this property is optional, as some people do
    /// not have middle names, or they choose not to disclose them.
    /// </summary>
    string? MiddleNames { get; }

    /// <summary>
    /// Gets the individual's last (also known as "family") name.
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// Gets the name that an individual would like to be known as.  This might be an abbreviated form of their
    /// first name (e.g., Geoff rather than Geoffrey) or just a nickname that they are commonly known by.  Optional.
    /// </summary>
    string? KnownAsName { get; }

    /// <summary>
    /// Gets a value indicating whether the individual has supplied a middle name.
    /// </summary>
    bool HasMiddleName { get; }

    /// <summary>
    /// Gets any initials provided as a space separated string.  Will be null if a <see cref="FirstName"/>
    /// has been provided.
    /// </summary>
    string? InitialsAsString { get; }
}