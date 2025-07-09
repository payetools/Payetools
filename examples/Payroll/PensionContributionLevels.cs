// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Model;
using Payetools.Payroll.PayRuns;

namespace Payroll;

public class PayrollPayRunInputs : IPayrollPayRunInputs<int>
{
    public int PayRunId { get; init; }

    public required IPayRunDetails PayRunDetails { get; init; }

    public required IEnumerable<IEmployeePayRunInputs<int>> EmployeePayRunInputs { get; init; }
}