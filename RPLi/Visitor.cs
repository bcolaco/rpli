using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Lextm.AnsiC;

namespace RPLi;

class Visitor : RplParserBaseVisitor<string>
{
    public override string VisitTemplate([NotNull] RplParser.TemplateContext context)
    {
        return base.VisitTemplate(context);
    }

    protected override string AggregateResult(string aggregate, string nextResult)
    {
        return string.Concat(aggregate, nextResult);
    }

    public override string VisitTerminal(ITerminalNode node)
    {
        return node.Symbol.Type switch
        {
            RplLexer.CONTENT => node.GetText(),
            RplLexer.EXPR_SYMBOL => node.GetText(),
            _ => base.VisitTerminal(node),
        };
    }
}