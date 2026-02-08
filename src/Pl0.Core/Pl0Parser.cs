namespace Pl0.Core;

public sealed class Pl0Parser
{
    private const int OprNegate = 1;
    private const int OprAdd = 2;
    private const int OprSub = 3;
    private const int OprMul = 4;
    private const int OprDiv = 5;
    private const int OprOdd = 6;
    private const int OprEq = 8;
    private const int OprNeq = 9;
    private const int OprLt = 10;
    private const int OprGeq = 11;
    private const int OprGt = 12;
    private const int OprLeq = 13;
    private const int OprRead = 14;
    private const int OprWrite = 15;

    private readonly IReadOnlyList<Pl0Token> _tokens;
    private readonly CompilerOptions _options;
    private readonly SymbolTable _symbols = new();
    private readonly List<Instruction> _code = [];
    private readonly List<CompilerDiagnostic> _diagnostics = [];
    private int _position;

    public Pl0Parser(IReadOnlyList<Pl0Token> tokens, CompilerOptions options)
    {
        _tokens = tokens;
        _options = options;
    }

    public CompilationResult Parse()
    {
        ParseBlock(level: 0, owner: null);
        if (!TryMatch(TokenKind.Period))
        {
            Report(9, "Period expected.");
        }

        Expect(TokenKind.EndOfFile, 99, "Unexpected termination.");
        return new CompilationResult(_code, _diagnostics);
    }

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

            Expect(TokenKind.Semicolon, 5, "Semicolon or comma missing.");
        }

        if (TryMatch(TokenKind.Var))
        {
            ParseVarDeclaration(level, ref dataIndex);
            while (TryMatch(TokenKind.Comma))
            {
                ParseVarDeclaration(level, ref dataIndex);
            }

            Expect(TokenKind.Semicolon, 5, "Semicolon or comma missing.");
        }

        while (TryMatch(TokenKind.Procedure))
        {
            var name = Expect(TokenKind.Ident, 4, "Procedure must be followed by an identifier.");
            var procedure = new SymbolEntry(name.Lexeme, SymbolKind.Procedure, level, address: 0);
            TryDeclare(procedure, 31, "Duplicate identifier in scope.");

            Expect(TokenKind.Semicolon, 5, "Semicolon or comma missing.");
            ParseBlock(level + 1, procedure);
            Expect(TokenKind.Semicolon, 5, "Semicolon or comma missing.");
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

    private void ParseVarDeclaration(int level, ref int dataIndex)
    {
        var name = Expect(TokenKind.Ident, 4, "var must be followed by an identifier.");
        var variable = new SymbolEntry(name.Lexeme, SymbolKind.Variable, level, dataIndex++);
        TryDeclare(variable, 31, "Duplicate identifier in scope.");
    }

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

    private void ParseBeginEnd(int level)
    {
        Advance();
        ParseStatement(level);
        while (TryMatch(TokenKind.Semicolon))
        {
            ParseStatement(level);
        }

        Expect(TokenKind.End, 17, "Semicolon or 'end' expected.");
    }

    private void ParseIf(int level)
    {
        Advance();
        ParseCondition(level);
        Expect(TokenKind.Then, 16, "'then' expected.");
        var jumpIfFalse = Emit(Opcode.Jpc, 0, 0);
        ParseStatement(level);
        PatchArgument(jumpIfFalse, _code.Count);
    }

    private void ParseWhile(int level)
    {
        Advance();
        var conditionStart = _code.Count;
        ParseCondition(level);
        var jumpIfFalse = Emit(Opcode.Jpc, 0, 0);
        Expect(TokenKind.Do, 18, "'do' expected.");
        ParseStatement(level);
        Emit(Opcode.Jmp, 0, conditionStart);
        PatchArgument(jumpIfFalse, _code.Count);
    }

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
                Expect(TokenKind.RParen, 22, "Right parenthesis missing.");
                return;
            default:
                Report(24, "An expression cannot begin with this symbol.");
                Advance();
                return;
        }
    }

    private SymbolEntry? Lookup(string name, TextPosition position)
    {
        var symbol = _symbols.Lookup(name);
        if (symbol is null)
        {
            Report(11, "Undeclared identifier.", position);
        }

        return symbol;
    }

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

    private void TryDeclare(SymbolEntry entry, int code, string message)
    {
        if (_symbols.TryDeclare(entry))
        {
            return;
        }

        Report(code, message);
    }

    private int Emit(Opcode opcode, int level, int argument)
    {
        var index = _code.Count;
        _code.Add(new Instruction(opcode, level, argument));
        return index;
    }

    private void PatchArgument(int index, int argument)
    {
        if (index < 0 || index >= _code.Count)
        {
            return;
        }

        var instruction = _code[index];
        _code[index] = instruction with { Argument = argument };
    }

    private bool TryMatch(TokenKind kind)
    {
        if (Current.Kind != kind)
        {
            return false;
        }

        _position++;
        return true;
    }

    private Pl0Token Expect(TokenKind kind, int code, string message)
    {
        if (Current.Kind == kind)
        {
            return Advance();
        }

        Report(code, message);
        return Advance();
    }

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

    private Pl0Token Current => _position < _tokens.Count ? _tokens[_position] : _tokens[^1];

    private static bool IsRelation(TokenKind kind) =>
        kind is TokenKind.Equal
            or TokenKind.NotEqual
            or TokenKind.Less
            or TokenKind.LessOrEqual
            or TokenKind.Greater
            or TokenKind.GreaterOrEqual;

    private void Report(int code, string message)
    {
        _diagnostics.Add(new CompilerDiagnostic(code, message, Current.Position));
    }

    private void Report(int code, string message, TextPosition position)
    {
        _diagnostics.Add(new CompilerDiagnostic(code, message, position));
    }
}
