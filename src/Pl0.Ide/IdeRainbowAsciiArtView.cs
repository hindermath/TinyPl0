using Terminal.Gui;
using EditorView = Terminal.Gui.Editor.Editor;

namespace Pl0.Ide;

internal sealed class IdeRainbowAsciiArtView : EditorView
{
    private Dictionary<int, Dictionary<int, int>> rainbowMap = [];

    private static readonly Color[] RainbowColors =
    [
        new Color("#FFB3B3"),
        new Color("#FFD700"),
        new Color("#B8FFB8"),
        new Color("#00FFFF"),
        new Color("#B8C8FF"),
        new Color("#FFB8FF")
    ];

    internal static IReadOnlyList<Color> RainbowPalette { get; } = Array.AsReadOnly(RainbowColors);

    public IdeRainbowAsciiArtView()
    {
        Multiline = true;
        ReadOnly = true;
        Text = string.Empty;
        LineTransformers.Add(new IdeColorTransformer(GetRainbowAttributeAt));
    }

    internal void SetAsciiArt(IReadOnlyList<string> asciiArt)
    {
        RebuildRainbowMap(asciiArt);
        Text = string.Join(Environment.NewLine, asciiArt);
    }

    internal int GetRainbowColorIndexAt(int rowIndex, int columnIndex)
    {
        if (!rainbowMap.TryGetValue(rowIndex, out var row))
        {
            return -1;
        }

        return row.TryGetValue(columnIndex, out var colorIndex)
            ? colorIndex
            : -1;
    }

    private Terminal.Gui.Drawing.Attribute? GetRainbowAttributeAt(int rowIndex, int columnIndex)
    {
        var colorIndex = GetRainbowColorIndexAt(rowIndex, columnIndex);
        if (colorIndex < 0)
        {
            return null;
        }

        var scheme = GetScheme();
        var background = scheme?.Normal.Background ?? Color.Black;
        var foreground = RainbowColors[colorIndex % RainbowColors.Length];
        return new Terminal.Gui.Drawing.Attribute(foreground, background);
    }

    private void RebuildRainbowMap(IReadOnlyList<string> asciiArt)
    {
        var map = new Dictionary<int, Dictionary<int, int>>();

        for (var row = 0; row < asciiArt.Count; row++)
        {
            var line = asciiArt[row];
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var rowMap = new Dictionary<int, int>();
            for (var col = 0; col < line.Length; col++)
            {
                if (char.IsWhiteSpace(line[col]))
                {
                    continue;
                }

                rowMap[col] = col % RainbowColors.Length;
            }

            if (rowMap.Count > 0)
            {
                map[row] = rowMap;
            }
        }

        rainbowMap = map;
    }
}
