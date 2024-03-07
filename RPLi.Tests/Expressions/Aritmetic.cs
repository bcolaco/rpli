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

    [Fact]
    public void CanDivideNumbers()
    {
        RPLi.Render("${3/2}").ShouldBe("1.5");
    }

    [Fact]
    public void CanCalculateModulusNumbers()
    {
        RPLi.Render("${3%2}").ShouldBe("1");
    }

    [Fact]
    public void MultiplicationPriority()
    {
        RPLi.Render("${1+2*3}").ShouldBe("7");
    }

    [Fact]
    public void ParenthesisPriority()
    {
        RPLi.Render("${(1+2)*3}").ShouldBe("9");
    }
}
