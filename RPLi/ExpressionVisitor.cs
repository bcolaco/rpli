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

    public override Value VisitBoolean([NotNull] RplParser.BooleanContext context)
    {
        return context.GetText() switch
        {
            "true" => Boolean.True,
            "false" => Boolean.False,
            _ => throw new NotImplementedException(),
        };
    }

    public override Value VisitAddExpression([NotNull] RplParser.AddExpressionContext context)
    {
        var leftExpression = this.Visit(context.expression()[0]);
        var rightExpression = this.Visit(context.expression()[1]);

        return leftExpression.Add(rightExpression);
    }

    public override Value VisitSubtractExpression([NotNull] RplParser.SubtractExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]).As<Number>();
        var right = this.Visit(context.expression()[1]).As<Number>();

        return left.Subtract(right);
    }

    public override Value VisitMultiplyExpression([NotNull] RplParser.MultiplyExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]).As<Number>();
        var right = this.Visit(context.expression()[1]).As<Number>();

        return left.Multiply(right);
    }

    public override Value VisitDivideExpression([NotNull] RplParser.DivideExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]).As<Number>();
        var right = this.Visit(context.expression()[1]).As<Number>();

        return left.Divide(right);
    }

    public override Value VisitModulusExpression([NotNull] RplParser.ModulusExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]).As<Number>();
        var right = this.Visit(context.expression()[1]).As<Number>();

        return left.Modulus(right);
    }

    public override Value VisitLogicalAndExpression([NotNull] RplParser.LogicalAndExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]).As<Boolean>();
        var right = this.Visit(context.expression()[1]).As<Boolean>();

        return left.And(right);
    }

    public override Value VisitLogicalOrExpression([NotNull] RplParser.LogicalOrExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]).As<Boolean>();
        var right = this.Visit(context.expression()[1]).As<Boolean>();

        return left.Or(right);
    }

    public override Value VisitNotExpression([NotNull] RplParser.NotExpressionContext context)
    {
        var expression = this.Visit(context.expression()).As<Boolean>();

        return expression.Not();
    }

    public override Value VisitCompareExpression([NotNull] RplParser.CompareExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]);
        var right = this.Visit(context.expression()[1]);

        return left.Equal(right);
    }
}
