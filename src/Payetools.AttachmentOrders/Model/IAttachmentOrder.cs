// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Calculators;
using Payetools.Common.Model;

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Interface that represents an attachment order.
/// </summary>
public interface IAttachmentOrder
{
    /// <summary>
    /// Gets the calculation behaviours to use for this attachment of earnings order.
    /// </summary>
    AttachmentOrderCalculationTraits CalculationBehaviours { get; }

    /// <summary>
    /// Gets the date on which the attachment order was issued; this may be used to determine the rates and
    /// thresholds that apply to the order. May be <see langword="null"/> if the order does note carry an
    /// issue date, such as in the case of Arrestment of Earnings orders (AEs) in Scotland.
    /// </summary>
    DateOnly? IssueDate { get; }

    /// <summary>
    /// Gets the date on which the attachment order was received by the employer or payroll department.
    /// </summary>
    DateOnly ReceivedDate { get; }

    /// <summary>
    /// Gets the date from which this attachment of earnings order is effective.
    /// </summary>
    DateOnly EffectiveDate { get; }

    /// <summary>
    /// Gets the date on which to cease applying this attachment of earnings order. This
    /// value is the last date on which the order should be applied.
    /// </summary>
    DateOnly CeaseDate { get; }

    /// <summary>
    /// Gets a value indicating whether this attachment order is a priority order.
    /// </summary>
    bool IsPriorityOrder { get; }

    /// <summary>
    /// Gets the optional rate type that applies to this attachment order, if applicable.
    /// </summary>
    AttachmentOrderRateType? RateType { get; }

    /// <summary>
    /// Gets the employee's pay frequency as it applies to this order.
    /// </summary>
    AttachmentOrderPayFrequency EmployeePayFrequency { get; }

    /// <summary>
    /// Gets the jurisdiction for which this attachment order is applicable. May be more
    /// than one sub-country.
    /// </summary>
    CountriesForTaxPurposes ApplicableJurisdiction { get; }

    /// <summary>
    /// Gets the payment amount that applies to this attachment order, if applicable, expressed in
    /// terms of the employee's pay frequency, typically weekly or monthly.
    /// </summary>
    decimal? PayFrequencyPeriodAmount { get; }

    /// <summary>
    /// Gets the protected earnings amount that applies to this attachment order, if applicable,
    /// expressed in terms of the employee's pay frequency, typically weekly or monthly.
    /// </summary>
    decimal? ProtectedEarnings { get; }

    /// <summary>
    /// Gets a value indicating whether the protected earnings amount is expressed as a percentage
    /// of the employee's net earnings, or as a fixed amount per pay period.
    /// </summary>
    bool ProtectedEarningsIsPercentage { get; }

    /// <summary>
    /// Gets a value indicating whether an admin charge should be applied to this attachment order.
    /// Note that this is not applied if the charge would reduce the employee's earnings below the
    /// National Minimum Wage (NMW) or National Living Wage (NLW).
    /// </summary>
    bool ApplyAdminCharge { get; }

    /// <summary>
    /// Gets the total amount payable for this attachment order, if applicable.
    /// </summary>
    decimal? TotalAmountPayable { get; }
}