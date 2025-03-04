// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Shouldly;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

public class TitleTests
{
    [Fact]
    public void TestStandardisedTitles()
    {
        Title.Parse("mr").ToString().ShouldBe("Mr");
        Title.Parse("MR").ToString().ShouldBe("Mr");
        Title.Parse("Mr.").ToString().ShouldBe("Mr");

        Title.Parse("mrs").ToString().ShouldBe("Mrs");
        Title.Parse("mrs.").ToString().ShouldBe("Mrs");
        Title.Parse("MRS").ToString().ShouldBe("Mrs");

        Title.Parse("ms").ToString().ShouldBe("Ms");
        Title.Parse("MS").ToString().ShouldBe("Ms");

        Title.Parse("Miss").ToString().ShouldBe("Miss");
        Title.Parse("MISS").ToString().ShouldBe("Miss");

        Title.Parse("prof").ToString().ShouldBe("Prof.");
        Title.Parse("PROF.").ToString().ShouldBe("Prof.");
        Title.Parse("Professor").ToString().ShouldBe("Prof.");

        Title.Parse("dr").ToString().ShouldBe("Dr.");
        Title.Parse("DR.").ToString().ShouldBe("Dr.");
        Title.Parse("DOCTOR").ToString().ShouldBe("Dr.");

        Title.Parse("rev").ToString().ShouldBe("Rev.");
        Title.Parse("rev.").ToString().ShouldBe("Rev.");
        Title.Parse("REVEREND").ToString().ShouldBe("Rev.");
        Title.Parse("REVD").ToString().ShouldBe("Rev.");
        Title.Parse("Revd.").ToString().ShouldBe("Rev.");
    }

    [Fact]
    public void TestEmptyTitles()
    {
        Title.Parse("").ShouldBeNull();
        Title.Parse("   ").ShouldBeNull();
    }

    [Fact]
    public void TestNonStandardisedTitle()
    {
        Title.Parse("The Right Honourable").ToString().ShouldBe("The Right Honourable");
    }

    [Fact]
    public void TestOverlengthTitle()
    {
        Action action = () => Title.Parse("The Right Honourable Mighty One and Only Most Majestic And Humble");

        action.ShouldThrow<ArgumentException>()
            .Message.ShouldBe("Titles may not exceed 35 characters in length (Parameter 'title')");
    }

    [Fact]
    public void TestImplicitCasts()
    {
        string? value = Title.Parse("mr");
        value.ShouldBe("Mr");

        value = Title.Parse("");
        value.ShouldBeNull();
    }
}
