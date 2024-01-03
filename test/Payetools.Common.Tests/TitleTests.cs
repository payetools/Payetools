// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

public class TitleTests
{
    [Fact]
    public void TestStandardisedTitles()
    {
        Title.Parse("mr").ToString().Should().Be("Mr");
        Title.Parse("MR").ToString().Should().Be("Mr");
        Title.Parse("Mr.").ToString().Should().Be("Mr");

        Title.Parse("mrs").ToString().Should().Be("Mrs");
        Title.Parse("mrs.").ToString().Should().Be("Mrs");
        Title.Parse("MRS").ToString().Should().Be("Mrs");

        Title.Parse("ms").ToString().Should().Be("Ms");
        Title.Parse("MS").ToString().Should().Be("Ms");

        Title.Parse("Miss").ToString().Should().Be("Miss");
        Title.Parse("MISS").ToString().Should().Be("Miss");

        Title.Parse("prof").ToString().Should().Be("Prof.");
        Title.Parse("PROF.").ToString().Should().Be("Prof.");
        Title.Parse("Professor").ToString().Should().Be("Prof.");

        Title.Parse("dr").ToString().Should().Be("Dr.");
        Title.Parse("DR.").ToString().Should().Be("Dr.");
        Title.Parse("DOCTOR").ToString().Should().Be("Dr.");

        Title.Parse("rev").ToString().Should().Be("Rev.");
        Title.Parse("rev.").ToString().Should().Be("Rev.");
        Title.Parse("REVEREND").ToString().Should().Be("Rev.");
        Title.Parse("REVD").ToString().Should().Be("Rev.");
        Title.Parse("Revd.").ToString().Should().Be("Rev.");
    }

    [Fact]
    public void TestEmptyTitles()
    {
        Title.Parse("").Should().BeNull();
        Title.Parse("   ").Should().BeNull();
    }

    [Fact]
    public void TestNonStandardisedTitle()
    {
        Title.Parse("The Right Honourable").ToString().Should().Be("The Right Honourable");
    }

    [Fact]
    public void TestOverlengthTitle()
    {
        Action action = () => Title.Parse("The Right Honourable Mighty One and Only Most Majestic And Humble");

        action.Should().Throw<ArgumentException>()
            .WithMessage("Titles may not exceed 35 characters in length (Parameter 'title')");
    }

    [Fact]
    public void TestImplicitCasts()
    {
        string? value = Title.Parse("mr");
        value.Should().Be("Mr");

        value = Title.Parse("");
        value.Should().BeNull();
    }
}
