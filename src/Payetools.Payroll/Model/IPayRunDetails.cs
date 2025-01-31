// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents key details about a given payrun.
/// </summary>
public interface IPayRunDetails
{
    /// <summary>
    /// Gets the <see cref="PayDate"/> for this payrun, which provides access to the pay date and the pay frequency.
    /// </summary>
    PayDate PayDate { get; }

    /// <summary>
    /// Gets the start and end dates of the pay period that pertains to this payrun, in the form of a <see cref="DateRange"/>.
    /// </summary>
    DateRange PayPeriod { get; }
}
