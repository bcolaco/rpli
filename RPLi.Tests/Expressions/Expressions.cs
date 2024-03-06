namespace RPLi.Tests.Expressions;

public class Expressions
{
    [Fact]
    public void CanBeADoubleQuoteString()
    {
        RPLi.Render("${\"a\"}").ShouldBe("a");
    }

    [Fact]
    public void CanBeASingleQuoteString()
    {
        RPLi.Render("${'a'}").ShouldBe("a");
    }

    [Fact]
    public void CanBeANumber()
    {
        RPLi.Render("${1.2}").ShouldBe("1.2");
    }

    [Fact]
    public void IgnoresWhiteSpaces()
    {
        RPLi.Render("${ 1 +  2\r\n+3\t}").ShouldBe("6");
    }
}
