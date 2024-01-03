// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Payroll.Model;

namespace Payetools.Payroll;

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
