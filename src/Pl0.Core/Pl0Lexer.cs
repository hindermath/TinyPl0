using System.Globalization;
using System.Resources;

namespace Pl0.Core;

/// <summary>
/// Tokenisiert PL/0-Quellcode in eine Folge von Token mit Diagnosen.
/// </summary>
public sealed class Pl0Lexer
{
    /// <summary>
    /// Compiler-Optionen für Grenzwerte und Dialekte.
    /// </summary>
    private readonly CompilerOptions _options;
    /// <summary>
    /// Zu tokenisierender Quelltext.
    /// </summary>
    private readonly string _text;
    /// <summary>
    /// Gesammelte Token.
    /// </summary>
    private readonly List<Pl0Token> _tokens = [];
    /// <summary>
    /// Gesammelte Lexer-Diagnosen.
    /// </summary>
    private readonly List<LexerDiagnostic> _diagnostics = [];
    /// <summary>
    /// Aktueller absoluter Index im Quelltext.
    /// </summary>
    private int _index;
    /// <summary>
    /// Aktuelle 1-basierte Zeilennummer.
    /// </summary>
    private int _line = 1;
    /// <summary>
    /// Aktuelle 1-basierte Spaltennummer.
    /// </summary>
    private int _column = 1;
    /// <summary>
    /// ResourceManager für lokalisierte Fehlertexte.
    /// </summary>
    private readonly ResourceManager _rm;
    /// <summary>
    /// Zielsprache für Fehlertexte.
    /// </summary>
    private readonly CultureInfo _culture;

    /// <summary>
    /// Erstellt einen Lexer für den angegebenen Quelltext.
    /// </summary>
    /// <param name="text">Der PL/0-Quelltext.</param>
    /// <param name="options">Optionale Compiler-Optionen; Standard wird verwendet wenn null.</param>
    public Pl0Lexer(string text, CompilerOptions? options = null)
    {
        _text = text ?? string.Empty;
        _options = options ?? CompilerOptions.Default;
        _rm = _options.Messages ?? Pl0CoreMessages.ResourceManager;
        _culture = CultureInfo.GetCultureInfo(_options.Language);
    }

    /// <summary>
    /// Führt die lexikalische Analyse durch und gibt Token und Diagnosen zurück.
    /// </summary>
    /// <returns>Das Lexer-Ergebnis mit Token und Diagnosen.</returns>
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
    /// Gibt das aktuelle Zeichen oder den Null-Terminator am Eingabeende zurück.
    /// </summary>
    private char Current => _index >= _text.Length ? '\0' : _text[_index];

    /// <summary>
    /// Liest voraus um einen gegebenen Offset ohne Vorrücken.
    /// </summary>
    /// <param name="offset">Zeichen-Offset vom aktuellen Index.</param>
    /// <returns>Das Zeichen am Offset oder Null-Terminator.</returns>
    private char Peek(int offset)
    {
        var pos = _index + offset;
        return pos >= _text.Length ? '\0' : _text[pos];
    }

    /// <summary>
    /// Gibt die aktuelle Textposition zurück.
    /// </summary>
    private TextPosition CurrentPosition => new(_line, _column, _index);

    /// <summary>
    /// Rückt den Lexer um ein Zeichen vor und aktualisiert Zeilen- und Spaltenzähler.
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
    /// Überspringt Leerzeichen.
    /// </summary>
    private void SkipWhiteSpace()
    {
        while (char.IsWhiteSpace(Current))
        {
            Advance();
        }
    }

    /// <summary>
    /// Fügt ein Token hinzu, das durch ein einzelnes Zeichen repräsentiert wird.
    /// </summary>
    /// <param name="kind">Auszugebende Token-Art.</param>
    private void AddSingleCharToken(TokenKind kind)
    {
        var start = CurrentPosition;
        var lexeme = Current.ToString(CultureInfo.InvariantCulture);
        Advance();
        _tokens.Add(new Pl0Token(kind, lexeme, start));
    }

    /// <summary>
    /// Liest einen Bezeichner oder ein Schlüsselwort-Token.
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
                string.Format(_culture,
                    _rm.GetString("Lexer_E33_IdentifierTooLong", _culture)
                        ?? "Identifier too long: '{0}'. Max: {1}.",
                    lexeme, _options.MaxIdentifierLength),
                start));
        }

        _tokens.Add(new Pl0Token(MapIdentifierKind(lexeme), lexeme, start));
    }

    /// <summary>
    /// Liest ein numerisches Literal-Token.
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
                string.Format(_culture,
                    _rm.GetString("Lexer_E30_NumberTooManyDigits", _culture)
                        ?? "Number has too many digits: '{0}'. Max: {1}.",
                    lexeme, _options.MaxNumberDigits),
                start));
            _tokens.Add(new Pl0Token(TokenKind.Number, lexeme, start));
            return;
        }

        if (!int.TryParse(lexeme, NumberStyles.None, CultureInfo.InvariantCulture, out var value))
        {
            _diagnostics.Add(new LexerDiagnostic(
                30,
                string.Format(_culture,
                    _rm.GetString("Lexer_E30_NumberTooLarge", _culture)
                        ?? "Number is too large: '{0}'.",
                    lexeme),
                start));
            _tokens.Add(new Pl0Token(TokenKind.Number, lexeme, start));
            return;
        }

        _tokens.Add(new Pl0Token(TokenKind.Number, lexeme, start, value));
    }

    /// <summary>
    /// Liest den &lt;- oder &lt;=-Operator.
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
    /// Liest den &gt;- oder &gt;=-Operator.
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
    /// Liest den :=-Operator oder meldet einen Fehler für ein einzelnes ':'.
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

        _diagnostics.Add(new LexerDiagnostic(
            99,
            _rm.GetString("Lexer_E99_UnexpectedColon", _culture)
                ?? "Unexpected ':'; expected ':='.",
            start));
        Advance();
        _tokens.Add(new Pl0Token(TokenKind.Nul, ":", start));
    }

    /// <summary>
    /// Meldet eine Diagnose für ein unerwartetes Zeichen.
    /// </summary>
    /// <param name="ch">Das unerwartete Zeichen.</param>
    private void ReportUnexpectedCharacter(char ch)
    {
        _diagnostics.Add(new LexerDiagnostic(
            99,
            string.Format(_culture,
                _rm.GetString("Lexer_E99_UnexpectedChar", _culture)
                    ?? "Unexpected character '{0}'.",
                ch),
            CurrentPosition));
    }

    /// <summary>
    /// Ordnet ein Bezeichner-Lexem seiner Schlüsselwort-Token-Art zu, wenn zutreffend.
    /// </summary>
    /// <param name="lexeme">Der Bezeichnertext.</param>
    /// <returns>Die Token-Art für das Lexem.</returns>
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
