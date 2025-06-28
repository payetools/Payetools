// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Enum representing a person's gender.  Note that HMRC only recognises the genders male and female for PAYE purposes, hence
/// only two options (plus unknown) are provided.
/// </summary>
public enum Gender
{
    /// <summary>Not known/undefined.</summary>
    Unknown,

    /// <summary>Male.</summary>
    Male,

    /// <summary>Female.</summary>
    Female
}