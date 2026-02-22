using Terminal.Gui;

namespace Pl0.Ide;

internal sealed class IdeRainbowAsciiArtView : TextView
{
    private Dictionary<int, Dictionary<int, int>> rainbowMap = [];

    private static readonly Color[] RainbowColors =
    [
        Color.BrightRed,
        Color.BrightYellow,
        Color.BrightGreen,
        Color.BrightCyan,
        Color.BrightBlue,
        Color.BrightMagenta
    ];

    public IdeRainbowAsciiArtView()
    {
        Multiline = true;
        ReadOnly = true;
        Text = string.Empty;
    }

    internal void SetAsciiArt(IReadOnlyList<string> asciiArt)
    {
        Text = string.Join(Environment.NewLine, asciiArt);
        RebuildRainbowMap(asciiArt);
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

    protected override void OnDrawNormalColor(List<Cell> line, int idxCol, int idxRow)
    {
        var colorIndex = GetRainbowColorIndexAt(idxRow, idxCol);
        if (colorIndex < 0)
        {
            base.OnDrawNormalColor(line, idxCol, idxRow);
            return;
        }

        var scheme = ColorScheme;
        var background = scheme?.Normal.Background ?? Color.Black;
        var foreground = RainbowColors[colorIndex % RainbowColors.Length];
        SetAttribute(new Terminal.Gui.Attribute(foreground, background));
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
