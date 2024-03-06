lexer grammar RplLexer;

COMMENT             : COMMENT_FRAG -> channel(1);
INTERPOLATION_START : '${"' -> pushMode(EXPR_MODE);
CONTENT             : ('<' | '$' | ~[$<]+) ;

mode EXPR_MODE;
EXPR_EXIT_R_BRACE : '"}' -> popMode;
EXPR_SYMBOL       : SYMBOL;

fragment COMMENT_FRAG : '<#--' .*? '-->';
fragment SYMBOL       : [_a-zA-Z][_a-zA-Z0-9]*;
