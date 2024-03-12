namespace Example.Tests;

using System.IO;
using RPLi;
using Snapshooter.Xunit;

public class Example
{
    [Fact]
    public void SimpleHTML()
    {
        var projectDirectory = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
        var rpl = File.ReadAllText(Path.Join(projectDirectory, "example.rpl"));

        var document = RPLi.Render(rpl);

        Snapshot.Match(document);
    }
}
