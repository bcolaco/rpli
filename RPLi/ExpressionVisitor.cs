namespace RPLi;

using Antlr4.Runtime.Tree;
using Lextm.AnsiC;

class ExpressionVisitor : RplParserBaseVisitor<string>
{
    public override string VisitTerminal(ITerminalNode node)
    {
        switch(node.Symbol.Type)
        {
            case RplLexer.CONTENT:
            case RplLexer.EXPR_NUMBER:
            case RplLexer.EXPR_SYMBOL:
            case RplLexer.DQS_CONTENT:
            case RplLexer.SQS_CONTENT:
                return node.GetText();
            default:
                return base.VisitTerminal(node);
        };
    }
}