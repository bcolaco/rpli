namespace RPLi.Tests.Expressions;

public class String
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
    public void CanConcatenateStrings()
    {
        RPLi.Render("${'a' + 'b'}").ShouldBe("ab");
    }
}
