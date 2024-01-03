// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using System.Net.Mail;

namespace Payetools.Employment.Model;

/// <summary>
/// Interface that represents an employee for payroll purposes.
/// </summary>
public interface IEmployee : INamedPerson
{
    /// <summary>
    /// Gets the individual's National Insurance number.
    /// </summary>
    NiNumber NiNumber { get; init; }

    /// <summary>
    /// Gets the individual's date of birth.
    /// </summary>
    DateOnly DateOfBirth { get; init; }

    /// <summary>
    /// Gets the individual's "official" gender as recognised by HMRC for payroll purposes.
    /// </summary>
    Gender Gender { get; init; }

    /// <summary>
    /// Gets or sets the employee's email address, if known.
    /// </summary>
    MailAddress? EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the employee's postal address.
    /// </summary>
    PostalAddress PostalAddress { get; set; }
}