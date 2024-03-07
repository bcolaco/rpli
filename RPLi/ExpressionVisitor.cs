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
            "true" => new Boolean(true),
            "false" => new Boolean(false),
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
        var left = this.GetValue<Number>(context.expression()[0]);
        var right = this.GetValue<Number>(context.expression()[1]);

        return left.Subtract(right);
    }

    public override Value VisitMultiplyExpression([NotNull] RplParser.MultiplyExpressionContext context)
    {
        var left = this.GetValue<Number>(context.expression()[0]);
        var right = this.GetValue<Number>(context.expression()[1]);

        return left.Multiply(right);
    }

    public override Value VisitDivideExpression([NotNull] RplParser.DivideExpressionContext context)
    {
        var left = this.GetValue<Number>(context.expression()[0]);
        var right = this.GetValue<Number>(context.expression()[1]);

        return left.Divide(right);
    }

    public override Value VisitModulusExpression([NotNull] RplParser.ModulusExpressionContext context)
    {
        var left = this.GetValue<Number>(context.expression()[0]);
        var right = this.GetValue<Number>(context.expression()[1]);

        return left.Modulus(right);
    }

    public override Value VisitLogicalAndExpression([NotNull] RplParser.LogicalAndExpressionContext context)
    {
        var left = this.GetValue<Boolean>(context.expression()[0]);
        var right = this.GetValue<Boolean>(context.expression()[1]);

        return left.And(right);
    }

    public override Value VisitLogicalOrExpression([NotNull] RplParser.LogicalOrExpressionContext context)
    {
        var left = this.GetValue<Boolean>(context.expression()[0]);
        var right = this.GetValue<Boolean>(context.expression()[1]);

        return left.Or(right);
    }

    public override Value VisitNotExpression([NotNull] RplParser.NotExpressionContext context)
    {
        var expression = this.GetValue<Boolean>(context.expression());

        return expression.Not();
    }

    private T GetValue<T>(RplParser.ExpressionContext context)
        where T: Value
    {
        var value = this.Visit(context);

        if (value is not T typedValue)
            throw new Exception($"Expected a {typeof(T).Name} but found a {value.GetType().Name}");

        return typedValue;
    }
}
