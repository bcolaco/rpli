namespace RPLi.Tests;

public class RPLiTests
{
    [Fact]
    public void RendersClearText()
    {
        RPLi.Render("a").ShouldBe("a");
    }

    [Fact]
    public void IgnoresComments()
    {
        RPLi.Render("a<#--c-->b").ShouldBe("ab");
    }

    [Fact]
    public void SupportsInterpolations()
    {
        RPLi.Render("${\"a\"}").ShouldBe("a");
    }
}