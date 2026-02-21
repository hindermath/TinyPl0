using System.Text;
using Pl0.Core;

namespace Pl0.Ide;

internal static class Pl0SourceFormatter
{
    private const int IndentSize = 2;

    public static bool TryFormat(string source, out string formattedSource, CompilerOptions? options = null)
    {
        var text = source ?? string.Empty;
        var lexerResult = new Pl0Lexer(text, options ?? CompilerOptions.Default).Lex();
        if (lexerResult.Diagnostics.Count > 0)
        {
            formattedSource = text;
            return false;
        }

        var tokens = lexerResult.Tokens
            .Where(t => t.Kind is not TokenKind.EndOfFile and not TokenKind.Nul)
            .ToList();

        formattedSource = FormatTokens(tokens);
        return true;
    }

    private static string FormatTokens(IReadOnlyList<Pl0Token> tokens)
    {
        if (tokens.Count == 0)
        {
            return string.Empty;
        }

        var builder = new StringBuilder();
        var indentationLevel = 0;
        var beginDepth = 0;
        var lineStart = true;
        var previousKind = TokenKind.Nul;
        var pendingProcedureHeader = false;
        var procedureFrames = new Stack<ProcedureFrame>();

        foreach (var token in tokens)
        {
            if ((token.Kind == TokenKind.End || token.Kind == TokenKind.Procedure) && !lineStart)
            {
                AppendNewLine(builder, ref lineStart);
            }

            if (lineStart)
            {
                var effectiveIndent = indentationLevel;
                if (token.Kind == TokenKind.End && effectiveIndent > 0)
                {
                    effectiveIndent--;
                }

                AppendIndent(builder, effectiveIndent);
                lineStart = false;
            }

            switch (token.Kind)
            {
                case TokenKind.Semicolon:
                    TrimTrailingSpaces(builder);
                    builder.Append(';');

                    if (pendingProcedureHeader)
                    {
                        pendingProcedureHeader = false;
                        procedureFrames.Push(new ProcedureFrame(beginDepth));
                        indentationLevel++;
                    }
                    else if (procedureFrames.Count > 0 &&
                             procedureFrames.Peek().InStatementPart &&
                             beginDepth == procedureFrames.Peek().BeginDepthAtOpen)
                    {
                        indentationLevel = Math.Max(0, indentationLevel - 1);
                        _ = procedureFrames.Pop();
                    }

                    AppendNewLine(builder, ref lineStart);
                    break;

                case TokenKind.Period:
                    TrimTrailingSpaces(builder);
                    builder.Append('.');
                    AppendNewLine(builder, ref lineStart);
                    break;

                case TokenKind.Comma:
                    TrimTrailingSpaces(builder);
                    builder.Append(", ");
                    break;

                case TokenKind.LParen:
                    builder.Append('(');
                    break;

                case TokenKind.RParen:
                    TrimTrailingSpaces(builder);
                    builder.Append(')');
                    break;

                case TokenKind.Plus:
                case TokenKind.Minus:
                case TokenKind.Times:
                case TokenKind.Slash:
                case TokenKind.Equal:
                case TokenKind.NotEqual:
                case TokenKind.Less:
                case TokenKind.LessOrEqual:
                case TokenKind.Greater:
                case TokenKind.GreaterOrEqual:
                case TokenKind.Becomes:
                    EnsureSpaceBeforeToken(builder, lineStart);
                    builder.Append(token.Lexeme);
                    builder.Append(' ');
                    break;

                case TokenKind.Bang:
                case TokenKind.Question:
                    EnsureSpaceBeforeToken(builder, lineStart);
                    builder.Append(token.Lexeme);
                    builder.Append(' ');
                    MarkProcedureStatementStart(token.Kind, previousKind, procedureFrames);
                    break;

                default:
                    EnsureSpaceBeforeToken(builder, lineStart);
                    builder.Append(token.Lexeme);

                    MarkProcedureStatementStart(token.Kind, previousKind, procedureFrames);

                    if (token.Kind == TokenKind.Procedure)
                    {
                        pendingProcedureHeader = true;
                    }
                    else if (token.Kind == TokenKind.Begin)
                    {
                        beginDepth++;
                        indentationLevel++;
                        AppendNewLine(builder, ref lineStart);
                    }
                    else if (token.Kind == TokenKind.End)
                    {
                        beginDepth = Math.Max(0, beginDepth - 1);
                        indentationLevel = Math.Max(0, indentationLevel - 1);
                    }

                    break;
            }

            previousKind = token.Kind;
        }

        return builder.ToString().TrimEnd();
    }

    private static void MarkProcedureStatementStart(
        TokenKind tokenKind,
        TokenKind previousKind,
        Stack<ProcedureFrame> procedureFrames)
    {
        if (procedureFrames.Count == 0)
        {
            return;
        }

        var frame = procedureFrames.Peek();
        if (frame.InStatementPart)
        {
            return;
        }

        if (!IsStatementStartToken(tokenKind, previousKind))
        {
            return;
        }

        frame.InStatementPart = true;
    }

    private static bool IsStatementStartToken(TokenKind tokenKind, TokenKind previousKind)
    {
        return tokenKind switch
        {
            TokenKind.Begin => true,
            TokenKind.Call => true,
            TokenKind.If => true,
            TokenKind.While => true,
            TokenKind.Question => true,
            TokenKind.Bang => true,
            TokenKind.End => true,
            TokenKind.Ident => previousKind is not TokenKind.Const
                and not TokenKind.Var
                and not TokenKind.Procedure
                and not TokenKind.Comma
                and not TokenKind.Equal,
            _ => false
        };
    }

    private static void EnsureSpaceBeforeToken(StringBuilder builder, bool lineStart)
    {
        if (lineStart || builder.Length == 0)
        {
            return;
        }

        var last = builder[^1];
        if (last is ' ' or '\n' or '(')
        {
            return;
        }

        builder.Append(' ');
    }

    private static void AppendIndent(StringBuilder builder, int indentationLevel)
    {
        if (indentationLevel <= 0)
        {
            return;
        }

        builder.Append(' ', indentationLevel * IndentSize);
    }

    private static void AppendNewLine(StringBuilder builder, ref bool lineStart)
    {
        TrimTrailingSpaces(builder);
        if (builder.Length == 0 || builder[^1] == '\n')
        {
            lineStart = true;
            return;
        }

        builder.Append('\n');
        lineStart = true;
    }

    private static void TrimTrailingSpaces(StringBuilder builder)
    {
        while (builder.Length > 0 && builder[^1] == ' ')
        {
            builder.Length--;
        }
    }

    private sealed class ProcedureFrame(int beginDepthAtOpen)
    {
        public int BeginDepthAtOpen { get; } = beginDepthAtOpen;
        public bool InStatementPart { get; set; }
    }
}
