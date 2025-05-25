

using System.Text.RegularExpressions;

namespace ProyectoCompiladores.lexer
{
    public class Lexer
    {
        private static readonly List<(TokenType, string)> TokenPatterns = new()
        {
            (TokenType.BEGIN_PROGRAM, @"BeginProgram"),
            (TokenType.WHITESPACE, @"\s+"),
            (TokenType.INT_KEYWORD, @"int"),
            (TokenType.FLOAT_KEYWORD, @"float"),
            (TokenType.STRING_KEYWORD, @"string"),
            (TokenType.BOOL_KEYWORD, @"bool"),
            (TokenType.IF, @"if"),
            (TokenType.ELSE, @"else"),
            (TokenType.WHILE, @"while"),
            (TokenType.READ, @"Read"),
            (TokenType.WRITE, @"Write"),
            (TokenType.RETURN, @"return"),
            (TokenType.FN, @"fn"),
            (TokenType.ARROW, @"->"),
            (TokenType.PLUS, @"\+"),
            (TokenType.MINUS, @"\-"),
            (TokenType.MULTIPLY, @"\*"),
            (TokenType.DIVIDE, @"/"),
            (TokenType.GREATER, @">"),
            (TokenType.LESS, @"<"),
            (TokenType.GREATER_EQUAL, @">="),
            (TokenType.LESS_EQUAL, @"<="),
            (TokenType.EQUAL, @"=="),
            (TokenType.NOT_EQUAL, @"!="),
            (TokenType.ASSIGN, @"="),
            (TokenType.LPAREN, @"\("),
            (TokenType.RPAREN, @"\)"),
            (TokenType.LBRACE, @"\{"),
            (TokenType.RBRACE, @"\}"),
            (TokenType.SEMICOLON, @";"),
            (TokenType.COMMA, @","),
            (TokenType.NOT, @"!"),
            (TokenType.STRING_LITERAL, @"""([^""\\]*(\\.[^""\\]*)*)"""),
            (TokenType.INT_LITERAL, @"[0-9]+"),
            (TokenType.FLOAT_LITERAL, @"f[0-9]+\.[0-9]+"),
            (TokenType.BOOL_LITERAL, @"true$|^false"),
            (TokenType.ID, @"[a-zA-Z][a-zA-Z1-9_]*")
        };

        private readonly string _input;
        private int _position;

        public Lexer(string input)
        {
            _input = input;
            _position = 0;
        }

        public Token NextToken()
        {
            if (_position >= _input.Length)
            {
                return new Token(TokenType.EOF, "");
            }
            foreach (var (type, pattern) in TokenPatterns)
            {
                var match = Regex.Match(_input[_position..], "^" + pattern);
                if (match.Success)
                {
                    var value = match.Value;
                    _position += value.Length;
                    return new Token(type, value);
                }
            }
            throw new Exception($"Invalid token at position {_position}");
        }

        public IEnumerable<Token> Tokenize()
        {
            while (_position < _input.Length)
            {
                Token token = NextToken();
                if (token.Type != TokenType.WHITESPACE) // Ignore whitespace
                    yield return token;
            }
        }
    }
}
