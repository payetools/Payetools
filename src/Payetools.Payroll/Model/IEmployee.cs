﻿// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Net.Mail;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents an employee for payroll purposes.
/// </summary>
public interface IEmployee : INamedPerson
{
    /// <summary>
    /// Gets the individual's National Insurance number.
    /// </summary>
    NiNumber NiNumber { get; }

    /// <summary>
    /// Gets the individual's date of birth.
    /// </summary>
    DateOnly DateOfBirth { get; }

    /// <summary>
    /// Gets the individual's "official" gender as recognised by HMRC for payroll purposes.
    /// </summary>
    Gender Gender { get; }

    /// <summary>
    /// Gets or sets the employee's email address, if known.
    /// </summary>
    MailAddress? EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the employee's postal address.
    /// </summary>
    PostalAddress PostalAddress { get; set; }

    /// <summary>
    /// Gets or sets the employee's passport number, if known.
    /// </summary>
    string? PassportNumber { get; set; }

    /// <summary>
    /// Gets or sets the employee's partner information, where appropriate.
    /// </summary>
    IEmployeePartnerDetails? PartnerDetails { get; set; }
}