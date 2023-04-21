// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using Paytools.Employment.Model;

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Interface that represents factory objects that create instances that implement <see cref="IPayrunProcessor"/>.
/// </summary>
public interface IPayrunProcessorFactory
{
    /// <summary>
    /// Gets a payrun processor for specified pay date and pay period.
    /// </summary>
    /// <param name="employer">Employer for this payrun processor.</param>
    /// <param name="payDate">Applicable pay date for the required payrun processor.</param>
    /// <param name="payPeriod">Applicable pay period for required payrun processor.</param>
    /// <returns>An implementation of <see cref="IPayrunProcessor"/> for the specified pay date
    /// and pay period.</returns>
    Task<IPayrunProcessor> GetProcessorAsync(IEmployer employer, PayDate payDate, PayReferencePeriod payPeriod);
}
