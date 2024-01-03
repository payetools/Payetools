// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.Payroll.Model;

namespace Payetools.Payroll.Payruns;

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
    IPayrunProcessor GetProcessor(IEmployer employer, PayDate payDate, PayReferencePeriod payPeriod);
}
