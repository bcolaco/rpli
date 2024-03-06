parser grammar RplParser;

options { tokenVocab=RplLexer; }

template: element* EOF;

element
    : CONTENT+
    | INTERPOLATION_START expression EXPR_EXIT_R_BRACE;

string
    : single_quote_string # SingleQuote
    | double_quote_string # DoubleQuote
    ;

expression
    : EXPR_NUMBER # NumberExpression
    | EXPR_SYMBOL # SymbolExpression
    | string      # StringExpression
    | expression EXPR_ADD expression      # AddExpression
    | expression EXPR_SUBTRACT expression # SubtractExpression
    | expression EXPR_MULTIPLY expression # MultiplyExpression
    ;

single_quote_string : EXPR_SINGLE_STR_START SQS_CONTENT SQS_EXIT;

double_quote_string : EXPR_DOUBLE_STR_START DQS_CONTENT DQS_EXIT;
