parser grammar RplParser;

options { tokenVocab=RplLexer; }

template: element* EOF;

element
    : CONTENT+
    | INTERPOLATION_START inlineExpr EXPR_EXIT_R_BRACE;

string
    : single_quote_string # SingleQuote
    | double_quote_string # DoubleQuote
    ;

inlineExpr
    : EXPR_SYMBOL # SymbolExpression
    | string      # StringExpression
    ;

single_quote_string : EXPR_SINGLE_STR_START SQS_CONTENT SQS_EXIT;

double_quote_string : EXPR_DOUBLE_STR_START DQS_CONTENT DQS_EXIT;
