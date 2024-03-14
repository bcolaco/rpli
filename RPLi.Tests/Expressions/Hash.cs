namespace RPLi.Tests.Expressions;

public class HashTests
{
    [Fact]
    public void CanBeAnEmptyHash()
    {

        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign a = {} >", ns);

        new Hash().Equal(ns["a"]).Value.ShouldBeTrue();
    }

    [Fact]
    public void CanHaveAKeyAndValue()
    {

        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign a = {'b': 1} >", ns);

        new Hash(new Dictionary<string, Value>{ {"b", new Number(1)} }).Equal(ns["a"]).Value.ShouldBeTrue();
    }

    [Fact]
    public void CanHaveMultipleKeysAndValues()
    {

        var ns = new Dictionary<string, Value>();

        RPLi.Render("<#assign a = {'b': 1, 'c': 2, 'd': 3} >", ns);

        new Hash(new Dictionary<string, Value>{
            {"b", new Number(1)},
            {"c", new Number(2)},
            {"d", new Number(3)}
        }).Equal(ns["a"]).Value.ShouldBeTrue();
    }

    [Fact]
    public void CanReferenceHashValue()
    {
        RPLi.Render("${ {'a': 'b'}.a }").ShouldBe("b");
    }
}
