namespace RPLi.Tests.Expressions;

public class Aritmetic
{
    [Fact]
    public void CanAddNumbers()
    {
        RPLi.Render("${1+1}").ShouldBe("2");
    }

    [Fact]
    public void CanSubtractNumbers()
    {
        RPLi.Render("${3-2}").ShouldBe("1");
    }

    [Fact]
    public void CanMultiplyNumbers()
    {
        RPLi.Render("${2*3}").ShouldBe("6");
    }
}
