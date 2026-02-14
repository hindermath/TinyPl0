namespace Pl0.Core;

/// <summary>
/// Recursive-descent parser that produces P-Code and diagnostics.
/// </summary>
public sealed class Pl0Parser
{
    /// <summary>
    /// OPR opcode for unary negation.
    /// </summary>
    private const int OprNegate = 1;
    /// <summary>
    /// OPR opcode for addition.
    /// </summary>
    private const int OprAdd = 2;
    /// <summary>
    /// OPR opcode for subtraction.
    /// </summary>
    private const int OprSub = 3;
    /// <summary>
    /// OPR opcode for multiplication.
    /// </summary>
    private const int OprMul = 4;
    /// <summary>
    /// OPR opcode for division.
    /// </summary>
    private const int OprDiv = 5;
    /// <summary>
    /// OPR opcode for odd test.
    /// </summary>
    private const int OprOdd = 6;
    /// <summary>
    /// OPR opcode for equality comparison.
    /// </summary>
    private const int OprEq = 8;
    /// <summary>
    /// OPR opcode for inequality comparison.
    /// </summary>
    private const int OprNeq = 9;
    /// <summary>
    /// OPR opcode for less-than comparison.
    /// </summary>
    private const int OprLt = 10;
    /// <summary>
    /// OPR opcode for greater-or-equal comparison.
    /// </summary>
    private const int OprGeq = 11;
    /// <summary>
    /// OPR opcode for greater-than comparison.
    /// </summary>
    private const int OprGt = 12;
    /// <summary>
    /// OPR opcode for less-or-equal comparison.
    /// </summary>
    private const int OprLeq = 13;
    /// <summary>
    /// OPR opcode for input read.
    /// </summary>
    private const int OprRead = 14;
    /// <summary>
    /// OPR opcode for output write.
    /// </summary>
    private const int OprWrite = 15;
    /// <summary>
    /// Diagnostic code for programs that exceed the maximum instruction count.
    /// </summary>
    private const int ErrorProgramTooLong = 35;
    /// <summary>
    /// Diagnostic code for symbol table overflow.
    /// </summary>
    private const int ErrorSymbolTableOverflow = 34;

    /// <summary>
    /// Tokens to parse.
    /// </summary>
    private readonly IReadOnlyList<Pl0Token> _tokens;
    /// <summary>
    /// Compiler options controlling limits and dialect.
    /// </summary>
    private readonly CompilerOptions _options;
    /// <summary>
    /// Symbol table for the current parse.
    /// </summary>
    private readonly SymbolTable _symbols = new();
    /// <summary>
    /// Generated P-Code instructions.
    /// </summary>
    private readonly List<Instruction> _code = [];
    /// <summary>
    /// Compilation diagnostics.
    /// </summary>
    private readonly List<CompilerDiagnostic> _diagnostics = [];
    /// <summary>
    /// Tracks if unexpected EOF has been reported.
    /// </summary>
    private bool _reportedUnexpectedEof;
    /// <summary>
    /// Current token position in the token list.
    /// </summary>
    private int _position;

    /// <summary>
    /// Tokens that can start a statement.
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
    /// Tokens that can continue a block after declarations.
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
    /// Tokens that can follow a statement.
    /// </summary>
    private static readonly TokenKind[] StatementFollowTokens =
    [
        TokenKind.Semicolon,
        TokenKind.End,
        TokenKind.Period,
    ];

    /// <summary>
    /// Tokens that can follow an expression.
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
    /// Creates a parser for the given token stream and options.
    /// </summary>
    /// <param name="tokens">Tokens produced by the lexer.</param>
    /// <param name="options">Compiler options.</param>
    public Pl0Parser(IReadOnlyList<Pl0Token> tokens, CompilerOptions options)
    {
        _tokens = tokens;
        _options = options;
    }

    /// <summary>
    /// Parses tokens into P-Code instructions and diagnostics.
    /// </summary>
    /// <returns>The compilation result.</returns>
    public CompilationResult Parse()
    {
        ParseBlock(level: 0, owner: null);
        if (!TryMatch(TokenKind.Period))
        {
            Report(9, "Period expected.");
            if (Current.Kind == TokenKind.EndOfFile)
            {
                ReportUnexpectedEofOnce();
            }
        }

        Expect(TokenKind.EndOfFile, 99, "Unexpected termination.");
        return new CompilationResult(_code, _diagnostics);
    }

    /// <summary>
    /// Parses a block with declarations and a statement body.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
    /// <param name="owner">Procedure symbol owning this block, if any.</param>
    private void ParseBlock(int level, SymbolEntry? owner)
    {
        if (level > _options.MaxLevel)
        {
            Report(32, "Maximum block nesting level exceeded.");
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

            ExpectOrSync(TokenKind.Semicolon, 5, "Semicolon or comma missing.", BlockContinuationTokens);
        }

        if (TryMatch(TokenKind.Var))
        {
            ParseVarDeclaration(level, ref dataIndex);
            while (TryMatch(TokenKind.Comma))
            {
                ParseVarDeclaration(level, ref dataIndex);
            }

            ExpectOrSync(TokenKind.Semicolon, 5, "Semicolon or comma missing.", BlockContinuationTokens);
        }

        while (TryMatch(TokenKind.Procedure))
        {
            var name = Expect(TokenKind.Ident, 4, "Procedure must be followed by an identifier.");
            var procedure = new SymbolEntry(name.Lexeme, SymbolKind.Procedure, level, address: 0);
            TryDeclare(procedure, 31, "Duplicate identifier in scope.");

            ExpectOrSync(TokenKind.Semicolon, 5, "Semicolon or comma missing.", BlockContinuationTokens);
            ParseBlock(level + 1, procedure);
            ExpectOrSync(TokenKind.Semicolon, 5, "Semicolon or comma missing.", BlockContinuationTokens);
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
    /// Parses a const declaration within the current block.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
    private void ParseConstDeclaration(int level)
    {
        var name = Expect(TokenKind.Ident, 4, "const must be followed by an identifier.");
        if (TryMatch(TokenKind.Becomes))
        {
            Report(1, "Use '=' instead of ':='.");
        }
        else
        {
            Expect(TokenKind.Equal, 3, "Identifier must be followed by '='.");
        }

        var number = Expect(TokenKind.Number, 2, "'=' must be followed by a number.");
        var value = number.NumberValue ?? 0;
        if (value > _options.MaxAddress)
        {
            Report(30, "This number is too large.", number.Position);
            value = 0;
        }

        var constant = new SymbolEntry(name.Lexeme, SymbolKind.Constant, level, address: 0, value);
        TryDeclare(constant, 31, "Duplicate identifier in scope.");
    }

    /// <summary>
    /// Parses a var declaration and allocates a stack address.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
    /// <param name="dataIndex">Current data allocation index.</param>
    private void ParseVarDeclaration(int level, ref int dataIndex)
    {
        var name = Expect(TokenKind.Ident, 4, "var must be followed by an identifier.");
        var variable = new SymbolEntry(name.Lexeme, SymbolKind.Variable, level, dataIndex++);
        TryDeclare(variable, 31, "Duplicate identifier in scope.");
    }

    /// <summary>
    /// Parses a single statement.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
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
    /// Parses an assignment statement.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
    private void ParseAssignment(int level)
    {
        var name = Expect(TokenKind.Ident, 11, "Undeclared identifier.");
        var symbol = Lookup(name.Lexeme, name.Position);
        if (symbol is not null && symbol.Kind != SymbolKind.Variable)
        {
            Report(12, "Assignment to constant or procedure is not allowed.", name.Position);
            symbol = null;
        }

        Expect(TokenKind.Becomes, 13, "Assignment operator ':=' expected.");
        ParseExpression(level);

        if (symbol is not null)
        {
            Emit(Opcode.Sto, ComputeLevelDifference(level, symbol), symbol.Address);
        }
    }

    /// <summary>
    /// Parses a procedure call statement.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
    private void ParseCall(int level)
    {
        Advance();
        var name = Expect(TokenKind.Ident, 14, "call must be followed by an identifier.");
        var symbol = Lookup(name.Lexeme, name.Position);
        if (symbol is null)
        {
            return;
        }

        if (symbol.Kind != SymbolKind.Procedure)
        {
            Report(15, "Call of a constant or a variable is meaningless.", name.Position);
            return;
        }

        Emit(Opcode.Cal, ComputeLevelDifference(level, symbol), symbol.Address);
    }

    /// <summary>
    /// Parses a begin/end statement sequence.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
    private void ParseBeginEnd(int level)
    {
        Advance();
        ParseStatement(level);
        while (TryMatch(TokenKind.Semicolon))
        {
            ParseStatement(level);
        }

        ExpectOrSync(TokenKind.End, 17, "Semicolon or 'end' expected.", StatementFollowTokens);
    }

    /// <summary>
    /// Parses an if-then statement.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
    private void ParseIf(int level)
    {
        Advance();
        ParseCondition(level);
        ExpectOrSync(TokenKind.Then, 16, "'then' expected.", StatementStartTokens);
        var jumpIfFalse = Emit(Opcode.Jpc, 0, 0);
        ParseStatement(level);
        PatchArgument(jumpIfFalse, _code.Count);
    }

    /// <summary>
    /// Parses a while-do loop.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
    private void ParseWhile(int level)
    {
        Advance();
        var conditionStart = _code.Count;
        ParseCondition(level);
        var jumpIfFalse = Emit(Opcode.Jpc, 0, 0);
        ExpectOrSync(TokenKind.Do, 18, "'do' expected.", StatementStartTokens);
        ParseStatement(level);
        Emit(Opcode.Jmp, 0, conditionStart);
        PatchArgument(jumpIfFalse, _code.Count);
    }

    /// <summary>
    /// Parses an input statement.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
    private void ParseInput(int level)
    {
        var question = Expect(TokenKind.Question, 19, "Incorrect symbol following statement.");
        var name = Expect(TokenKind.Ident, 4, "Identifier expected after '?'.");
        if (!_options.EnableIoStatements)
        {
            Report(19, "Input statement '?' is not available in classic mode.", question.Position);
            return;
        }

        var symbol = Lookup(name.Lexeme, name.Position);
        if (symbol is null)
        {
            return;
        }

        if (symbol.Kind != SymbolKind.Variable)
        {
            Report(12, "Input target must be a variable.", name.Position);
            return;
        }

        Emit(Opcode.Opr, 0, OprRead);
        Emit(Opcode.Sto, ComputeLevelDifference(level, symbol), symbol.Address);
    }

    /// <summary>
    /// Parses an output statement.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
    private void ParseOutput(int level)
    {
        var bang = Expect(TokenKind.Bang, 19, "Incorrect symbol following statement.");
        ParseExpression(level);
        if (!_options.EnableIoStatements)
        {
            Report(19, "Output statement '!' is not available in classic mode.", bang.Position);
            return;
        }

        Emit(Opcode.Opr, 0, OprWrite);
    }

    /// <summary>
    /// Parses a condition expression.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
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
            Report(20, "Relational operator expected.");
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
    /// Parses an arithmetic expression.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
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
    /// Parses a term within an expression.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
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
    /// Parses a factor within a term.
    /// </summary>
    /// <param name="level">Current lexical level.</param>
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
                        Report(21, "Expression must not contain a procedure identifier.", name.Position);
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
                    Report(30, "This number is too large.", number.Position);
                    value = 0;
                }

                Emit(Opcode.Lit, 0, value);
                return;
            }
            case TokenKind.LParen:
                Advance();
                ParseExpression(level);
                ExpectOrSync(TokenKind.RParen, 22, "Right parenthesis missing.", ExpressionFollowTokens);
                return;
            default:
                Report(24, "An expression cannot begin with this symbol.");
                Advance();
                return;
        }
    }

    /// <summary>
    /// Looks up a symbol and reports an error if it is missing.
    /// </summary>
    /// <param name="name">Symbol name.</param>
    /// <param name="position">Position for diagnostics.</param>
    /// <returns>The symbol entry or null.</returns>
    private SymbolEntry? Lookup(string name, TextPosition position)
    {
        var symbol = _symbols.Lookup(name);
        if (symbol is null)
        {
            Report(11, "Undeclared identifier.", position);
        }

        return symbol;
    }

    /// <summary>
    /// Computes the lexical level difference for a symbol reference.
    /// </summary>
    /// <param name="currentLevel">Current lexical level.</param>
    /// <param name="symbol">Symbol being referenced.</param>
    /// <returns>The non-negative level difference.</returns>
    private int ComputeLevelDifference(int currentLevel, SymbolEntry symbol)
    {
        var diff = currentLevel - symbol.Level;
        if (diff >= 0)
        {
            return diff;
        }

        Report(99, "Invalid lexical level reference.");
        return 0;
    }

    /// <summary>
    /// Declares a symbol or reports duplicate/overflow errors.
    /// </summary>
    /// <param name="entry">Symbol to declare.</param>
    /// <param name="code">Diagnostic code on failure.</param>
    /// <param name="message">Diagnostic message on failure.</param>
    private void TryDeclare(SymbolEntry entry, int code, string message)
    {
        if (_symbols.Count >= _options.MaxSymbolCount)
        {
            Report(ErrorSymbolTableOverflow, $"Symbol table overflow (max {_options.MaxSymbolCount}).");
            return;
        }

        if (_symbols.TryDeclare(entry))
        {
            return;
        }

        Report(code, message);
    }

    /// <summary>
    /// Emits a P-Code instruction and returns its index.
    /// </summary>
    /// <param name="opcode">Opcode to emit.</param>
    /// <param name="level">Lexical level argument.</param>
    /// <param name="argument">Instruction argument.</param>
    /// <returns>Instruction index.</returns>
    private int Emit(Opcode opcode, int level, int argument)
    {
        if (_code.Count >= _options.MaxCodeLength)
        {
            Report(
                ErrorProgramTooLong,
                $"Program too long (max {_options.MaxCodeLength} instructions).");
            return _code.Count == 0 ? 0 : _code.Count - 1;
        }

        var index = _code.Count;
        _code.Add(new Instruction(opcode, level, argument));
        return index;
    }

    /// <summary>
    /// Patches the argument of a previously emitted instruction.
    /// </summary>
    /// <param name="index">Instruction index.</param>
    /// <param name="argument">New argument value.</param>
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
    /// Consumes the current token if it matches the expected kind.
    /// </summary>
    /// <param name="kind">Expected token kind.</param>
    /// <returns>True when matched and consumed.</returns>
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
    /// Expects a token of a given kind or reports a diagnostic.
    /// </summary>
    /// <param name="kind">Expected token kind.</param>
    /// <param name="code">Diagnostic code on mismatch.</param>
    /// <param name="message">Diagnostic message on mismatch.</param>
    /// <returns>The consumed token.</returns>
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
    /// Expects a token or synchronizes to a recovery set.
    /// </summary>
    /// <param name="kind">Expected token kind.</param>
    /// <param name="code">Diagnostic code on mismatch.</param>
    /// <param name="message">Diagnostic message on mismatch.</param>
    /// <param name="syncTokens">Tokens used for synchronization.</param>
    /// <returns>The consumed token when found.</returns>
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
    /// Advances tokens until a synchronization token is found.
    /// </summary>
    /// <param name="syncTokens">Synchronization tokens.</param>
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
    /// Advances to the next token and returns the previous token.
    /// </summary>
    /// <returns>The consumed token.</returns>
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
    /// Gets the current token.
    /// </summary>
    private Pl0Token Current => _position < _tokens.Count ? _tokens[_position] : _tokens[^1];

    /// <summary>
    /// Determines whether a token kind is a relational operator.
    /// </summary>
    /// <param name="kind">Token kind to check.</param>
    /// <returns>True if the token is a relation operator.</returns>
    private static bool IsRelation(TokenKind kind) =>
        kind is TokenKind.Equal
            or TokenKind.NotEqual
            or TokenKind.Less
            or TokenKind.LessOrEqual
            or TokenKind.Greater
            or TokenKind.GreaterOrEqual;

    /// <summary>
    /// Reports a diagnostic at the current token position.
    /// </summary>
    /// <param name="code">Diagnostic code.</param>
    /// <param name="message">Diagnostic message.</param>
    private void Report(int code, string message)
    {
        _diagnostics.Add(new CompilerDiagnostic(code, message, Current.Position));
    }

    /// <summary>
    /// Reports a diagnostic at a specified position.
    /// </summary>
    /// <param name="code">Diagnostic code.</param>
    /// <param name="message">Diagnostic message.</param>
    /// <param name="position">Source position.</param>
    private void Report(int code, string message, TextPosition position)
    {
        _diagnostics.Add(new CompilerDiagnostic(code, message, position));
    }

    /// <summary>
    /// Reports unexpected end-of-file once per parse.
    /// </summary>
    private void ReportUnexpectedEofOnce()
    {
        if (_reportedUnexpectedEof)
        {
            return;
        }

        _diagnostics.Add(new CompilerDiagnostic(98, "Program incomplete: unexpected end of input.", Current.Position));
        _reportedUnexpectedEof = true;
    }
}
