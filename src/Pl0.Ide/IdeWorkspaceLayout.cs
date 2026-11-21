using System.Drawing;
using Terminal.Gui;

namespace Pl0.Ide;

internal enum IdePane { Source, Code, Debug, Messages, Output }
internal enum IdeDivider { SourceCode, CodeDebug, Rows, MessagesOutput }

internal sealed class IdeWorkspaceLayout
{
    private static readonly (string German, string English)[] DividerNames =
    [
        ("Quellcode / Code", "Source / code"),
        ("Code / Debug", "Code / debug"),
        ("Oben / unten", "Upper / lower"),
        ("Meldungen / Ausgabe", "Messages / output")
    ];

    private readonly View workspace;
    private readonly Window[] panes;
    private readonly IdeLayoutGrip[] grips;
    private readonly Label hint;
    private readonly Label cursorStatus;
    private IdePane selectedPane;
    private IdeDivider? currentDivider;
    private IdeSplitPositions splits = IdeSplitPositions.Default;
    private IdeSplitPositions originalSplits;
    private View? previousFocus;
    private bool? wasCompact;

    internal IdeWorkspaceLayout(View workspace, Window[] panes, Label cursorStatus)
    {
        this.workspace = workspace;
        this.panes = panes;
        this.cursorStatus = cursorStatus;
        grips = Enum.GetValues<IdeDivider>().Select(kind => new IdeLayoutGrip(kind, Move, HandleArrangementKey)).ToArray();
        hint = new Label { Y = Pos.AnchorEnd(1), Width = Dim.Fill(), Height = 1, Visible = false };
        workspace.Add(grips);
        workspace.Add(hint);
        workspace.SubViewLayout += (_, _) => Apply();
    }

    internal static bool IsCompact(Size size) => size.Width < 80 || size.Height < 24;

    internal void SelectPane(IdePane pane)
    {
        EndArrangement(restore: false, restoreFocus: false);
        selectedPane = pane;
        Apply();
        panes[(int)pane].SetFocus();
    }

    internal void StartArrangement()
    {
        if (IsCompact(workspace.Viewport.Size) || currentDivider is not null)
        {
            return;
        }

        previousFocus = workspace.MostFocused;
        originalSplits = splits;
        SetActiveDivider(IdeDivider.SourceCode);
    }

    internal void Reset()
    {
        EndArrangement(restore: false);
        splits = IdeSplitPositions.Default;
        Apply();
    }

    private void Apply()
    {
        var size = workspace.Viewport.Size;
        var compact = IsCompact(size);
        if (compact && wasCompact != true)
        {
            EndArrangement(restore: true, restoreFocus: false);
            var focusedPane = Array.FindIndex(panes, pane => pane.HasFocus);
            if (focusedPane >= 0)
            {
                selectedPane = (IdePane)focusedPane;
            }
        }
        wasCompact = compact;

        var bounds = new Rectangle(0, 1, size.Width, Math.Max(0, size.Height - 2));
        if (compact)
        {
            for (var i = 0; i < panes.Length; i++)
            {
                var visible = i == (int)selectedPane;
                SetFrame(panes[i], bounds, visible);
            }
            foreach (var grip in grips)
            {
                grip.StopDrag();
                grip.Visible = false;
            }
            return;
        }

        var (source, code, top, messages) = splits.GetCuts(bounds.Size);
        var lowerY = bounds.Y + top + 1;
        var lowerHeight = bounds.Height - top - 1;
        SetFrame(panes[0], new(0, bounds.Y, source, top));
        SetFrame(panes[1], new(source + 1, bounds.Y, code - source - 1, top));
        SetFrame(panes[2], new(code + 1, bounds.Y, bounds.Width - code - 1, top));
        SetFrame(panes[3], new(0, lowerY, messages, lowerHeight));
        SetFrame(panes[4], new(messages + 1, lowerY, bounds.Width - messages - 1, lowerHeight));
        SetFrame(grips[0], new(source, bounds.Y, 1, top));
        SetFrame(grips[1], new(code, bounds.Y, 1, top));
        SetFrame(grips[2], new(0, bounds.Y + top, bounds.Width, 1));
        SetFrame(grips[3], new(messages, lowerY, 1, lowerHeight));
    }

    private static void SetFrame(View view, Rectangle frame, bool visible = true)
    {
        if (view.Frame != frame)
        {
            view.Frame = frame;
        }
        if (view.Visible != visible)
        {
            view.Visible = visible;
        }
    }

    internal void Move(IdeDivider divider, int delta)
    {
        if (delta == 0 || IsCompact(workspace.Viewport.Size))
        {
            return;
        }
        var size = workspace.Viewport.Size;
        splits = splits.Move(divider, delta, new Size(size.Width, size.Height - 2));
        Apply();
    }

    internal bool HandleArrangementKey(Key key)
    {
        if (currentDivider is not { } divider)
        {
            return false;
        }
        if (key == Key.Esc || key == Key.Enter)
        {
            EndArrangement(restore: key == Key.Esc);
            Apply();
            return true;
        }
        if (key == Key.Tab || key == Key.Tab.WithShift)
        {
            var direction = key == Key.Tab ? 1 : -1;
            SetActiveDivider((IdeDivider)(((int)divider + direction + grips.Length) % grips.Length));
            return true;
        }
        var horizontal = divider == IdeDivider.Rows;
        if (key == (horizontal ? Key.CursorUp : Key.CursorLeft))
        {
            Move(divider, -1);
        }
        else if (key == (horizontal ? Key.CursorDown : Key.CursorRight))
        {
            Move(divider, 1);
        }
        // Im Größenmodus gehen Eingaben nicht versehentlich in den Quelltext.
        // In size mode, input cannot accidentally reach the source text.
        return true;
    }

    private void SetActiveDivider(IdeDivider divider)
    {
        var previous = currentDivider;
        currentDivider = divider;
        var grip = grips[(int)divider];
        grip.CanFocus = true;
        grip.SetFocus();
        if (previous is { } old && old != divider)
        {
            grips[(int)old].CanFocus = false;
        }
        var (german, english) = DividerNames[(int)divider];
        hint.Text = $"{german} ({english}) | Tab · ↑↓←→ · Enter · Esc";
        hint.Visible = true;
        cursorStatus.Visible = false;
    }

    private void EndArrangement(bool restore, bool restoreFocus = true)
    {
        if (currentDivider is not { } active)
        {
            return;
        }
        if (restore)
        {
            splits = originalSplits;
        }
        currentDivider = null;
        if (restoreFocus)
        {
            if (previousFocus is null || !previousFocus.SetFocus())
            {
                panes[(int)selectedPane].SetFocus();
            }
        }
        previousFocus = null;
        grips[(int)active].CanFocus = false;
        hint.Visible = false;
        cursorStatus.Visible = true;
    }
}

internal readonly record struct IdeSplitPositions(double Source, double Code, double Top, double Messages)
{
    private const int MinimumWidth = 18;
    private const int MinimumHeight = 4;
    internal static IdeSplitPositions Default => new(.45, .70, .70, .65);

    internal (int Source, int Code, int Top, int Messages) GetCuts(Size size)
    {
        var source = Math.Clamp((int)Math.Round(size.Width * Source), MinimumWidth, size.Width - 2 * MinimumWidth - 2);
        var code = Math.Clamp((int)Math.Round(size.Width * Code), source + MinimumWidth + 1, size.Width - MinimumWidth - 1);
        var top = Math.Clamp((int)Math.Round(size.Height * Top), MinimumHeight, size.Height - MinimumHeight - 1);
        var messages = Math.Clamp((int)Math.Round(size.Width * Messages), MinimumWidth, size.Width - MinimumWidth - 1);
        return (source, code, top, messages);
    }

    internal IdeSplitPositions Move(IdeDivider divider, int delta, Size size)
    {
        var (source, code, top, messages) = GetCuts(size);
        return divider switch
        {
            IdeDivider.SourceCode => this with { Source = (double)Math.Clamp(source + delta, MinimumWidth, code - MinimumWidth - 1) / size.Width },
            IdeDivider.CodeDebug => this with { Code = (double)Math.Clamp(code + delta, source + MinimumWidth + 1, size.Width - MinimumWidth - 1) / size.Width },
            IdeDivider.Rows => this with { Top = (double)Math.Clamp(top + delta, MinimumHeight, size.Height - MinimumHeight - 1) / size.Height },
            IdeDivider.MessagesOutput => this with { Messages = (double)Math.Clamp(messages + delta, MinimumWidth, size.Width - MinimumWidth - 1) / size.Width },
            _ => throw new ArgumentOutOfRangeException(nameof(divider))
        };
    }
}

internal sealed class IdeLayoutGrip(
    IdeDivider divider, Action<IdeDivider, int> move, Func<Key, bool> handleKey) : View
{
    private int? lastMouseCoordinate;
    private bool IsHorizontal => divider == IdeDivider.Rows;

    protected override bool OnDrawingContent(DrawContext? context)
    {
        var active = HasFocus || lastMouseCoordinate.HasValue;
        var scheme = GetScheme();
        SetAttribute(active ? scheme.Focus : scheme.Normal);
        var length = IsHorizontal ? Viewport.Width : Viewport.Height;
        var line = IsHorizontal ? '─' : '│';
        for (var cell = 0; cell < length; cell++)
        {
            Move(IsHorizontal ? cell : 0, IsHorizontal ? 0 : cell);
            AddRune(cell == length / 2 ? IsHorizontal ? '↕' : '↔' : line);
        }
        return true;
    }

    protected override bool OnKeyDown(Key key) => handleKey(key) || base.OnKeyDown(key);

    protected override bool OnMouseEvent(Mouse mouse)
    {
        var coordinate = IsHorizontal ? mouse.ScreenPosition.Y : mouse.ScreenPosition.X;
        if (lastMouseCoordinate is { } previous)
        {
            if (mouse.Flags.HasFlag(MouseFlags.LeftButtonReleased))
            {
                move(divider, coordinate - previous);
                StopDrag();
                return true;
            }
            if (mouse.Flags.HasFlag(MouseFlags.PositionReport) || mouse.Flags.HasFlag(MouseFlags.LeftButtonPressed))
            {
                lastMouseCoordinate = coordinate;
                move(divider, coordinate - previous);
                return true;
            }
        }
        else if (mouse.Flags.HasFlag(MouseFlags.LeftButtonPressed) && App?.Mouse is { } mouseHandler)
        {
            mouseHandler.GrabMouse(this);
            if (mouseHandler.IsGrabbed(this))
            {
                lastMouseCoordinate = coordinate;
                SetNeedsDraw();
                return true;
            }
        }
        return base.OnMouseEvent(mouse);
    }

    internal void StopDrag()
    {
        if (!lastMouseCoordinate.HasValue)
        {
            return;
        }
        lastMouseCoordinate = null;
        if (App?.Mouse is { } mouse && mouse.IsGrabbed(this))
        {
            mouse.UngrabMouse();
        }
        SetNeedsDraw();
    }
}
