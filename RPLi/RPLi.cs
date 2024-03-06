namespace RPLi;

using System.Text.RegularExpressions;
using Antlr4.Runtime;
using Lextm.AnsiC;

public class RPLi
{
    public static string Render(string rpl)
    {
        return Render(rpl, new Dictionary<string, Value>());
    }

    public static string Render(string rpl, IDictionary<string, Value> ns)
    {
        var inputStream = new AntlrInputStream(rpl);
        var lexer = new RplLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new RplParser(tokenStream);
        var visitor = new TempalteVisitor(ns);
        rpl = visitor.Visit(parser.template());

        return rpl;
    }
}
