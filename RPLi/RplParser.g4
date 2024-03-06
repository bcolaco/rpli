parser grammar RplParser;

options { tokenVocab=RplLexer; }

template: element* EOF;

element
    : CONTENT+                                         # ContentElement
    | directive                                        # DirectiveElement
    | INTERPOLATION_START expression EXPR_EXIT_R_BRACE # ExpressionElement
    ;

directive: directiveAssign;

directiveAssign: START_DIRECTIVE_TAG EXPR_ASSIGN EXPR_SYMBOL EXPR_EQ expression EXPR_EXIT_DIV_GT;

string
    : single_quote_string # SingleQuote
    | double_quote_string # DoubleQuote
    ;

number: (EXPR_ADD | EXPR_SUBTRACT)? EXPR_NUMBER;

expression
    : number      # NumberExpression
    | EXPR_SYMBOL # SymbolExpression
    | string      # StringExpression
    | expression EXPR_ADD expression      # AddExpression
    | expression EXPR_SUBTRACT expression # SubtractExpression
    | expression EXPR_MULTIPLY expression # MultiplyExpression
    | expression EXPR_DIVIDE expression   # DivideExpression
    | expression EXPR_MODULUS expression  # ModulusExpression
    ;

single_quote_string : EXPR_SINGLE_STR_START SQS_CONTENT SQS_EXIT;

double_quote_string : EXPR_DOUBLE_STR_START DQS_CONTENT DQS_EXIT;
