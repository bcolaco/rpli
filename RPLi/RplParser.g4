parser grammar RplParser;

options { tokenVocab=RplLexer; }

template: elements EOF;

elements: element*;

element
    : CONTENT+                                         # ContentElement
    | directive                                        # DirectiveElement
    | INTERPOLATION_START expression EXPR_EXIT_R_BRACE # ExpressionElement
    ;

directive
    : directiveAssign
    | directiveIf;

directiveAssign: START_DIRECTIVE_TAG EXPR_ASSIGN EXPR_SYMBOL EXPR_EQ expression EXPR_EXIT_DIV_GT;

directiveIf
    : START_DIRECTIVE_TAG EXPR_IF directiveIfExpression EXPR_EXIT_GT directiveIfTrueElements
      (START_DIRECTIVE_TAG EXPR_ELSEIF directiveIfElseIfExpression EXPR_EXIT_GT directiveIfElseIfTrueElements)*
      (START_DIRECTIVE_TAG EXPR_ELSE EXPR_EXIT_GT directiveIfElseElements)?
      END_DIRECTIVE_TAG EXPR_IF EXPR_EXIT_GT
    ;

directiveIfExpression: expression;
directiveIfTrueElements: elements;
directiveIfElseIfExpression: expression;
directiveIfElseIfTrueElements: elements;
directiveIfElseElements: elements;

string
    : single_quote_string # SingleQuote
    | double_quote_string # DoubleQuote
    ;

number: (EXPR_ADD | EXPR_SUBTRACT)? EXPR_NUMBER;

boolean: EXPR_TRUE | EXPR_FALSE;

expression
    : number      # NumberExpression
    | EXPR_SYMBOL # SymbolExpression
    | string      # StringExpression
    | boolean     # BooleanExpression
    | expression EXPR_ADD expression          # AddExpression
    | expression EXPR_SUBTRACT expression     # SubtractExpression
    | expression EXPR_MULTIPLY expression     # MultiplyExpression
    | expression EXPR_DIVIDE expression       # DivideExpression
    | expression EXPR_MODULUS expression      # ModulusExpression
    | expression EXPR_LOGICAL_AND expression  # LogicalAndExpression
    | expression EXPR_LOGICAL_OR expression   # LogicalOrExpression
    ;

single_quote_string : EXPR_SINGLE_STR_START SQS_CONTENT SQS_EXIT;

double_quote_string : EXPR_DOUBLE_STR_START DQS_CONTENT DQS_EXIT;
