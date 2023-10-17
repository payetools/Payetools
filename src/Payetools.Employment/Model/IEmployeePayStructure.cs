// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Employment.Model;

/// <summary>
/// Interface that represents an employee's pay structure.
/// </summary>
public interface IEmployeePayStructure
{
    /// <summary>
    /// Gets the unique ID for this pay structure.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the rate of pay.  The type of this rate of pay is given by <see cref="PayRateType"/>.
    /// </summary>
    decimal PayRate { get; }

    /// <summary>
    /// Gets the type of pay that <see cref="PayRate"/> represents.
    /// </summary>
    PayRateType PayRateType { get; }

    /// <summary>
    /// Gets the pay component that this pay structure is based on.
    /// </summary>
    IEarningsDetails PayComponent { get; }
}
