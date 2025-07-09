// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
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
    /// Gets the name information (first name, last name, etc.) of the employee's partner.
    /// </summary>
    INamedPerson NameInfo { get; }

    /// <summary>
    /// Gets the National Insurance number of the employee.
    /// </summary>
    NiNumber NiNumber { get; }
}