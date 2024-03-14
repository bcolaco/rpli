namespace RPLi;

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Lextm.AnsiC;

class ExpressionVisitor(IDictionary<string, Value> Namespace) : RplParserBaseVisitor<Value>
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
                return Namespace[node.GetText()];
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

    public override Value VisitEqualityExpression([NotNull] RplParser.EqualityExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]);
        var right = this.Visit(context.expression()[1]);

        return left.Equal(right);
    }

    public override Value VisitInequalityExpression([NotNull] RplParser.InequalityExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]);
        var right = this.Visit(context.expression()[1]);

        return left.Equal(right).Not();
    }

    public override Value VisitLessThanExpression([NotNull] RplParser.LessThanExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]).As<Number>();
        var right = this.Visit(context.expression()[1]).As<Number>();

        return left.LessThan(right);
    }

    public override Value VisitLessThanOrEqualExpression([NotNull] RplParser.LessThanOrEqualExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]).As<Number>();
        var right = this.Visit(context.expression()[1]).As<Number>();

        return left.LessThan(right).Or(left.Equal(right));
    }

    public override Value VisitGreaterThanExpression([NotNull] RplParser.GreaterThanExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]).As<Number>();
        var right = this.Visit(context.expression()[1]).As<Number>();

        return left.GreaterThan(right);
    }

    public override Value VisitGreaterThanOrEqualExpression([NotNull] RplParser.GreaterThanOrEqualExpressionContext context)
    {
        var left = this.Visit(context.expression()[0]).As<Number>();
        var right = this.Visit(context.expression()[1]).As<Number>();

        return left.GreaterThan(right).Or(left.Equal(right));
    }

    public override Value VisitParenthesisExpression([NotNull] RplParser.ParenthesisExpressionContext context)
    {
        return this.Visit(context.expression());
    }

    public override Value VisitSequenceExpression([NotNull] RplParser.SequenceExpressionContext context)
    {
        var elements = new List<Value>();

        foreach (var elementExpression in context.expression() ?? [])
            elements.Add(this.Visit(elementExpression));

        return new Sequence([.. elements]);
    }

    public override Value VisitHashExpression([NotNull] RplParser.HashExpressionContext context)
    {
        var keyValues = new Dictionary<string, Value>();
        
        foreach (var keyValue in context.keyValueExpression())
        {
            var key = this.Visit(keyValue.keyExpression())?.As<String>().Value ?? string.Empty;
            var value = this.Visit(keyValue.valueExpression());

            keyValues.Add(key, value);
        }

        return new Hash(keyValues);
    }

    public override Value VisitDotAccessExpression([NotNull] RplParser.DotAccessExpressionContext context)
    {
        var hash = this.Visit(context.expression()).As<Hash>();
        var key = context.EXPR_SYMBOL().GetText();

        return hash.Values[key];
    }
}
