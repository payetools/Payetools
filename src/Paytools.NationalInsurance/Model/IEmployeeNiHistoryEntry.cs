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

using Paytools.Common.Model;

namespace Paytools.NationalInsurance.Model;

/// <summary>
/// Interface that represents once element of the employee's NI history during the current tax year.  For employees that have
/// only have one NI category throughout the tax year, they will have just one instance of <see cref="EmployeeNiHistoryEntry"/>
/// applicable.  But it is of course possible for an employee's NI category to change throughout the tax year (for example
/// because they turned 21 years of age), and in this case, multiple records must be held.
/// </summary>
public interface IEmployeeNiHistoryEntry
{
    /// <summary>
    /// Gets the National Insurance category letter pertaining to this record.
    /// </summary>
    NiCategory NiCategoryPertaining { get; }

    /// <summary>
    /// Gets the gross NI-able earnings applicable for the duration of this record.
    /// </summary>
    decimal GrossNicableEarnings { get; }

    /// <summary>
    /// Gets the total employee contribution made during the duration of this record.
    /// </summary>
    decimal EmployeeContribution { get; }

    /// <summary>
    /// Gets the total employer contribution made during the duration of this record.
    /// </summary>
    decimal EmployerContribution { get; }

    /// <summary>
    /// Gets the total contribution (employee + employer) made during the duration of this record.
    /// </summary>
    decimal TotalContribution { get; }

    /// <summary>
    /// Gets the earnings up to and including the Lower Earnings Limit for this record.
    /// </summary>
    decimal EarningsUpToAndIncludingLEL { get; }

    /// <summary>
    /// Gets the earnings up above the Lower Earnings Limit and up to and including the Secondary Threshold
    /// for this record.
    /// </summary>
    decimal EarningsAboveLELUpToAndIncludingST { get; }

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Primary Threshold
    /// for this record.
    /// </summary>
    decimal EarningsAboveSTUpToAndIncludingPT { get; }

    /// <summary>
    /// Gets the earnings up above the Primary Threshold and up to and including the Freeport Upper Secondary
    /// Threshold for this record.
    /// </summary>
    decimal EarningsAbovePTUpToAndIncludingFUST { get; }

    /// <summary>
    /// Gets the earnings up above the Freeport Upper Secondary Threshold and up to and including the Upper
    /// Earnings Limit for this record.
    /// </summary>
    decimal EarningsAboveFUSTUpToAndIncludingUEL { get; }

    /// <summary>
    /// Gets the earnings up above the Upper Earnings Limit for this record.
    /// </summary>
    decimal EarningsAboveUEL { get; }

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Upper Earnings Limit
    /// for this record.
    /// </summary>
    decimal EarningsAboveSTUpToAndIncludingUEL { get; }

    /// <summary>
    /// Adds the results of an NI calculation to the current history and returns a new instance of <see cref="IEmployeeNiHistoryEntry"/>
    /// with the results applied.
    /// </summary>
    /// <param name="result">NI calculation results to add to this history entry.</param>
    /// <returns>New instance of <see cref="IEmployeeNiHistoryEntry"/> with the NI calculation result applied.</returns>
    IEmployeeNiHistoryEntry Add(in INiCalculationResult result);
}