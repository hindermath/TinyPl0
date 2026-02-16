using System.Globalization;

namespace Pl0.Core;

/// <summary>
/// Tokenizes PL/0 source code into a sequence of tokens with diagnostics.
/// </summary>
public sealed class Pl0Lexer
{
    /// <summary>
    /// Compiler options controlling lexer limits and dialects.
    /// </summary>
    private readonly CompilerOptions _options;
    /// <summary>
    /// Source text being tokenized.
    /// </summary>
    private readonly string _text;
    /// <summary>
    /// Accumulated tokens.
    /// </summary>
    private readonly List<Pl0Token> _tokens = [];
    /// <summary>
    /// Accumulated lexer diagnostics.
    /// </summary>
    private readonly List<LexerDiagnostic> _diagnostics = [];
    /// <summary>
    /// Current absolute index in the source text.
    /// </summary>
    private int _index;
    /// <summary>
    /// Current 1-based line number.
    /// </summary>
    private int _line = 1;
    /// <summary>
    /// Current 1-based column number.
    /// </summary>
    private int _column = 1;

    /// <summary>
    /// Creates a lexer for the provided source text.
    /// </summary>
    /// <param name="text">The PL/0 source text.</param>
    /// <param name="options">Optional compiler options; defaults are used when null.</param>
    public Pl0Lexer(string text, CompilerOptions? options = null)
    {
        _text = text ?? string.Empty;
        _options = options ?? CompilerOptions.Default;
    }

    /// <summary>
    /// Performs lexical analysis and returns tokens and diagnostics.
    /// </summary>
    /// <returns>The lexer result with tokens and diagnostics.</returns>
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

    /// <summary>
    /// Gets the current character or null terminator at end of input.
    /// </summary>
    private char Current => _index >= _text.Length ? '\0' : _text[_index];

    /// <summary>
    /// Peeks ahead by a given offset without advancing.
    /// </summary>
    /// <param name="offset">Character offset from current index.</param>
    /// <returns>The character at the offset or null terminator.</returns>
    private char Peek(int offset)
    {
        var pos = _index + offset;
        return pos >= _text.Length ? '\0' : _text[pos];
    }

    /// <summary>
    /// Gets the current text position.
    /// </summary>
    private TextPosition CurrentPosition => new(_line, _column, _index);

    /// <summary>
    /// Advances the lexer by one character, updating line and column counters.
    /// </summary>
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

    /// <summary>
    /// Skips whitespace characters.
    /// </summary>
    private void SkipWhiteSpace()
    {
        while (char.IsWhiteSpace(Current))
        {
            Advance();
        }
    }

    /// <summary>
    /// Adds a token that is represented by a single character.
    /// </summary>
    /// <param name="kind">Token kind to emit.</param>
    private void AddSingleCharToken(TokenKind kind)
    {
        var start = CurrentPosition;
        var lexeme = Current.ToString(CultureInfo.InvariantCulture);
        Advance();
        _tokens.Add(new Pl0Token(kind, lexeme, start));
    }

    /// <summary>
    /// Lexes an identifier or keyword token.
    /// </summary>
    private void LexIdentifierOrKeyword()
    {
        var start = CurrentPosition;
        var begin = _index;
        while (char.IsLetterOrDigit(Current))
        {
            Advance();
        }

        var lexeme = _text.Substring(begin, _index - begin).ToLowerInvariant();
        if (lexeme.Length > _options.MaxIdentifierLength)
        {
            _diagnostics.Add(new LexerDiagnostic(
                33,
                $"Identifier is too long: '{lexeme}'. Max length is {_options.MaxIdentifierLength}.",
                start));
        }

        _tokens.Add(new Pl0Token(MapIdentifierKind(lexeme), lexeme, start));
    }

    /// <summary>
    /// Lexes a numeric literal token.
    /// </summary>
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

    /// <summary>
    /// Lexes the &lt; or &lt;= operator.
    /// </summary>
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

    /// <summary>
    /// Lexes the &gt; or &gt;= operator.
    /// </summary>
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

    /// <summary>
    /// Lexes the := operator or reports an error for a bare ':'.
    /// </summary>
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

    /// <summary>
    /// Reports a diagnostic for an unexpected character.
    /// </summary>
    /// <param name="ch">The unexpected character.</param>
    private void ReportUnexpectedCharacter(char ch)
    {
        _diagnostics.Add(new LexerDiagnostic(
            99,
            $"Unexpected character '{ch}'.",
            CurrentPosition));
    }

    /// <summary>
    /// Maps an identifier lexeme to its keyword token kind when applicable.
    /// </summary>
    /// <param name="lexeme">The identifier text.</param>
    /// <returns>The token kind for the lexeme.</returns>
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
