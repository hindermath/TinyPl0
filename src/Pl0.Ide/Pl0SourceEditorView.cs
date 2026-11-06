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
    private const int MaximumHighlightedSourceLength = 200_000;
    private readonly CompilerOptions compilerOptions;
    private Dictionary<int, List<HighlightSpan>> highlightMap = [];

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

        var lowerBound = 0;
        var upperBound = lineHighlights.Count - 1;

        while (lowerBound <= upperBound)
        {
            var middle = lowerBound + ((upperBound - lowerBound) / 2);
            var candidate = lineHighlights[middle];
            if (columnIndex < candidate.Start)
            {
                upperBound = middle - 1;
            }
            else if (columnIndex >= candidate.End)
            {
                lowerBound = middle + 1;
            }
            else
            {
                return candidate.Kind;
            }
        }

        return Pl0HighlightKind.None;
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
        if (text.Length > MaximumHighlightedSourceLength)
        {
            highlightMap = [];
            return;
        }

        var lexer = new Pl0Lexer(text, compilerOptions);
        var lexerResult = lexer.Lex();

        var newMap = new Dictionary<int, List<HighlightSpan>>();

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

            lineHighlights.Add(new HighlightSpan(
                columnStart,
                checked(columnStart + token.Lexeme.Length),
                highlightKind));
        }

        // Die Binärsuche benötigt sortierte Bereiche; die einmalige Sortierung hält das Zeichnen schnell.
        // Binary search requires sorted spans; sorting once keeps per-cell drawing fast.
        foreach (var lineHighlights in newMap.Values)
        {
            lineHighlights.Sort(static (left, right) => left.Start.CompareTo(right.Start));
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

    private readonly record struct HighlightSpan(int Start, int End, Pl0HighlightKind Kind);
}
