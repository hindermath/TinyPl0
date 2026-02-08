using System.Globalization;

namespace Pl0.Core;

public sealed class Pl0Lexer
{
    private readonly CompilerOptions _options;
    private readonly string _text;
    private readonly List<Pl0Token> _tokens = [];
    private readonly List<LexerDiagnostic> _diagnostics = [];
    private int _index;
    private int _line = 1;
    private int _column = 1;

    public Pl0Lexer(string text, CompilerOptions? options = null)
    {
        _text = text ?? string.Empty;
        _options = options ?? CompilerOptions.Default;
    }

    public LexerResult Lex()
    {
        while (true)
        {
            SkipWhiteSpace();

            var start = CurrentPosition;
            var ch = Current;
            if (ch == '\0')
            {
                _tokens.Add(new Pl0Token(TokenKind.EndOfFile, string.Empty, start));
                break;
            }

            if (char.IsLetter(ch))
            {
                LexIdentifierOrKeyword();
                continue;
            }

            if (char.IsDigit(ch))
            {
                LexNumber();
                continue;
            }

            switch (ch)
            {
                case '+':
                    AddSingleCharToken(TokenKind.Plus);
                    break;
                case '-':
                    AddSingleCharToken(TokenKind.Minus);
                    break;
                case '*':
                    AddSingleCharToken(TokenKind.Times);
                    break;
                case '/':
                    AddSingleCharToken(TokenKind.Slash);
                    break;
                case '(':
                    AddSingleCharToken(TokenKind.LParen);
                    break;
                case ')':
                    AddSingleCharToken(TokenKind.RParen);
                    break;
                case ',':
                    AddSingleCharToken(TokenKind.Comma);
                    break;
                case ';':
                    AddSingleCharToken(TokenKind.Semicolon);
                    break;
                case '.':
                    AddSingleCharToken(TokenKind.Period);
                    break;
                case '=':
                    AddSingleCharToken(TokenKind.Equal);
                    break;
                case '#':
                    AddSingleCharToken(TokenKind.NotEqual);
                    break;
                case '?':
                    AddSingleCharToken(TokenKind.Question);
                    break;
                case '!':
                    AddSingleCharToken(TokenKind.Bang);
                    break;
                case '<':
                    LexLessOrLessEqual();
                    break;
                case '>':
                    LexGreaterOrGreaterEqual();
                    break;
                case '[':
                    AddSingleCharToken(TokenKind.LessOrEqual);
                    break;
                case ']':
                    AddSingleCharToken(TokenKind.GreaterOrEqual);
                    break;
                case ':':
                    LexBecomesOrDiagnostic();
                    break;
                default:
                    ReportUnexpectedCharacter(ch);
                    Advance();
                    break;
            }
        }

        return new LexerResult(_tokens, _diagnostics);
    }

    private char Current => _index >= _text.Length ? '\0' : _text[_index];

    private char Peek(int offset)
    {
        var pos = _index + offset;
        return pos >= _text.Length ? '\0' : _text[pos];
    }

    private TextPosition CurrentPosition => new(_line, _column, _index);

    private void Advance()
    {
        if (_index >= _text.Length)
        {
            return;
        }

        if (_text[_index] == '\n')
        {
            _line++;
            _column = 1;
            _index++;
            return;
        }

        _index++;
        _column++;
    }

    private void SkipWhiteSpace()
    {
        while (char.IsWhiteSpace(Current))
        {
            Advance();
        }
    }

    private void AddSingleCharToken(TokenKind kind)
    {
        var start = CurrentPosition;
        var lexeme = Current.ToString(CultureInfo.InvariantCulture);
        Advance();
        _tokens.Add(new Pl0Token(kind, lexeme, start));
    }

    private void LexIdentifierOrKeyword()
    {
        var start = CurrentPosition;
        var begin = _index;
        while (char.IsLetterOrDigit(Current))
        {
            Advance();
        }

        var lexeme = _text.Substring(begin, _index - begin);
        if (lexeme.Length > _options.MaxIdentifierLength)
        {
            _diagnostics.Add(new LexerDiagnostic(
                33,
                $"Identifier is too long: '{lexeme}'. Max length is {_options.MaxIdentifierLength}.",
                start));
        }

        _tokens.Add(new Pl0Token(MapIdentifierKind(lexeme), lexeme, start));
    }

    private void LexNumber()
    {
        var start = CurrentPosition;
        var begin = _index;
        while (char.IsDigit(Current))
        {
            Advance();
        }

        var lexeme = _text.Substring(begin, _index - begin);
        if (lexeme.Length > _options.MaxNumberDigits)
        {
            _diagnostics.Add(new LexerDiagnostic(
                30,
                $"Number has too many digits: '{lexeme}'. Max digits is {_options.MaxNumberDigits}.",
                start));
            _tokens.Add(new Pl0Token(TokenKind.Number, lexeme, start));
            return;
        }

        if (!int.TryParse(lexeme, NumberStyles.None, CultureInfo.InvariantCulture, out var value))
        {
            _diagnostics.Add(new LexerDiagnostic(30, $"Number is too large: '{lexeme}'.", start));
            _tokens.Add(new Pl0Token(TokenKind.Number, lexeme, start));
            return;
        }

        _tokens.Add(new Pl0Token(TokenKind.Number, lexeme, start, value));
    }

    private void LexLessOrLessEqual()
    {
        var start = CurrentPosition;
        if (Peek(1) == '=')
        {
            var lexeme = _text.Substring(_index, 2);
            Advance();
            Advance();
            _tokens.Add(new Pl0Token(TokenKind.LessOrEqual, lexeme, start));
            return;
        }

        AddSingleCharToken(TokenKind.Less);
    }

    private void LexGreaterOrGreaterEqual()
    {
        var start = CurrentPosition;
        if (Peek(1) == '=')
        {
            var lexeme = _text.Substring(_index, 2);
            Advance();
            Advance();
            _tokens.Add(new Pl0Token(TokenKind.GreaterOrEqual, lexeme, start));
            return;
        }

        AddSingleCharToken(TokenKind.Greater);
    }

    private void LexBecomesOrDiagnostic()
    {
        var start = CurrentPosition;
        if (Peek(1) == '=')
        {
            var lexeme = _text.Substring(_index, 2);
            Advance();
            Advance();
            _tokens.Add(new Pl0Token(TokenKind.Becomes, lexeme, start));
            return;
        }

        _diagnostics.Add(new LexerDiagnostic(99, "Unexpected ':'; expected ':='.", start));
        Advance();
        _tokens.Add(new Pl0Token(TokenKind.Nul, ":", start));
    }

    private void ReportUnexpectedCharacter(char ch)
    {
        _diagnostics.Add(new LexerDiagnostic(
            99,
            $"Unexpected character '{ch}'.",
            CurrentPosition));
    }

    private static TokenKind MapIdentifierKind(string lexeme) =>
        lexeme switch
        {
            "begin" => TokenKind.Begin,
            "call" => TokenKind.Call,
            "const" => TokenKind.Const,
            "do" => TokenKind.Do,
            "end" => TokenKind.End,
            "if" => TokenKind.If,
            "odd" => TokenKind.Odd,
            "procedure" => TokenKind.Procedure,
            "then" => TokenKind.Then,
            "var" => TokenKind.Var,
            "while" => TokenKind.While,
            _ => TokenKind.Ident,
        };
}
