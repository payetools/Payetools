// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Enumeration that is used to indicate a band of normal hours worked by an employee.
/// </summary>
public enum NormalHoursWorkedBand
{
    /// <summary>Up to 15.99 hours</summary>
    A,

    /// <summary>16 to 23.99 hours</summary>
    B,

    /// <summary>24 to 29.99 hours</summary>
    C,

    /// <summary>30 hours or more</summary>
    D,

    /// <summary>Other</summary>
    E
}