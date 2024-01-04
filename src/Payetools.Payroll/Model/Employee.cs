// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Net.Mail;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an employee for payroll purposes.
/// </summary>
public record Employee : IEmployee
{
    /// <summary>
    /// Gets the individual's title, e.g., Mr, Mrs, Miss, Dr., etc.
    /// </summary>
    public Title? Title { get; init; }

    /// <summary>
    /// Gets the individual's first (also known as "given" or "Christian") name.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Gets a list of the individual's initials as an array.  Note that this property is only used if the individual's
    /// first name is not known, and its use is mutually exclusive with <see cref="FirstName"/> and <see cref="MiddleNames"/>.
    /// </summary>
    public char[]? Initials { get; init; }

    /// <summary>
    /// Gets the middle names of the individual, space separated.  Note that this property is optional, as some people do
    /// not have middle names, or they choose not to disclose them.
    /// </summary>
    public string? MiddleNames { get; init; }

    /// <summary>
    /// Gets the individual's last (also known as "family") name.
    /// </summary>
    public string LastName { get; init; } = default!;

    /// <summary>
    /// Gets the name that an individual would like to be known as.  This might be an abbreviated form of their
    /// first name (e.g., Geoff rather than Geoffrey) or just a nickname that they are commonly known by.  Optional.
    /// </summary>
    public string? KnownAsName { get; init; } = default!;

    /// <summary>
    /// Gets a value indicating whether the individual has supplied a middle name.
    /// </summary>
    public bool HasMiddleName => MiddleNames != null;

    /// <summary>
    /// Gets any initials provided as a space separated string.  Will be null if a <see cref="FirstName"/>
    /// has been provided.
    /// </summary>
    public string? InitialsAsString { get; init; }

    /// <summary>
    /// Gets the individual's National Insurance number.
    /// </summary>
    public NiNumber NiNumber { get; init; }

    /// <summary>
    /// Gets the individual's date of birth.
    /// </summary>
    public DateOnly DateOfBirth { get; init; }

    /// <summary>
    /// Gets the individual's "official" gender as recognised by HMRC for payroll purposes.
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// Gets or sets the employee's email address, if known.
    /// </summary>
    public MailAddress? EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the employee's postal address.
    /// </summary>
    public PostalAddress PostalAddress { get; set; } = default!;
}