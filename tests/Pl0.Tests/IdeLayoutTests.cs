using System.Drawing;
using Pl0.Ide;
using Terminal.Gui.Input;
using Terminal.Gui.Views;

namespace Pl0.Tests;

[Collection("IDE GUI")]
public sealed class IdeLayoutTests
{
    [Fact]
    public void Compact_Layout_Keeps_The_Same_Editor_And_Restores_Five_Panes()
    {
        using var ide = new IdeMainView();
        ide.SourceEditor.Text = "begin ! 42 end.";
        var document = ide.SourceEditor.Document;

        Assert.True(ide.Layout(new Size(79, 23)));
        var visible = Assert.Single(ide.SubViews.OfType<Window>(), window => window.Visible);
        Assert.Same(ide.SourceEditor, Assert.Single(visible.SubViews));
        ide.MessagesOutput.Text = "Testmeldung";
        ide.WorkspaceLayout.SelectPane(IdePane.Messages);
        var messages = Assert.Single(ide.SubViews.OfType<Window>(), window => window.Visible);
        Assert.Same(ide.MessagesOutput, Assert.Single(messages.SubViews));

        Assert.True(ide.Layout(new Size(80, 24)));
        Assert.Equal(5, ide.SubViews.OfType<Window>().Count(window => window.Visible));
        Assert.Same(document, ide.SourceEditor.Document);
        Assert.Equal("Testmeldung", ide.MessagesOutput.Text);
    }

    [Fact]
    public void Keyboard_Resize_Can_Cancel_Commit_And_Reset()
    {
        using var ide = new IdeMainView();
        var size = new Size(134, 27);
        Assert.True(ide.Layout(size));
        var layout = ide.WorkspaceLayout;
        var panes = ide.SubViews.OfType<Window>().ToDictionary(window => window.Title.ToString());
        var source = panes["Quellcode"];
        var code = panes["Assembler-Code"];
        var messages = panes["Meldungen"];
        var originalSource = source.Frame;
        var originalCode = code.Frame;
        var originalMessages = messages.Frame;

        layout.StartArrangement();
        Assert.True(layout.HandleArrangementKey(Key.CursorRight));
        Assert.Equal(originalSource.Width + 1, source.Frame.Width);
        Assert.Equal(originalCode.Width - 1, code.Frame.Width);
        Assert.True(layout.HandleArrangementKey(Key.Tab));
        Assert.True(layout.HandleArrangementKey(Key.CursorRight));
        Assert.Equal(originalCode.Right + 1, code.Frame.Right);
        Assert.True(layout.HandleArrangementKey(Key.Esc));
        Assert.Equal(originalSource, source.Frame);
        Assert.Equal(originalCode, code.Frame);

        layout.StartArrangement();
        Assert.True(layout.HandleArrangementKey(Key.Tab.WithShift));
        Assert.True(layout.HandleArrangementKey(Key.CursorLeft));
        Assert.True(layout.HandleArrangementKey(Key.Enter));
        Assert.Equal(originalMessages.Width - 1, messages.Frame.Width);

        Assert.True(ide.Layout(new Size(79, 23)));
        Assert.True(ide.Layout(size));
        Assert.Equal(originalMessages.Width - 1, messages.Frame.Width);
        layout.Reset();
        Assert.Equal(originalMessages, messages.Frame);
    }

    [Fact]
    public void Divider_Moves_Stay_Inside_The_Minimum_Pane_Sizes()
    {
        var size = new Size(80, 22);
        var splits = IdeSplitPositions.Default.Move(IdeDivider.SourceCode, 100, size)
            .Move(IdeDivider.CodeDebug, 100, size)
            .Move(IdeDivider.Rows, 100, size)
            .Move(IdeDivider.MessagesOutput, -100, size);
        var (source, code, top, messages) = splits.GetCuts(size);
        Assert.True(source >= 18);
        Assert.Equal(18, size.Width - code - 1);
        Assert.True(code - source - 1 >= 18);
        Assert.Equal(4, size.Height - top - 1);
        Assert.Equal(18, messages);
    }
}
