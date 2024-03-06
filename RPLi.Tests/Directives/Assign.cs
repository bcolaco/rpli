namespace RPLi.Tests.Directives;

public class Assign
{
    [Fact]
    public void ReturnsEmptyText()
    {
        RPLi.Render("<#assign a='b'>").ShouldBe(string.Empty);
    }

    [Fact]
    public void SetsNewVariable()
    {
        var ns = new Dictionary<string, Value>();
        RPLi.Render("<#assign a='b'>", ns);
        ns["a"].ToString().ShouldBe("b");
    }

    [Fact]
    public void ReplacesExistingVariableValue()
    {
        var ns = new Dictionary<string, Value>()
        {
            ["a"] = new Number(0),
        };

        RPLi.Render("<#assign a='b'>", ns);
        
        ns["a"].ToString().ShouldBe("b");
    }
}
