parser grammar RplParser;

options { tokenVocab=RplLexer; }

template: element* EOF;

element
    : CONTENT+
    | INTERPOLATION_START inlineExpr EXPR_EXIT_R_BRACE;

inlineExpr: EXPR_SYMBOL;
