using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores.lexer
{
    public enum TokenType
    {
        // White space
        WHITESPACE,

        // Reserved words
        INT_KEYWORD,
        FLOAT_KEYWORD,
        STRING_KEYWORD,
        BOOL_KEYWORD,
        BEGIN_PROGRAM,
        IF,
        ELSE,
        WHILE,
        READ,
        WRITE,
        RETURN,
        FN,

        // Special Symbols
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        GREATER,
        LESS,
        GREATER_EQUAL,
        LESS_EQUAL,
        EQUAL,
        NOT_EQUAL,
        ASSIGN,
        LPAREN,
        RPAREN,
        LBRACE,
        RBRACE,
        SEMICOLON,
        COMMA,
        ARROW,
        NOT,

        // Identifiers and Literals
        ID,
        STRING_LITERAL,
        INT_LITERAL,
        FLOAT_LITERAL,
        BOOL_LITERAL,

        // End of file
        EOF,

        // Error
        ERROR,
    }
}
