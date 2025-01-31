// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Testing.Data.NationalInsurance;
using System.Text;

namespace Payetools.Testing.Data
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
