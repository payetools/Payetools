// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents a new employee's starter declaration, if provided.  In the case of high earners, the
/// starter declaration may be omitted and the employee put on a 0T tax code rather than a BR or
/// "emergency tax code", in order to avoid paying insufficient tax.
/// </summary>
public enum StarterDeclaration
{
    /// <summary>
    /// HMRC: "This is my first job since 6 April and since the 6 April I have not received
    /// payments from any of the following:
    /// <list type="bullet">
    /// <item>Jobseeker's Allowance</item>
    /// <item>Employment and Support Allowance</item>
    /// <item>Incapacity Benefit</item>
    /// </list>".
    /// </summary>
    A,

    /// <summary>
    /// HMRC: "Since 6 April I have had another job but I do not have a P45. And/or since
    /// the 6 April I have received payments from any of the following:
    /// <list type="bullet">
    /// <item>Jobseeker's Allowance</item>
    /// <item>Employment and Support Allowance</item>
    /// <item>Incapacity Benefit</item>
    /// </list>".
    /// </summary>
    B,

    /// <summary>
    ///  have another job and/or I am in receipt of a State, workplace or private pension.
    /// </summary>
    C,

    /// <summary>
    /// No Starter Declaration captured as the employee is a high earner.
    /// </summary>
    HighEarnerNotRecorded
}