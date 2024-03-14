namespace RPLi;

public class RenderResult
{
    public static readonly RenderResult Empty = new RenderResult(string.Empty);
    
    public string Text { get; }
    public bool IsBreaking { get; }

    public RenderResult(string text) : this(text, false)
    {
    }

    public RenderResult(string text, bool isBreaking)
    {
        this.Text = text;
        this.IsBreaking = isBreaking;
    }
}
