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

using FluentAssertions;
using Paytools.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paytools.Common.Tests;

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
