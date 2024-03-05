namespace RPLi.Tests;

public class RPLiTests
{
    [Fact]
    public void RendersClearText()
    {
        RPLi.Render("a").ShouldBe("a");
    }
}