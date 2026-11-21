using Pl0.Ide;

namespace Pl0.Tests;

[Collection("IDE GUI")]
public sealed class IdeEditorMigrationTests
{
    [Fact]
    public void All_Text_Panes_Use_The_Current_Editor_Control()
    {
        var mainView = new IdeMainView();
        const string editorType = "Terminal.Gui.Editor.Editor";

        Assert.Equal(editorType, mainView.SourceEditor.GetType().BaseType?.FullName);
        Assert.All(new object[]
        {
            mainView.PCodeOutput,
            mainView.MessagesOutput,
            mainView.RuntimeOutput,
            mainView.DebugOutput
        }, pane => Assert.Contains(editorType, new[] { pane.GetType().FullName, pane.GetType().BaseType?.FullName }));
    }
}
