namespace RPLi.Tests;

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
}
