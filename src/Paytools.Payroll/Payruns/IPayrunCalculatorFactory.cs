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

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Interface that represents factory objects that create instances that implement <see cref="IPayrunCalculator"/>.
/// </summary>
public interface IPayrunCalculatorFactory
{
    /// <summary>
    /// Gets a calculator for specified pay date.
    /// </summary>
    /// <param name="payDate">Applicable pay date for the required calculator.</param>
    /// <returns>An implementation of <see cref="IPayrunCalculator"/> for the specified pay date.</returns>
    Task<IPayrunCalculator> GetCalculatorAsync(PayDate payDate);
}
