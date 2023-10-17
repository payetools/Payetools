// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Common.Model;

/// <summary>
/// Enum representing a person's gender.  Note that HMRC only recognises the genders male and female for PAYE purposes, hence
/// only two options (plus unknown) are provided.
/// </summary>
public enum Gender
{
    /// <summary>Not known/undefined</summary>
    Unknown,

    /// <summary>Male</summary>
    Male,

    /// <summary>Female</summary>
    Female
}