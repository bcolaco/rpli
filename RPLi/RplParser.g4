parser grammar RplParser;

options { tokenVocab=RplLexer; }

template: elements EOF;

elements: element*;

element
    : CONTENT+                                    # ContentElement
    | directive                                   # DirectiveElement
    | INTERPOLATION_START expression EXPR_R_BRACE # ExpressionElement
    ;

directive
    : directiveAssign
    | directiveBreak
    | directiveIf
    | directiveList;

directiveAssign: START_DIRECTIVE_TAG EXPR_ASSIGN EXPR_SYMBOL EXPR_EQ expression (EXPR_EXIT_GT | EXPR_EXIT_DIV_GT);

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

directiveList
    : START_DIRECTIVE_TAG EXPR_LIST expression EXPR_AS EXPR_SYMBOL EXPR_EXIT_GT
      directiveListElements
      END_DIRECTIVE_TAG EXPR_LIST EXPR_EXIT_GT
    ;

directiveListElements: elements;

directiveBreak: START_DIRECTIVE_TAG EXPR_BREAK EXPR_EXIT_GT;

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
    | expression EXPR_DOT EXPR_SYMBOL         # DotAccessExpression
    | EXPR_L_PAREN expression EXPR_R_PAREN    # ParenthesisExpression
    | EXPR_BANG expression                    # NotExpression
    | expression EXPR_DIVIDE expression       # DivideExpression
    | expression EXPR_MULTIPLY expression     # MultiplyExpression
    | expression EXPR_ADD expression          # AddExpression
    | expression EXPR_SUBTRACT expression     # SubtractExpression
    | expression EXPR_MODULUS expression      # ModulusExpression
    | expression EXPR_LOGICAL_AND expression  # LogicalAndExpression
    | expression EXPR_LOGICAL_OR expression   # LogicalOrExpression
    | expression EXPR_COMPARE_EQ expression   # EqualityExpression
    | expression EXPR_COMPARE_NEQ expression  # InequalityExpression
    | expression op=(EXPR_LT_SYM | EXPR_LT_STR) expression       # LessThanExpression
    | expression op=(EXPR_LTE_SYM | EXPR_LTE_STR) expression     # LessThanOrEqualExpression
    | expression EXPR_GT_STR expression                          # GreaterThanExpression
    | expression po=(EXPR_GTE_SYM | EXPR_GTE_STR) expression     # GreaterThanOrEqualExpression
    | EXPR_L_SQ_PAREN (expression (EXPR_COMMA expression)*)? EXPR_R_SQ_PAREN # SequenceExpression
    | EXPR_L_BRACE (keyValueExpression (EXPR_COMMA keyValueExpression)*)? EXPR_R_BRACE # HashExpression
    ;

keyValueExpression: keyExpression EXPR_COLON valueExpression;
keyExpression: expression;
valueExpression: expression;

single_quote_string : EXPR_SINGLE_STR_START SQS_CONTENT SQS_EXIT;

double_quote_string : EXPR_DOUBLE_STR_START DQS_CONTENT DQS_EXIT;
