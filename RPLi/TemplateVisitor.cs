namespace RPLi;

using System.Text;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Lextm.AnsiC;

class TempalteVisitor(IDictionary<string, Value> Namespace) : RplParserBaseVisitor<RenderResult>
{
    private readonly ExpressionVisitor expressionVisitor = new ExpressionVisitor(Namespace);
    
    protected override RenderResult AggregateResult(RenderResult aggregate, RenderResult nextResult)
    {
        return new RenderResult(string.Concat(aggregate?.Text ?? string.Empty, nextResult?.Text ?? string.Empty), (aggregate?.IsBreaking ?? false) || (nextResult?.IsBreaking ?? false));
    }

    public override RenderResult VisitTerminal(ITerminalNode node)
    {
        switch(node.Symbol.Type)
        {
            case RplLexer.CONTENT:
                return new RenderResult(node.GetText());
            default:
                return base.VisitTerminal(node);
        };
    }

    public override RenderResult VisitExpressionElement([NotNull] RplParser.ExpressionElementContext context)
    {
        return new RenderResult(this.expressionVisitor.Visit(context.expression()).ToString() ?? string.Empty);
    }

    public override RenderResult VisitDirectiveAssign([NotNull] RplParser.DirectiveAssignContext context)
    {
        var name = context.EXPR_SYMBOL().GetText();
        var value = this.expressionVisitor.Visit(context.expression());
        Namespace[name] = value;
        return base.VisitDirectiveAssign(context);
    }

    public override RenderResult VisitDirectiveIf([NotNull] RplParser.DirectiveIfContext context)
    {
        var expressionValue = this.expressionVisitor.Visit(context.directiveIfExpression());

        if (expressionValue is not Boolean booleanExpression)
            throw new Exception();

        if (booleanExpression.Value == true)
            return this.Visit(context.directiveIfTrueElements());

        var elseIfExpressions = context.directiveIfElseIfExpression();
        for (var i=0; i< elseIfExpressions.Length; i++)
        {
            var elseifExpressionValue = this.expressionVisitor.Visit(elseIfExpressions[i]);

            if (elseifExpressionValue is not Boolean elseIfBooleanExpression)
                throw new Exception();

            if (elseIfBooleanExpression.Value == true)
                return this.Visit(context.directiveIfElseIfTrueElements()[i]);
            }

        var directiveElseElements = context.directiveIfElseElements();

        if (directiveElseElements is not null)
            return this.Visit(directiveElseElements);

        return RenderResult.Empty;
    }

    public override RenderResult VisitDirectiveList([NotNull] RplParser.DirectiveListContext context)
    {
        var sequence = this.expressionVisitor.Visit(context.expression()).As<Sequence>();
        var item = context.EXPR_SYMBOL().GetText();
        var elements = context.directiveListElements();

        if (elements is null)
            return RenderResult.Empty;
        
        var result = new StringBuilder();
        var itemIdex = 0;
        var elementCount = sequence.Values.Length;
        foreach (var element in sequence.Values)
        {
            Namespace[item] = element;
            Namespace[$"{item}_index"] = new Number(itemIdex++);
            Namespace[$"{item}_has_next"] = new Boolean(itemIdex < elementCount);
            var elementResult = this.Visit(elements);
            result.Append(elementResult.Text);

            if (elementResult.IsBreaking)
                break;
        }

        return new RenderResult(result.ToString());
    }

    public override RenderResult VisitDirectiveBreak([NotNull] RplParser.DirectiveBreakContext context) => new(string.Empty, true);
}
