namespace RPLi;

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Lextm.AnsiC;

class TempalteVisitor : RplParserBaseVisitor<string>
{
    private readonly ExpressionVisitor expressionVisitor = new ExpressionVisitor();
    
    protected override string AggregateResult(string aggregate, string nextResult)
    {
        return string.Concat(aggregate, nextResult);
    }

    public override string VisitTerminal(ITerminalNode node)
    {
        switch(node.Symbol.Type)
        {
            case RplLexer.CONTENT:
                return node.GetText();
            default:
                return base.VisitTerminal(node);
        };
    }

    public override string VisitExpressionElement([NotNull] RplParser.ExpressionElementContext context)
    {
        return this.expressionVisitor.Visit(context.expression()).ToString() ?? string.Empty;
    }
}
