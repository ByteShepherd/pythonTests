using Intepreter.Lexical;

namespace Interpreter.Lexical.Tests
{
    public class LexerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestNextToken()
        {
            string input = @"let five = 5;
let ten = 10;

let add = fn(x, y) {
  x + y;
};

let result = add(five, ten);
!-/*5;
5 < 10 > 5;

if (5 < 10) {
	return true;
} else {
	return false;
}

10 == 10;
10 != 9;
";

            var tests = new[]
            {
                new { ExpectedType = TokenType.LET, ExpectedLiteral = "let" },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "five" },
                new { ExpectedType = TokenType.ASSIGN, ExpectedLiteral = "=" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "5" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.LET, ExpectedLiteral = "let" },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "ten" },
                new { ExpectedType = TokenType.ASSIGN, ExpectedLiteral = "=" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "10" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.LET, ExpectedLiteral = "let" },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "add" },
                new { ExpectedType = TokenType.ASSIGN, ExpectedLiteral = "=" },
                new { ExpectedType = TokenType.FUNCTION, ExpectedLiteral = "fn" },
                new { ExpectedType = TokenType.LPAREN, ExpectedLiteral = "(" },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "x" },
                new { ExpectedType = TokenType.COMMA, ExpectedLiteral = "," },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "y" },
                new { ExpectedType = TokenType.RPAREN, ExpectedLiteral = ")" },
                new { ExpectedType = TokenType.LBRACE, ExpectedLiteral = "{" },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "x" },
                new { ExpectedType = TokenType.PLUS, ExpectedLiteral = "+" },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "y" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.RBRACE, ExpectedLiteral = "}" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.LET, ExpectedLiteral = "let" },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "result" },
                new { ExpectedType = TokenType.ASSIGN, ExpectedLiteral = "=" },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "add" },
                new { ExpectedType = TokenType.LPAREN, ExpectedLiteral = "(" },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "five" },
                new { ExpectedType = TokenType.COMMA, ExpectedLiteral = "," },
                new { ExpectedType = TokenType.IDENT, ExpectedLiteral = "ten" },
                new { ExpectedType = TokenType.RPAREN, ExpectedLiteral = ")" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.BANG, ExpectedLiteral = "!" },
                new { ExpectedType = TokenType.MINUS, ExpectedLiteral = "-" },
                new { ExpectedType = TokenType.SLASH, ExpectedLiteral = "/" },
                new { ExpectedType = TokenType.ASTERISK, ExpectedLiteral = "*" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "5" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "5" },
                new { ExpectedType = TokenType.LT, ExpectedLiteral = "<" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "10" },
                new { ExpectedType = TokenType.GT, ExpectedLiteral = ">" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "5" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.IF, ExpectedLiteral = "if" },
                new { ExpectedType = TokenType.LPAREN, ExpectedLiteral = "(" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "5" },
                new { ExpectedType = TokenType.LT, ExpectedLiteral = "<" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "10" },
                new { ExpectedType = TokenType.RPAREN, ExpectedLiteral = ")" },
                new { ExpectedType = TokenType.LBRACE, ExpectedLiteral = "{" },
                new { ExpectedType = TokenType.RETURN, ExpectedLiteral = "return" },
                new { ExpectedType = TokenType.TRUE, ExpectedLiteral = "true" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.RBRACE, ExpectedLiteral = "}" },
                new { ExpectedType = TokenType.ELSE, ExpectedLiteral = "else" },
                new { ExpectedType = TokenType.LBRACE, ExpectedLiteral = "{" },
                new { ExpectedType = TokenType.RETURN, ExpectedLiteral = "return" },
                new { ExpectedType = TokenType.FALSE, ExpectedLiteral = "false" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.RBRACE, ExpectedLiteral = "}" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "10" },
                new { ExpectedType = TokenType.EQ, ExpectedLiteral = "==" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "10" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "10" },
                new { ExpectedType = TokenType.NOT_EQ, ExpectedLiteral = "!=" },
                new { ExpectedType = TokenType.INT, ExpectedLiteral = "9" },
                new { ExpectedType = TokenType.SEMICOLON, ExpectedLiteral = ";" },
                new { ExpectedType = TokenType.EOF, ExpectedLiteral = "" }
            };

            var lexer = new Lexer(input);

            for (int i = 0; i < tests.Length; i++)
            {
                var tt = tests[i];
                var tok = lexer.NextToken();

                Assert.That(tok.Type, Is.EqualTo(tt.ExpectedType), $"tests[{i}] - tokentype wrong. expected={tt.ExpectedType}, got={tok.Type}");
                Assert.That(tok.Literal, Is.EqualTo(tt.ExpectedLiteral), $"tests[{i}] - literal wrong. expected={tt.ExpectedLiteral}, got={tok.Literal}");
            }
        }
    }
}