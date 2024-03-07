namespace RPLi.Tests.Expressions;

public class Boolean
{
    [Fact]
    public void CanSpecifyTrue()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign v=true>", ns);

        ((global::RPLi.Boolean)ns["v"]).Value.ShouldBe(true);
    }

    [Fact]
    public void CanSpecifyFalse()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign v=false>", ns);

        ((global::RPLi.Boolean)ns["v"]).Value.ShouldBe(false);
    }

    [Fact]
    public void And()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign v = true && false>", ns);

        ((global::RPLi.Boolean)ns["v"]).Value.ShouldBe(false);
    }

    [Fact]
    public void Or()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign v = true || false>", ns);

        ((global::RPLi.Boolean)ns["v"]).Value.ShouldBe(true);
    }

    [Fact]
    public void Not()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign v = !false>", ns);

        ((global::RPLi.Boolean)ns["v"]).Value.ShouldBe(true);
    }

    [Fact]
    public void Equality()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign v = 1 == 1>", ns);

        ((global::RPLi.Boolean)ns["v"]).Value.ShouldBe(true);
    }

    [Fact]
    public void Inequality()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign v = 1 != 2>", ns);

        ((global::RPLi.Boolean)ns["v"]).Value.ShouldBe(true);
    }

    [Fact]
    public void LessThan()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign v = 1 < 2>", ns);

        ((global::RPLi.Boolean)ns["v"]).Value.ShouldBe(true);
    }
}
