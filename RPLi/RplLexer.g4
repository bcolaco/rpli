lexer grammar RplLexer;

COMMENT             : COMMENT_FRAG -> channel(1);
END_DIRECTIVE_TAG   : '</#' -> pushMode(EXPR_MODE);
START_DIRECTIVE_TAG : '<#' -> pushMode(EXPR_MODE);
INTERPOLATION_START : '${' -> pushMode(EXPR_MODE);
CONTENT             : ('<' | '$' | ~[$<]+) ;

mode DOUBLE_QUOTE_STRING_MODE;
DQS_EXIT    : '"' -> popMode;
DQS_CONTENT : (~[\\$"])+;

mode SINGLE_QUOTE_STRING_MODE;
SQS_EXIT    : '\'' -> popMode;
SQS_CONTENT : (~[\\$'])+;

mode EXPR_MODE;
EXPR_ASSIGN           : 'assign';
EXPR_ELSE             : 'else';
EXPR_ELSEIF           : 'elseif';
EXPR_FALSE            : 'false';
EXPR_IF               : 'if';
EXPR_TRUE             : 'true';
EXPR_EXIT_R_BRACE     : '}' -> popMode;
EXPR_DOUBLE_STR_START : '"' -> pushMode(DOUBLE_QUOTE_STRING_MODE);
EXPR_SINGLE_STR_START : '\'' -> pushMode(SINGLE_QUOTE_STRING_MODE);
EXPR_EXIT_GT          : '>' -> popMode;
EXPR_EXIT_DIV_GT      : '/>' -> popMode;
EXPR_ADD              : '+';
EXPR_SUBTRACT         : '-';
EXPR_MULTIPLY         : '*';
EXPR_DIVIDE           : '/';
EXPR_MODULUS          : '%';
EXPR_L_PAREN          : '(';
EXPR_R_PAREN          : ')';
EXPR_EQ               : '=';
EXPR_COMPARE_EQ       : '==';
EXPR_COMPARE_NEQ      : '!=';
EXPR_BANG             : '!';
EXPR_LOGICAL_AND      : '&&';
EXPR_LOGICAL_OR       : '||';
EXPR_LT_SYM           : '<';
EXPR_LT_STR           : 'lt';
EXPR_LTE_SYM          : '<=';
EXPR_LTE_STR          : 'lte';
EXPR_GT_STR           : 'gt';
EXPR_GTE_SYM          : '>=';
EXPR_GTE_STR          : 'gte';
EXPR_WS               : [ \n]+ -> channel(2);
EXPR_NUMBER           : NUMBER;
EXPR_SYMBOL           : SYMBOL;

fragment COMMENT_FRAG : '<#--' .*? '-->';
fragment NUMBER       : [0-9]+ ('.' [0-9]+)?;
fragment SYMBOL       : [_a-zA-Z][_a-zA-Z0-9]*;
