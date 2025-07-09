// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.ReferenceData;
using Payetools.StudentLoans;
using Payetools.Testing.Utils;

namespace Payetools.NationalMinimumWage.Tests;

public class StudentLoanCalculatorFactoryDataFixture : CalculatorFactoryDataFixture<IStudentLoanCalculatorFactory>
{
    protected override IStudentLoanCalculatorFactory MakeFactory(IHmrcReferenceDataProvider provider) =>
        new StudentLoanCalculatorFactory(provider);
}