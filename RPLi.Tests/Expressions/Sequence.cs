namespace RPLi.Tests.Expressions;

public class SequenceTests
{
    [Fact]
    public void CanBeAnEmptySequence()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign a = [] >", ns);

        new Sequence([]).Equal(ns["a"]).Value.ShouldBeTrue();
    }

    [Fact]
    public void CanBeSingleElement()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign a = [1] >", ns);

        new Sequence([new Number(1)]).Equal(ns["a"]).Value.ShouldBeTrue();
    }

    [Fact]
    public void CanBeMultipleElements()
    {
        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign a = [1, 2] >", ns);

        new Sequence([new Number(1), new Number(2)]).Equal(ns["a"]).Value.ShouldBeTrue();
    }
}