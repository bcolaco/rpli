namespace RPLi.Tests.Expressions;

public class Expressions
{
    [Fact]
    public void CanBeANumber()
    {
        RPLi.Render("${1.2}").ShouldBe("1.2");
    }

    [Theory]
    [InlineData("+1", 1)]
    [InlineData("-1", -1)]
    public void CanSpecifyNumberSignal(string numberString, double expectedValue)
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render($"<#assign a = {numberString}/>", ns);

        ns["a"].ShouldBe(new Number(expectedValue));
    }

    [Fact]
    public void IgnoresWhiteSpaces()
    {
        RPLi.Render("${ 1 +  2\r\n+3\t}").ShouldBe("6");
    }
}
