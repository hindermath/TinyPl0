using System.Globalization;
using System.Resources;

namespace Pl0.Core;

/// <summary>
/// Rekursiv-absteigender Parser, der P-Code und Diagnosen erzeugt.
/// </summary>
public sealed class Pl0Parser
{
    /// <summary>
    /// OPR-Opcode für unäre Negation.
    /// </summary>
    private const int OprNegate = 1;
    /// <summary>
    /// OPR-Opcode für Addition.
    /// </summary>
    private const int OprAdd = 2;
    /// <summary>
    /// OPR-Opcode für Subtraktion.
    /// </summary>
    private const int OprSub = 3;
    /// <summary>
    /// OPR-Opcode für Multiplikation.
    /// </summary>
    private const int OprMul = 4;
    /// <summary>
    /// OPR-Opcode für Division.
    /// </summary>
    private const int OprDiv = 5;
    /// <summary>
    /// OPR-Opcode für Ungerade-Test.
    /// </summary>
    private const int OprOdd = 6;
    /// <summary>
    /// OPR-Opcode für Gleichheitsvergleich.
    /// </summary>
    private const int OprEq = 8;
    /// <summary>
    /// OPR-Opcode für Ungleichheitsvergleich.
    /// </summary>
    private const int OprNeq = 9;
    /// <summary>
    /// OPR-Opcode für Kleiner-als-Vergleich.
    /// </summary>
    private const int OprLt = 10;
    /// <summary>
    /// OPR-Opcode für Größer-gleich-Vergleich.
    /// </summary>
    private const int OprGeq = 11;
    /// <summary>
    /// OPR-Opcode für Größer-als-Vergleich.
    /// </summary>
    private const int OprGt = 12;
    /// <summary>
    /// OPR-Opcode für Kleiner-gleich-Vergleich.
    /// </summary>
    private const int OprLeq = 13;
    /// <summary>
    /// OPR-Opcode für Eingabe-Lesen.
    /// </summary>
    private const int OprRead = 14;
    /// <summary>
    /// OPR-Opcode für Ausgabe-Schreiben.
    /// </summary>
    private const int OprWrite = 15;
    /// <summary>
    /// Diagnose-Code für Programme, die die maximale Befehlsanzahl überschreiten.
    /// </summary>
    private const int ErrorProgramTooLong = 35;
    /// <summary>
    /// Diagnose-Code für Symboltabellen-Überlauf.
    /// </summary>
    private const int ErrorSymbolTableOverflow = 34;

    /// <summary>
    /// Zu parsende Token.
    /// </summary>
    private readonly IReadOnlyList<Pl0Token> _tokens;
    /// <summary>
    /// Compiler-Optionen für Grenzwerte und Dialekt.
    /// </summary>
    private readonly CompilerOptions _options;
    /// <summary>
    /// Symboltabelle für den aktuellen Parse-Vorgang.
    /// </summary>
    private readonly SymbolTable _symbols = new();
    /// <summary>
    /// Erzeugte P-Code-Befehle.
    /// </summary>
    private readonly List<Instruction> _code = [];
    /// <summary>
    /// Compiler-Diagnosen.
    /// </summary>
    private readonly List<CompilerDiagnostic> _diagnostics = [];
    /// <summary>
    /// Verfolgt ob unerwartetes EOF bereits gemeldet wurde.
    /// </summary>
    private bool _reportedUnexpectedEof;
    /// <summary>
    /// Aktuelle Token-Position in der Token-Liste.
    /// </summary>
    private int _position;
    /// <summary>
    /// ResourceManager für lokalisierte Fehlertexte.
    /// </summary>
    private readonly ResourceManager _rm;
    /// <summary>
    /// Zielsprache für Fehlertexte.
    /// </summary>
    private readonly CultureInfo _culture;

    /// <summary>
    /// Token, die eine Anweisung beginnen können.
    /// </summary>
    private static readonly TokenKind[] StatementStartTokens =
    [
        TokenKind.Ident,
        TokenKind.Call,
        TokenKind.Begin,
        TokenKind.If,
        TokenKind.While,
        TokenKind.Question,
        TokenKind.Bang,
    ];

    /// <summary>
    /// Token, die einen Block nach Deklarationen fortsetzen können.
    /// </summary>
    private static readonly TokenKind[] BlockContinuationTokens =
    [
        TokenKind.Const,
        TokenKind.Var,
        TokenKind.Procedure,
        TokenKind.Ident,
        TokenKind.Call,
        TokenKind.Begin,
        TokenKind.If,
        TokenKind.While,
        TokenKind.Question,
        TokenKind.Bang,
        TokenKind.End,
        TokenKind.Period,
    ];

    /// <summary>
    /// Token, die einer Anweisung folgen können.
    /// </summary>
    private static readonly TokenKind[] StatementFollowTokens =
    [
        TokenKind.Semicolon,
        TokenKind.End,
        TokenKind.Period,
    ];

    /// <summary>
    /// Token, die einem Ausdruck folgen können.
    /// </summary>
    private static readonly TokenKind[] ExpressionFollowTokens =
    [
        TokenKind.Semicolon,
        TokenKind.End,
        TokenKind.Then,
        TokenKind.Do,
        TokenKind.Period,
        TokenKind.Equal,
        TokenKind.NotEqual,
        TokenKind.Less,
        TokenKind.LessOrEqual,
        TokenKind.Greater,
        TokenKind.GreaterOrEqual,
        TokenKind.RParen,
    ];

    /// <summary>
    /// Erstellt einen Parser für den gegebenen Token-Strom und Optionen.
    /// </summary>
    /// <param name="tokens">Vom Lexer erzeugte Token.</param>
    /// <param name="options">Compiler-Optionen.</param>
    public Pl0Parser(IReadOnlyList<Pl0Token> tokens, CompilerOptions options)
    {
        _tokens = tokens;
        _options = options;
        _rm = options.Messages ?? Pl0CoreMessages.ResourceManager;
        _culture = CultureInfo.GetCultureInfo(options.Language);
    }

    /// <summary>
    /// Parst Token in P-Code-Befehle und Diagnosen.
    /// </summary>
    /// <returns>Das Kompilierungsergebnis.</returns>
    public CompilationResult Parse()
    {
        ParseBlock(level: 0, owner: null);
        if (!TryMatch(TokenKind.Period))
        {
            Report(9, Msg("Parser_E09_PeriodExpected"));
            if (Current.Kind == TokenKind.EndOfFile)
            {
                ReportUnexpectedEofOnce();
            }
        }

        Expect(TokenKind.EndOfFile, 99, Msg("Parser_E99_UnexpectedTermination"));
        return new CompilationResult(_code, _diagnostics);
    }

    /// <summary>
    /// Parst einen Block mit Deklarationen und Anweisungsrumpf.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    /// <param name="owner">Prozedur-Symbol, das diesen Block besitzt, wenn vorhanden.</param>
    private void ParseBlock(int level, SymbolEntry? owner)
    {
        if (level > _options.MaxLevel)
        {
            Report(32, Msg("Parser_E32_NestingTooDeep"));
        }

        _symbols.EnterScope();
        var dataIndex = 3;

        var jumpToBody = Emit(Opcode.Jmp, 0, 0);

        if (TryMatch(TokenKind.Const))
        {
            ParseConstDeclaration(level);
            while (TryMatch(TokenKind.Comma))
            {
                ParseConstDeclaration(level);
            }

            ExpectOrSync(TokenKind.Semicolon, 5, Msg("Parser_E05_SemiOrComma"), BlockContinuationTokens);
        }

        if (TryMatch(TokenKind.Var))
        {
            ParseVarDeclaration(level, ref dataIndex);
            while (TryMatch(TokenKind.Comma))
            {
                ParseVarDeclaration(level, ref dataIndex);
            }

            ExpectOrSync(TokenKind.Semicolon, 5, Msg("Parser_E05_SemiOrComma"), BlockContinuationTokens);
        }

        while (TryMatch(TokenKind.Procedure))
        {
            var name = Expect(TokenKind.Ident, 4, Msg("Parser_E04_IdentAfterProc"));
            var procedure = new SymbolEntry(name.Lexeme, SymbolKind.Procedure, level, address: 0);
            TryDeclare(procedure, 31, "Duplicate identifier in scope.");

            ExpectOrSync(TokenKind.Semicolon, 5, Msg("Parser_E05_SemiOrComma"), BlockContinuationTokens);
            ParseBlock(level + 1, procedure);
            ExpectOrSync(TokenKind.Semicolon, 5, Msg("Parser_E05_SemiOrComma"), BlockContinuationTokens);
        }

        var bodyStart = _code.Count;
        PatchArgument(jumpToBody, bodyStart);
        if (owner is not null)
        {
            owner.Address = bodyStart;
        }

        Emit(Opcode.Int, 0, dataIndex);
        ParseStatement(level);
        Emit(Opcode.Opr, 0, 0);

        _symbols.ExitScope();
    }

    /// <summary>
    /// Parst eine Konstantendeklaration im aktuellen Block.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseConstDeclaration(int level)
    {
        var name = Expect(TokenKind.Ident, 4, Msg("Parser_E04_IdentAfterConst"));
        if (TryMatch(TokenKind.Becomes))
        {
            Report(1, Msg("Parser_E01_UseEqualNotAssign"));
        }
        else
        {
            Expect(TokenKind.Equal, 3, Msg("Parser_E03_EqualAfterIdent"));
        }

        var number = Expect(TokenKind.Number, 2, Msg("Parser_E02_NumberAfterEquals"));
        var value = number.NumberValue ?? 0;
        if (value > _options.MaxAddress)
        {
            Report(30, Msg("Parser_E30_NumberTooLarge"), number.Position);
            value = 0;
        }

        var constant = new SymbolEntry(name.Lexeme, SymbolKind.Constant, level, address: 0, value);
        TryDeclare(constant, 31, "Duplicate identifier in scope.");
    }

    /// <summary>
    /// Parst eine Variablendeklaration und weist eine Stack-Adresse zu.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    /// <param name="dataIndex">Aktueller Datenzuweisungsindex.</param>
    private void ParseVarDeclaration(int level, ref int dataIndex)
    {
        var name = Expect(TokenKind.Ident, 4, Msg("Parser_E04_IdentAfterVar"));
        var variable = new SymbolEntry(name.Lexeme, SymbolKind.Variable, level, dataIndex++);
        TryDeclare(variable, 31, "Duplicate identifier in scope.");
    }

    /// <summary>
    /// Parst eine einzelne Anweisung.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseStatement(int level)
    {
        switch (Current.Kind)
        {
            case TokenKind.Ident:
                ParseAssignment(level);
                return;
            case TokenKind.Call:
                ParseCall(level);
                return;
            case TokenKind.Begin:
                ParseBeginEnd(level);
                return;
            case TokenKind.If:
                ParseIf(level);
                return;
            case TokenKind.While:
                ParseWhile(level);
                return;
            case TokenKind.Question:
                ParseInput(level);
                return;
            case TokenKind.Bang:
                ParseOutput(level);
                return;
            default:
                return;
        }
    }

    /// <summary>
    /// Parst eine Zuweisungsanweisung.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseAssignment(int level)
    {
        var name = Expect(TokenKind.Ident, 11, Msg("Parser_E11_UndeclaredIdent"));
        var symbol = Lookup(name.Lexeme, name.Position);
        if (symbol is not null && symbol.Kind != SymbolKind.Variable)
        {
            Report(12, Msg("Parser_E12_AssignToConst"), name.Position);
            symbol = null;
        }

        Expect(TokenKind.Becomes, 13, Msg("Parser_E13_AssignOpExpected"));
        ParseExpression(level);

        if (symbol is not null)
        {
            Emit(Opcode.Sto, ComputeLevelDifference(level, symbol), symbol.Address);
        }
    }

    /// <summary>
    /// Parst eine Prozeduraufruf-Anweisung.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseCall(int level)
    {
        Advance();
        var name = Expect(TokenKind.Ident, 14, Msg("Parser_E14_CallNeedsIdent"));
        var symbol = Lookup(name.Lexeme, name.Position);
        if (symbol is null)
        {
            return;
        }

        if (symbol.Kind != SymbolKind.Procedure)
        {
            Report(15, Msg("Parser_E15_CallConstOrVar"), name.Position);
            return;
        }

        Emit(Opcode.Cal, ComputeLevelDifference(level, symbol), symbol.Address);
    }

    /// <summary>
    /// Parst eine BEGIN-END-Anweisungsfolge.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseBeginEnd(int level)
    {
        Advance();
        ParseStatement(level);
        while (TryMatch(TokenKind.Semicolon))
        {
            ParseStatement(level);
        }

        ExpectOrSync(TokenKind.End, 17, Msg("Parser_E17_SemiOrEndExpected"), StatementFollowTokens);
    }

    /// <summary>
    /// Parst eine IF-THEN-Anweisung.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseIf(int level)
    {
        Advance();
        ParseCondition(level);
        ExpectOrSync(TokenKind.Then, 16, Msg("Parser_E16_ThenExpected"), StatementStartTokens);
        var jumpIfFalse = Emit(Opcode.Jpc, 0, 0);
        ParseStatement(level);
        PatchArgument(jumpIfFalse, _code.Count);
    }

    /// <summary>
    /// Parst eine WHILE-DO-Schleife.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseWhile(int level)
    {
        Advance();
        var conditionStart = _code.Count;
        ParseCondition(level);
        var jumpIfFalse = Emit(Opcode.Jpc, 0, 0);
        ExpectOrSync(TokenKind.Do, 18, Msg("Parser_E18_DoExpected"), StatementStartTokens);
        ParseStatement(level);
        Emit(Opcode.Jmp, 0, conditionStart);
        PatchArgument(jumpIfFalse, _code.Count);
    }

    /// <summary>
    /// Parst eine Eingabeanweisung.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseInput(int level)
    {
        var question = Expect(TokenKind.Question, 19, Msg("Parser_E19_IncorrectSymbol"));
        var name = Expect(TokenKind.Ident, 4, Msg("Parser_E04_IdentAfterInput"));
        if (!_options.EnableIoStatements)
        {
            Report(19, Msg("Parser_E19_InputNotInClassic"), question.Position);
            return;
        }

        var symbol = Lookup(name.Lexeme, name.Position);
        if (symbol is null)
        {
            return;
        }

        if (symbol.Kind != SymbolKind.Variable)
        {
            Report(12, Msg("Parser_E12_InputTargetMustBeVar"), name.Position);
            return;
        }

        Emit(Opcode.Opr, 0, OprRead);
        Emit(Opcode.Sto, ComputeLevelDifference(level, symbol), symbol.Address);
    }

    /// <summary>
    /// Parst eine Ausgabeanweisung.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseOutput(int level)
    {
        var bang = Expect(TokenKind.Bang, 19, Msg("Parser_E19_IncorrectSymbol"));
        ParseExpression(level);
        if (!_options.EnableIoStatements)
        {
            Report(19, Msg("Parser_E19_OutputNotInClassic"), bang.Position);
            return;
        }

        Emit(Opcode.Opr, 0, OprWrite);
    }

    /// <summary>
    /// Parst einen Bedingungsausdruck.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseCondition(int level)
    {
        if (TryMatch(TokenKind.Odd))
        {
            ParseExpression(level);
            Emit(Opcode.Opr, 0, OprOdd);
            return;
        }

        ParseExpression(level);
        var relation = Current.Kind;
        if (!IsRelation(relation))
        {
            Report(20, Msg("Parser_E20_RelOpExpected"));
            return;
        }

        Advance();
        ParseExpression(level);

        var oprCode = relation switch
        {
            TokenKind.Equal => OprEq,
            TokenKind.NotEqual => OprNeq,
            TokenKind.Less => OprLt,
            TokenKind.GreaterOrEqual => OprGeq,
            TokenKind.Greater => OprGt,
            TokenKind.LessOrEqual => OprLeq,
            _ => OprEq,
        };
        Emit(Opcode.Opr, 0, oprCode);
    }

    /// <summary>
    /// Parst einen arithmetischen Ausdruck.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseExpression(int level)
    {
        var unaryMinus = false;
        if (Current.Kind is TokenKind.Plus or TokenKind.Minus)
        {
            unaryMinus = Current.Kind == TokenKind.Minus;
            Advance();
        }

        ParseTerm(level);
        if (unaryMinus)
        {
            Emit(Opcode.Opr, 0, OprNegate);
        }

        while (Current.Kind is TokenKind.Plus or TokenKind.Minus)
        {
            var op = Current.Kind;
            Advance();
            ParseTerm(level);
            Emit(Opcode.Opr, 0, op == TokenKind.Plus ? OprAdd : OprSub);
        }
    }

    /// <summary>
    /// Parst einen Term innerhalb eines Ausdrucks.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseTerm(int level)
    {
        ParseFactor(level);
        while (Current.Kind is TokenKind.Times or TokenKind.Slash)
        {
            var op = Current.Kind;
            Advance();
            ParseFactor(level);
            Emit(Opcode.Opr, 0, op == TokenKind.Times ? OprMul : OprDiv);
        }
    }

    /// <summary>
    /// Parst einen Faktor innerhalb eines Terms.
    /// </summary>
    /// <param name="level">Aktuelle lexikalische Ebene.</param>
    private void ParseFactor(int level)
    {
        switch (Current.Kind)
        {
            case TokenKind.Ident:
            {
                var name = Advance();
                var symbol = Lookup(name.Lexeme, name.Position);
                if (symbol is null)
                {
                    return;
                }

                switch (symbol.Kind)
                {
                    case SymbolKind.Constant:
                        Emit(Opcode.Lit, 0, symbol.Value);
                        break;
                    case SymbolKind.Variable:
                        Emit(Opcode.Lod, ComputeLevelDifference(level, symbol), symbol.Address);
                        break;
                    case SymbolKind.Procedure:
                        Report(21, Msg("Parser_E21_ProcInExpr"), name.Position);
                        break;
                }

                return;
            }
            case TokenKind.Number:
            {
                var number = Advance();
                var value = number.NumberValue ?? 0;
                if (value > _options.MaxAddress)
                {
                    Report(30, Msg("Parser_E30_NumberTooLarge"), number.Position);
                    value = 0;
                }

                Emit(Opcode.Lit, 0, value);
                return;
            }
            case TokenKind.LParen:
                Advance();
                ParseExpression(level);
                ExpectOrSync(TokenKind.RParen, 22, Msg("Parser_E22_RightParenMissing"), ExpressionFollowTokens);
                return;
            default:
                Report(24, Msg("Parser_E24_BadExprStart"));
                Advance();
                return;
        }
    }

    /// <summary>
    /// Schlägt ein Symbol nach und meldet einen Fehler wenn es fehlt.
    /// </summary>
    /// <param name="name">Symbolname.</param>
    /// <param name="position">Position für Diagnosen.</param>
    /// <returns>Der Symboltabelleneintrag oder null.</returns>
    private SymbolEntry? Lookup(string name, TextPosition position)
    {
        var symbol = _symbols.Lookup(name);
        if (symbol is null)
        {
            Report(11, Msg("Parser_E11_UndeclaredIdent"), position);
        }

        return symbol;
    }

    /// <summary>
    /// Berechnet den lexikalischen Ebenenunterschied für eine Symbolreferenz.
    /// </summary>
    /// <param name="currentLevel">Aktuelle lexikalische Ebene.</param>
    /// <param name="symbol">Referenziertes Symbol.</param>
    /// <returns>Der nicht-negative Ebenenunterschied.</returns>
    private int ComputeLevelDifference(int currentLevel, SymbolEntry symbol)
    {
        var diff = currentLevel - symbol.Level;
        if (diff >= 0)
        {
            return diff;
        }

        Report(99, Msg("Parser_E99_InvalidLexLevel"));
        return 0;
    }

    /// <summary>
    /// Deklariert ein Symbol oder meldet Duplikat-/Überlauf-Fehler.
    /// </summary>
    /// <param name="entry">Zu deklarierendes Symbol.</param>
    /// <param name="code">Diagnose-Code bei Fehler.</param>
    /// <param name="message">Diagnose-Meldung bei Fehler.</param>
    private void TryDeclare(SymbolEntry entry, int code, string message)
    {
        if (_symbols.Count >= _options.MaxSymbolCount)
        {
            Report(ErrorSymbolTableOverflow,
                string.Format(_culture,
                    _rm.GetString("Parser_E34_SymbolTableOverflow", _culture)
                        ?? "Symbol table full (max {0}).",
                    _options.MaxSymbolCount));
            return;
        }

        if (_symbols.TryDeclare(entry))
        {
            return;
        }

        Report(code, message);
    }

    /// <summary>
    /// Gibt einen P-Code-Befehl aus und gibt seinen Index zurück.
    /// </summary>
    /// <param name="opcode">Auszugebender Opcode.</param>
    /// <param name="level">Lexikalisches Ebenen-Argument.</param>
    /// <param name="argument">Befehlsargument.</param>
    /// <returns>Befehlsindex.</returns>
    private int Emit(Opcode opcode, int level, int argument)
    {
        if (_code.Count >= _options.MaxCodeLength)
        {
            Report(
                ErrorProgramTooLong,
                string.Format(_culture,
                    _rm.GetString("Parser_E35_ProgramTooLong", _culture)
                        ?? "Program too long (max {0} instructions).",
                    _options.MaxCodeLength));
            return _code.Count == 0 ? 0 : _code.Count - 1;
        }

        var index = _code.Count;
        _code.Add(new Instruction(opcode, level, argument));
        return index;
    }

    /// <summary>
    /// Patcht das Argument eines zuvor ausgegebenen Befehls.
    /// </summary>
    /// <param name="index">Befehlsindex.</param>
    /// <param name="argument">Neuer Argumentwert.</param>
    private void PatchArgument(int index, int argument)
    {
        if (index < 0 || index >= _code.Count)
        {
            return;
        }

        var instruction = _code[index];
        _code[index] = instruction with { Argument = argument };
    }

    /// <summary>
    /// Verbraucht das aktuelle Token wenn es der erwarteten Art entspricht.
    /// </summary>
    /// <param name="kind">Erwartete Token-Art.</param>
    /// <returns>True wenn gefunden und verbraucht.</returns>
    private bool TryMatch(TokenKind kind)
    {
        if (Current.Kind != kind)
        {
            return false;
        }

        _position++;
        return true;
    }

    /// <summary>
    /// Erwartet ein Token einer bestimmten Art oder meldet eine Diagnose.
    /// </summary>
    /// <param name="kind">Erwartete Token-Art.</param>
    /// <param name="code">Diagnose-Code bei Nichtübereinstimmung.</param>
    /// <param name="message">Diagnose-Meldung bei Nichtübereinstimmung.</param>
    /// <returns>Das verbrauchte Token.</returns>
    private Pl0Token Expect(TokenKind kind, int code, string message)
    {
        if (Current.Kind == kind)
        {
            return Advance();
        }

        Report(code, message);
        if (Current.Kind == TokenKind.EndOfFile)
        {
            ReportUnexpectedEofOnce();
            return Current;
        }

        return Advance();
    }

    /// <summary>
    /// Erwartet ein Token oder synchronisiert zu einer Wiederherstellungsmenge.
    /// </summary>
    /// <param name="kind">Erwartete Token-Art.</param>
    /// <param name="code">Diagnose-Code bei Nichtübereinstimmung.</param>
    /// <param name="message">Diagnose-Meldung bei Nichtübereinstimmung.</param>
    /// <param name="syncTokens">Token zur Synchronisation.</param>
    /// <returns>Das verbrauchte Token wenn gefunden.</returns>
    private Pl0Token ExpectOrSync(TokenKind kind, int code, string message, params TokenKind[] syncTokens)
    {
        if (Current.Kind == kind)
        {
            return Advance();
        }

        Report(code, message);
        if (Current.Kind == TokenKind.EndOfFile)
        {
            ReportUnexpectedEofOnce();
            return Current;
        }

        Synchronize(syncTokens.Append(kind));
        if (Current.Kind == kind)
        {
            return Advance();
        }

        return Current;
    }

    /// <summary>
    /// Rückt Token vor bis ein Synchronisations-Token gefunden wird.
    /// </summary>
    /// <param name="syncTokens">Synchronisations-Token.</param>
    private void Synchronize(IEnumerable<TokenKind> syncTokens)
    {
        var sync = new HashSet<TokenKind>(syncTokens)
        {
            TokenKind.EndOfFile,
        };

        while (!sync.Contains(Current.Kind))
        {
            Advance();
        }
    }

    /// <summary>
    /// Rückt zum nächsten Token vor und gibt das vorherige zurück.
    /// </summary>
    /// <returns>Das verbrauchte Token.</returns>
    private Pl0Token Advance()
    {
        if (_position < _tokens.Count - 1)
        {
            return _tokens[_position++];
        }

        if (_tokens.Count == 0)
        {
            return new Pl0Token(TokenKind.EndOfFile, string.Empty, new TextPosition(1, 1, 0));
        }

        return _tokens[^1];
    }

    /// <summary>
    /// Gibt das aktuelle Token zurück.
    /// </summary>
    private Pl0Token Current => _position < _tokens.Count ? _tokens[_position] : _tokens[^1];

    /// <summary>
    /// Ermittelt ob eine Token-Art ein Vergleichsoperator ist.
    /// </summary>
    /// <param name="kind">Zu prüfende Token-Art.</param>
    /// <returns>True wenn das Token ein Vergleichsoperator ist.</returns>
    private static bool IsRelation(TokenKind kind) =>
        kind is TokenKind.Equal
            or TokenKind.NotEqual
            or TokenKind.Less
            or TokenKind.LessOrEqual
            or TokenKind.Greater
            or TokenKind.GreaterOrEqual;

    /// <summary>
    /// Meldet eine Diagnose an der aktuellen Token-Position.
    /// </summary>
    /// <param name="code">Diagnose-Code.</param>
    /// <param name="message">Diagnose-Meldung.</param>
    private void Report(int code, string message)
    {
        _diagnostics.Add(new CompilerDiagnostic(code, message, Current.Position));
    }

    /// <summary>
    /// Meldet eine Diagnose an einer angegebenen Position.
    /// </summary>
    /// <param name="code">Diagnose-Code.</param>
    /// <param name="message">Diagnose-Meldung.</param>
    /// <param name="position">Quellposition.</param>
    private void Report(int code, string message, TextPosition position)
    {
        _diagnostics.Add(new CompilerDiagnostic(code, message, position));
    }

    /// <summary>
    /// Meldet unerwartetes Dateiende einmalig pro Parse-Vorgang.
    /// </summary>
    private void ReportUnexpectedEofOnce()
    {
        if (_reportedUnexpectedEof)
        {
            return;
        }

        _diagnostics.Add(new CompilerDiagnostic(
            98,
            Msg("Parser_E98_UnexpectedEndOfInput"),
            Current.Position));
        _reportedUnexpectedEof = true;
    }

    /// <summary>
    /// Gibt den lokalisierten Meldungstext für den angegebenen Schlüssel zurück.
    /// </summary>
    /// <param name="key">Ressourcen-Schlüssel.</param>
    /// <returns>Lokalisierter Meldungstext oder der Schlüssel als Fallback.</returns>
    private string Msg(string key) => _rm.GetString(key, _culture) ?? key;
}
