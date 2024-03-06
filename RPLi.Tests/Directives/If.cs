namespace RPLi.Tests.Directives;

public class If
{
    [Fact]
    public void TrueRendersIfBlock()
    {
        RPLi.Render("<#if true>a</#if>").ShouldBe("a");
    }

    [Fact]
    public void FalseDoesNotRenderIfBlock()
    {
        RPLi.Render("<#if false>a</#if>").ShouldBe("");
    }

    [Fact]
    public void FalseRendersElseBlock()
    {
        RPLi.Render("<#if false>a<#else>b</#if>").ShouldBe("b");
    }

    [Fact]
    public void ElseIfTrueRendersElseIfBlock()
    {
        RPLi.Render("<#if false>a<#elseif true>b<#else>c</#if>").ShouldBe("b");
    }
}
