// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Testing.Data.NationalInsurance;
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
