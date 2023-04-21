// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Employment.Model;

namespace Paytools.Payroll;

/// <summary>
/// Not sure what this is here for.
/// </summary>
public interface IEmployerInfoProvider
{
    /// <summary>
    /// Gets Not sure what this is here for.
    /// </summary>
    IEmployer Employer { get; }
}
