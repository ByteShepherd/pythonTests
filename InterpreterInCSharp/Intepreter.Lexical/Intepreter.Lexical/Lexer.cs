using System;

namespace Intepreter.Lexical
{
    public class Lexer
    {
        private string _input;
        private int _position;     // current position in input (points to current char)
        private int _readPosition; // current reading position in input (after current char)
        private char _ch;          // current char under examination

        public Lexer(string input)
        {
            _input = input;
            ReadChar();
        }

        public Token NextToken()
        {
            Token tok;

            SkipWhitespace();

            switch (_ch)
            {
                case '=':
                    if (PeekChar() == '=')
                    {
                        char ch = _ch;
                        ReadChar();
                        string literal = ch.ToString() + _ch.ToString();
                        tok = new Token(TokenType.EQ, literal);
                    }
                    else
                    {
                        tok = NewToken(TokenType.ASSIGN, _ch);
                    }
                    break;
                case '+':
                    tok = NewToken(TokenType.PLUS, _ch);
                    break;
                case '-':
                    tok = NewToken(TokenType.MINUS, _ch);
                    break;
                case '!':
                    if (PeekChar() == '=')
                    {
                        char ch = _ch;
                        ReadChar();
                        string literal = ch.ToString() + _ch.ToString();
                        tok = new Token(TokenType.NOT_EQ, literal);
                    }
                    else
                    {
                        tok = NewToken(TokenType.BANG, _ch);
                    }
                    break;
                case '/':
                    tok = NewToken(TokenType.SLASH, _ch);
                    break;
                case '*':
                    tok = NewToken(TokenType.ASTERISK, _ch);
                    break;
                case '<':
                    tok = NewToken(TokenType.LT, _ch);
                    break;
                case '>':
                    tok = NewToken(TokenType.GT, _ch);
                    break;
                case ';':
                    tok = NewToken(TokenType.SEMICOLON, _ch);
                    break;
                case ',':
                    tok = NewToken(TokenType.COMMA, _ch);
                    break;
                case '{':
                    tok = NewToken(TokenType.LBRACE, _ch);
                    break;
                case '}':
                    tok = NewToken(TokenType.RBRACE, _ch);
                    break;
                case '(':
                    tok = NewToken(TokenType.LPAREN, _ch);
                    break;
                case ')':
                    tok = NewToken(TokenType.RPAREN, _ch);
                    break;
                case '\0':
                    tok = new Token(TokenType.EOF, "");
                    break;
                default:
                    if (IsLetter(_ch))
                    {
                        string literal = ReadIdentifier();
                        tok = new Token(TokenHelpers.LookupIdent(literal), literal);
                        return tok;
                    }
                    else if (IsDigit(_ch))
                    {
                        tok = new Token(TokenType.INT, ReadNumber());
                        return tok;
                    }
                    else
                    {
                        tok = NewToken(TokenType.ILLEGAL, _ch);
                    }
                    break;
            }

            ReadChar();
            return tok;
        }

        private void SkipWhitespace()
        {
            while (_ch == ' ' || _ch == '\t' || _ch == '\n' || _ch == '\r')
            {
                ReadChar();
            }
        }

        private void ReadChar()
        {
            if (_readPosition >= _input.Length)
            {
                _ch = '\0';
            }
            else
            {
                _ch = _input[_readPosition];
            }
            _position = _readPosition;
            _readPosition++;
        }

        private char PeekChar()
        {
            return _readPosition >= _input.Length ? '\0' : _input[_readPosition];
        }

        private string ReadIdentifier()
        {
            int position = _position;
            while (IsLetter(_ch))
            {
                ReadChar();
            }
            return _input.Substring(position, _position - position);
        }

        private string ReadNumber()
        {
            int position = _position;
            while (IsDigit(_ch))
            {
                ReadChar();
            }
            return _input.Substring(position, _position - position);
        }

        private static bool IsLetter(char ch)
        {
            return 'a' <= ch && ch <= 'z' || 'A' <= ch && ch <= 'Z' || ch == '_';
        }

        private static bool IsDigit(char ch)
        {
            return '0' <= ch && ch <= '9';
        }

        private Token NewToken(TokenType tokenType, char ch)
        {
            return new Token(tokenType, ch.ToString());
        }
    }
}
