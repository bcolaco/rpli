namespace RPLi;

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Lextm.AnsiC;

class ExpressionVisitor : RplParserBaseVisitor<Value>
{
    protected override Value AggregateResult(Value aggregate, Value nextResult)
    {
        if (aggregate is null)
            return nextResult;
        
        return new String(string.Concat(aggregate, nextResult));
    }
    
    public override Value VisitTerminal(ITerminalNode node)
    {
        switch(node.Symbol.Type)
        {
            case RplLexer.EXPR_SYMBOL:
            case RplLexer.DQS_CONTENT:
            case RplLexer.SQS_CONTENT:
                return new String(node.GetText());
            default:
                return base.VisitTerminal(node);
        };
    }

    public override Value VisitNumber([NotNull] RplParser.NumberContext context)
    {
        return new Number(double.Parse(context.GetText()));
    }

    public override Value VisitAddExpression([NotNull] RplParser.AddExpressionContext context)
    {
        var leftExpression = this.Visit(context.expression()[0]);
        var rightExpression = this.Visit(context.expression()[1]);

        return leftExpression.Add(rightExpression);
    }

    public override Value VisitSubtractExpression([NotNull] RplParser.SubtractExpressionContext context)
    {
        var leftExpression = this.Visit(context.expression()[0]);
        var rightExpression = this.Visit(context.expression()[1]);

        return leftExpression.Subtract(rightExpression);
    }

    public override Value VisitMultiplyExpression([NotNull] RplParser.MultiplyExpressionContext context)
    {
        var leftExpression = this.Visit(context.expression()[0]);
        var rightExpression = this.Visit(context.expression()[1]);

        return leftExpression.Multiply(rightExpression);
    }

    public override Value VisitDivideExpression([NotNull] RplParser.DivideExpressionContext context)
    {
        var leftExpression = this.Visit(context.expression()[0]);
        var rightExpression = this.Visit(context.expression()[1]);

        return leftExpression.Divide(rightExpression);
    }

    public override Value VisitModulusExpression([NotNull] RplParser.ModulusExpressionContext context)
    {
        var leftExpression = this.Visit(context.expression()[0]);
        var rightExpression = this.Visit(context.expression()[1]);

        return leftExpression.Modulus(rightExpression);
    }
}
