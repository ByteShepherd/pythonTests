using System.Collections.Generic;

namespace Intepreter.Lexical
{
    public enum TokenType
    {
        ILLEGAL,
        EOF,

        // Identifiers + literals
        IDENT, // add, foobar, x, y, ...
        INT,   // 1343456

        // Operators
        ASSIGN,
        PLUS,
        MINUS,
        BANG,
        ASTERISK,
        SLASH,

        LT,
        GT,

        EQ,
        NOT_EQ,

        // Delimiters
        COMMA,
        SEMICOLON,

        LPAREN,
        RPAREN,
        LBRACE,
        RBRACE,

        // Keywords
        FUNCTION,
        LET,
        TRUE,
        FALSE,
        IF,
        ELSE,
        RETURN
    }

    public class Token
    {
        public TokenType Type { get; }
        public string Literal { get; }

        public Token(TokenType type, string literal)
        {
            Type = type;
            Literal = literal;
        }
    }

    public static class TokenHelpers
    {
        private static readonly Dictionary<string, TokenType> Keywords = new()
        {
            { "fn", TokenType.FUNCTION },
            { "let", TokenType.LET },
            { "true", TokenType.TRUE },
            { "false", TokenType.FALSE },
            { "if", TokenType.IF },
            { "else", TokenType.ELSE },
            { "return", TokenType.RETURN }
        };

        public static TokenType LookupIdent(string ident)
        {
            return Keywords.TryGetValue(ident, out var tok) ? tok : TokenType.IDENT;
        }
    }
}
