// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.Payroll.Model;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Interface that represents factory objects that create instances that implement <see cref="IPayRunProcessor"/>.
/// </summary>
public interface IPayRunProcessorFactory
{
    /// <summary>
    /// Gets a payrun processor for specified pay date and pay period.
    /// </summary>
    /// <param name="payDate">Applicable pay date for the required payrun processor.</param>
    /// <param name="payPeriod">Applicable pay period for required payrun processor.</param>
    /// <returns>An implementation of <see cref="IPayRunProcessor"/> for the specified pay date
    /// and pay period.</returns>
    IPayRunProcessor GetProcessor(PayDate payDate, DateRange payPeriod);

    /// <summary>
    /// Gets a payrun processor for specified pay run details.
    /// </summary>
    /// <param name="payRunDetails">Pay run detaiils that provide applicable pay date and
    /// pay period for the required payrun processor.</param>
    /// <returns>An implementation of <see cref="IPayRunProcessor"/> for the specified pay date
    /// and pay period.</returns>
    IPayRunProcessor GetProcessor(IPayRunDetails payRunDetails);
}
