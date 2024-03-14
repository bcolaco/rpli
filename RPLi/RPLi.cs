namespace RPLi;

using System.IO;
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
        parser.AddErrorListener(new ErrorListener());
        var visitor = new TempalteVisitor(ns);
        var result = visitor.Visit(parser.template());

        return result.Text;
    }

    class ErrorListener : IAntlrErrorListener<IToken>
    {
        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new Exception(msg);
        }
    }
}
