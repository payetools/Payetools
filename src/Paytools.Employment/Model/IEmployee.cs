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
using System.Net.Mail;

namespace Paytools.Employment.Model;

/// <summary>
/// Interface that represents an employee for payroll purposes.
/// </summary>
public interface IEmployee : INamedIndividual
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
    UkPostalAddress PostalAddress { get; set; }
}