// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that provides access to information about an employee's partner, typically used in
/// Shared Parental Leave/Pay scenarios.
/// </summary>
public interface IEmployeePartnerDetails
{
    /// <summary>
    /// Gets or sets the name information (first name, last name, etc.) of the employee's partner.
    /// </summary>
    INamedPerson NameInfo { get; set; }

    /// <summary>
    /// Gets or sets the National Insurance number of the employee.
    /// </summary>
    NiNumber NiNumber { get; set; }
}
