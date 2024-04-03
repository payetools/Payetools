// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents key details about a given payrun.
/// </summary>
public readonly struct PayRunDetails : IPayRunDetails
{
    /// <summary>
    /// Gets the <see cref="PayDate"/> for this payrun, which provides access to the pay date and the pay frequency.
    /// </summary>
    public PayDate PayDate { get; }

    /// <summary>
    /// Gets the start and end dates of the pay period that pertains to this payrun, in the form of a <see cref="DateRange"/>.
    /// </summary>
    public DateRange PayPeriod { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PayRunDetails"/> struct.
    /// </summary>
    /// <param name="payDate">Pay date.</param>
    /// <param name="payPeriod">Pay period.</param>
    public PayRunDetails(PayDate payDate, DateRange payPeriod)
    {
        PayDate = payDate;
        PayPeriod = payPeriod;
    }
}
