namespace RPLi.Tests.Directives;

public class List
{
    [Fact]
    public void RendersTheNumberOfItems()
    {
        var document = RPLi.Render("<#list [1, 2, 3] as x>a</#list>");
        
        document.ShouldBe("aaa");
    }

    [Fact]
    public void RendersTheItemValues()
    {
        var document = RPLi.Render("<#list ['a', 'b', 'c'] as x>${x}</#list>");
        
        document.ShouldBe("abc");
    }

    [Fact]
    public void DefinesItemIndex()
    {
        var document = RPLi.Render("<#list ['a', 'b', 'c'] as x>${x_index}</#list>");
        
        document.ShouldBe("012");
    }

    [Fact]
    public void DefinesItemHasNext()
    {
        var document = RPLi.Render("<#list ['a', 'b', 'c'] as x>${x}<#if x_has_next>,</#if></#list>");
        
        document.ShouldBe("a,b,c");
    }
}
