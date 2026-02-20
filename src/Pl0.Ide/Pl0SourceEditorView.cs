using Pl0.Core;
using Terminal.Gui;

namespace Pl0.Ide;

internal enum Pl0HighlightKind
{
    None = 0,
    Keyword,
    Number,
    Operator
}

internal sealed class Pl0SourceEditorView : TextView
{
    private readonly CompilerOptions compilerOptions;
    private Dictionary<int, Dictionary<int, Pl0HighlightKind>> highlightMap = [];

    public Pl0SourceEditorView(CompilerOptions? compilerOptions = null)
    {
        this.compilerOptions = compilerOptions ?? CompilerOptions.Default;

        Multiline = true;
        ReadOnly = false;
        Text = string.Empty;

        TextChanged += (_, _) => RebuildHighlightMap();
        ContentsChanged += (_, _) => RebuildHighlightMap();

        RebuildHighlightMap();
    }

    internal Pl0HighlightKind GetHighlightKindAt(int rowIndex, int columnIndex)
    {
        if (!highlightMap.TryGetValue(rowIndex, out var lineHighlights))
        {
            return Pl0HighlightKind.None;
        }

        return lineHighlights.TryGetValue(columnIndex, out var kind)
            ? kind
            : Pl0HighlightKind.None;
    }

    protected override void OnDrawNormalColor(List<Cell> line, int idxCol, int idxRow)
    {
        var highlightKind = GetHighlightKindAt(idxRow, idxCol);
        var scheme = ColorScheme;
        if (highlightKind == Pl0HighlightKind.None || scheme is null)
        {
            base.OnDrawNormalColor(line, idxCol, idxRow);
            return;
        }

        var attribute = highlightKind switch
        {
            Pl0HighlightKind.Keyword => scheme.HotNormal,
            Pl0HighlightKind.Number => scheme.HotFocus,
            Pl0HighlightKind.Operator => scheme.Focus,
            _ => scheme.Normal
        };

        SetAttribute(attribute);
    }

    private void RebuildHighlightMap()
    {
        var text = Text?.ToString() ?? string.Empty;
        var lexer = new Pl0Lexer(text, compilerOptions);
        var lexerResult = lexer.Lex();

        var newMap = new Dictionary<int, Dictionary<int, Pl0HighlightKind>>();

        foreach (var token in lexerResult.Tokens)
        {
            var highlightKind = ClassifyToken(token.Kind);
            if (highlightKind == Pl0HighlightKind.None || string.IsNullOrEmpty(token.Lexeme))
            {
                continue;
            }

            var rowIndex = Math.Max(0, token.Position.Line - 1);
            var columnStart = Math.Max(0, token.Position.Column - 1);

            if (!newMap.TryGetValue(rowIndex, out var lineHighlights))
            {
                lineHighlights = [];
                newMap[rowIndex] = lineHighlights;
            }

            for (var i = 0; i < token.Lexeme.Length; i++)
            {
                lineHighlights[columnStart + i] = highlightKind;
            }
        }

        highlightMap = newMap;
    }

    private static Pl0HighlightKind ClassifyToken(TokenKind tokenKind)
    {
        return tokenKind switch
        {
            TokenKind.Begin or
            TokenKind.Call or
            TokenKind.Const or
            TokenKind.Do or
            TokenKind.End or
            TokenKind.If or
            TokenKind.Odd or
            TokenKind.Procedure or
            TokenKind.Then or
            TokenKind.Var or
            TokenKind.While => Pl0HighlightKind.Keyword,

            TokenKind.Number => Pl0HighlightKind.Number,

            TokenKind.Bang or
            TokenKind.Becomes or
            TokenKind.Comma or
            TokenKind.Equal or
            TokenKind.Greater or
            TokenKind.GreaterOrEqual or
            TokenKind.Less or
            TokenKind.LessOrEqual or
            TokenKind.LParen or
            TokenKind.Minus or
            TokenKind.NotEqual or
            TokenKind.Period or
            TokenKind.Plus or
            TokenKind.Question or
            TokenKind.RParen or
            TokenKind.Semicolon or
            TokenKind.Slash or
            TokenKind.Times => Pl0HighlightKind.Operator,

            _ => Pl0HighlightKind.None
        };
    }
}
