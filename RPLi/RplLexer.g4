lexer grammar RplLexer;

COMMENT             : COMMENT_FRAG -> channel(1);
INTERPOLATION_START : '${' -> pushMode(EXPR_MODE);
CONTENT             : ('<' | '$' | ~[$<]+) ;

mode DOUBLE_QUOTE_STRING_MODE;
DQS_EXIT    : '"' -> popMode;
DQS_CONTENT : (~[\\$"])+;

mode SINGLE_QUOTE_STRING_MODE;
SQS_EXIT    : '\'' -> popMode;
SQS_CONTENT : (~[\\$'])+;

mode EXPR_MODE;
EXPR_EXIT_R_BRACE     : '}' -> popMode;
EXPR_DOUBLE_STR_START : '"' -> pushMode(DOUBLE_QUOTE_STRING_MODE);
EXPR_SINGLE_STR_START : '\'' -> pushMode(SINGLE_QUOTE_STRING_MODE);
EXPR_ADD              : '+';
EXPR_SUBTRACT         : '-';
EXPR_MULTIPLY         : '*';
EXPR_DIVIDE           : '/';
EXPR_NUMBER           : NUMBER;
EXPR_SYMBOL           : SYMBOL;

fragment COMMENT_FRAG : '<#--' .*? '-->';
fragment NUMBER       : [0-9]+ ('.' [0-9]+)?;
fragment SYMBOL       : [_a-zA-Z][_a-zA-Z0-9]*;
