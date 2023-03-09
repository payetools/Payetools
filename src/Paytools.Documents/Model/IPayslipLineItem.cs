// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Paytools.Documents.Model;

/// <summary>
/// Interface that represents a line item on a payslip.
/// </summary>
public interface IPayslipLineItem
{
    /// <summary>
    /// Gets the descriptive text for this line item.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets the optional quantity for this line item.  Null if not applicable.
    /// </summary>
    decimal? Quantity { get;  }

    /// <summary>
    /// Gets the optional units for this line item.  Null if not applicable.
    /// </summary>
    string? Units { get; }

    /// <summary>
    /// Gets the optional rate for this line item.  Null if not applicable.
    /// </summary>
    decimal? Rate { get; }

    /// <summary>
    /// Gets the amount for this payslip period.
    /// </summary>
    decimal AmountForPeriod { get; }

    /// <summary>
    /// Gets the amount for the tax year to date.
    /// </summary>
    decimal AmountYtd { get; }
}
