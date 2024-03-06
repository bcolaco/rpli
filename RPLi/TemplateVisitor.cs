namespace RPLi;

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Lextm.AnsiC;

class TempalteVisitor : RplParserBaseVisitor<string>
{
    protected override string AggregateResult(string aggregate, string nextResult)
    {
        return string.Concat(aggregate, nextResult);
    }

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

    public override string VisitAddExpression([NotNull] RplParser.AddExpressionContext context)
    {
        var leftExpression = new Number(double.Parse(context.expression()[0].GetText()));
        var rightExpression = new Number(double.Parse(context.expression()[1].GetText()));

        return leftExpression.Add(rightExpression).ToString() ?? string.Empty;
    }

    public override string VisitSubtractExpression([NotNull] RplParser.SubtractExpressionContext context)
    {
        var leftExpression = new Number(double.Parse(context.expression()[0].GetText()));
        var rightExpression = new Number(double.Parse(context.expression()[1].GetText()));

        return leftExpression.Subtract(rightExpression).ToString() ?? string.Empty;
    }
}
