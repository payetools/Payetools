// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Enumeration used to indicate whether De Minimis State Aid rules apply with regard to Employment Allowance.
/// </summary>
public enum StateAidForEmploymentAllowance
{
    /// <summary>This value should be selected if the employer is involved in economic activity and the
    /// business is in the agriculture sector.</summary>
    Agriculture,

    /// <summary>This value should be selected if the employer is involved in economic activity and the
    /// business is in the fisheries and aquaculture sector.</summary>
    FisheriesAndAquaculture,

    /// <summary>This value should be selected if the employer is involved in economic activity and the
    /// business is in the road transport sector.</summary>
    RoadTransport,

    /// <summary>This value should be selected if the employer is undertaking economic activity and none
    /// Agriculture, FisheriesAndAquaculture or RoadTransport apply.</summary>
    IndustrialOther,

    /// <summary>This value should be selected if the employer is not undertaking economic activity, for
    /// example, charities, community amateur sports clubs, employing someone to provide personal care.
    /// Here, de minimis state aid rules do not apply.</summary>
    NotApplicable
}