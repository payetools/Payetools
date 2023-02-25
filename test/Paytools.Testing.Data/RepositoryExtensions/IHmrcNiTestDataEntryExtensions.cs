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

using System.Text;

namespace Paytools.Testing.Data
{
    public static class IHmrcNiTestDataEntryExtensions
    {
        public static string ToDebugString(this IHmrcNiTestDataEntry entry)
        {
            var sb = new StringBuilder();

            sb.Append($"{{ TestIdentifier: '{entry.TestIdentifier}', ");
            sb.Append($"PayFrequency: {entry.PayFrequency}, ");
            sb.Append($"Period: {entry.Period}, ");
            sb.Append($"GrossPay: {entry.GrossPay}, ");
            sb.Append($"EmployeeNiContribution: {entry.EmployeeNiContribution}, ");
            sb.Append($"EmployerNiContribution: {entry.EmployerNiContribution}, ");
            sb.Append(" }");

            return sb.ToString();
        }
    }
}
