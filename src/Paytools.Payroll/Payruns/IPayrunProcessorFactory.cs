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
