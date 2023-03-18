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

using Paytools.Common.Model;
using Paytools.Employment.Model;
using System.Net.Mail;

namespace Paytools.Payroll.Model;

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
    public string[]? Initials { get; init; }

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
    /// Gets a value indicating whether the individual has supplied a middle name.
    /// </summary>
    public bool HasMiddleName { get; }

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