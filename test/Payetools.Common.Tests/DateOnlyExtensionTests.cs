// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Payetools.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payetools.Common.Tests;

public class DateOnlyExtensionTests
{
    [Fact]
    public void TestAgeAtCalculations()
    {
        var date = new DateOnly(2022, 1, 1);
        var dateOfBirth = new DateOnly(2000, 1, 1);
        dateOfBirth.AgeAt(date).Should().Be(22, $"date of birth = {dateOfBirth} and date = {date}");

        date = new DateOnly(2022, 12, 31);
        dateOfBirth = new DateOnly(2000, 1, 1);
        dateOfBirth.AgeAt(date).Should().Be(22, $"date of birth = {dateOfBirth} and date = {date}");

        date = new DateOnly(2023, 1, 1);
        dateOfBirth = new DateOnly(2000, 1, 1);
        dateOfBirth.AgeAt(date).Should().Be(23, $"date of birth = {dateOfBirth} and date = {date}");

        date = new DateOnly(2023, 2, 28);
        dateOfBirth = new DateOnly(2000, 2, 28);
        dateOfBirth.AgeAt(date).Should().Be(23, $"date of birth = {dateOfBirth} and date = {date}");

        date = new DateOnly(2023, 2, 28);
        dateOfBirth = new DateOnly(2000, 2, 29);
        dateOfBirth.AgeAt(date).Should().Be(22, $"date of birth = {dateOfBirth} and date = {date}");

        date = new DateOnly(2023, 3, 1);
        dateOfBirth = new DateOnly(2000, 2, 29);
        dateOfBirth.AgeAt(date).Should().Be(23, $"date of birth = {dateOfBirth} and date = {date}");
    }
}
