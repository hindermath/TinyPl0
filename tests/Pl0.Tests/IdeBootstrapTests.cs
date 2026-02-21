using Pl0.Ide;
using Terminal.Gui;

namespace Pl0.Tests;

public sealed class IdeBootstrapTests
{
    [Fact]
    public void CreateApplication_Uses_V2_Backend()
    {
        var app = Pl0.Ide.Program.CreateApplication();
        Assert.False(app.IsLegacy);
    }

    [Fact]
    public void MainView_Contains_Core_Panes()
    {
        var mainView = new IdeMainView();
        Assert.Single(mainView.Subviews.OfType<MenuBar>());

        var windows = mainView.Subviews.OfType<Window>().ToArray();

        Assert.Equal(3, windows.Length);
        Assert.Contains(windows, window => window.Title.ToString() == "Quellcode");
        Assert.Contains(windows, window => window.Title.ToString() == "P-Code");
        Assert.Contains(windows, window => window.Title.ToString() == "Meldungen");
    }

    [Fact]
    public void MainView_Contains_Dedicated_Source_Code_Editor_In_Source_Window()
    {
        var mainView = new IdeMainView();
        var sourceWindow = mainView
            .Subviews
            .OfType<Window>()
            .Single(window => window.Title.ToString() == "Quellcode");

        var sourceEditor = Assert.Single(sourceWindow.Subviews.OfType<TextView>());
        Assert.Same(mainView.SourceEditor, sourceEditor);
        Assert.False(sourceEditor.ReadOnly);
        Assert.True(sourceEditor.Multiline);
    }

    [Fact]
    public void SourceEditor_Highlights_Keywords_Numbers_And_Operators()
    {
        var editor = new Pl0SourceEditorView
        {
            Text = "const x = 1;\n! x + 2."
        };

        Assert.Equal(Pl0HighlightKind.Keyword, editor.GetHighlightKindAt(0, 0)); // c in const
        Assert.Equal(Pl0HighlightKind.Operator, editor.GetHighlightKindAt(0, 8)); // =
        Assert.Equal(Pl0HighlightKind.Number, editor.GetHighlightKindAt(0, 10)); // 1
        Assert.Equal(Pl0HighlightKind.Operator, editor.GetHighlightKindAt(1, 0)); // !
        Assert.Equal(Pl0HighlightKind.Operator, editor.GetHighlightKindAt(1, 4)); // +
        Assert.Equal(Pl0HighlightKind.Number, editor.GetHighlightKindAt(1, 6)); // 2
    }

    [Fact]
    public void SourceEditor_Does_Not_Highlight_Identifiers_As_Keywords()
    {
        var editor = new Pl0SourceEditorView
        {
            Text = "foo := 42;"
        };

        Assert.Equal(Pl0HighlightKind.None, editor.GetHighlightKindAt(0, 0)); // f in foo
        Assert.Equal(Pl0HighlightKind.Operator, editor.GetHighlightKindAt(0, 4)); // :
        Assert.Equal(Pl0HighlightKind.Number, editor.GetHighlightKindAt(0, 7)); // 4
    }

    [Fact]
    public void CompileSource_FromEditor_Succeeds_For_Valid_Source_And_Writes_PCode()
    {
        var mainView = new IdeMainView();
        mainView.SourceEditor.Text = """
                                     var x;
                                     begin
                                       x := 1
                                     end.
                                     """;

        var result = mainView.CompileSource();
        var pCodeText = mainView.PCodeOutput.Text?.ToString() ?? string.Empty;
        var messagesText = mainView.MessagesOutput.Text?.ToString() ?? string.Empty;

        Assert.True(result.Success);
        Assert.Same(result, mainView.LastCompilationResult);
        Assert.NotEmpty(pCodeText);
        Assert.Contains("Kompilierung erfolgreich", messagesText);
    }

    [Fact]
    public void CompileSource_FromEditor_Returns_Diagnostics_For_Invalid_Source()
    {
        var mainView = new IdeMainView();
        mainView.SourceEditor.Text = """
                                     var x;
                                     begin
                                       x := 1
                                     """;

        var result = mainView.CompileSource();
        var pCodeText = mainView.PCodeOutput.Text?.ToString() ?? string.Empty;
        var messagesText = mainView.MessagesOutput.Text?.ToString() ?? string.Empty;

        Assert.False(result.Success);
        Assert.Same(result, mainView.LastCompilationResult);
        Assert.Empty(pCodeText);
        Assert.Contains("E", messagesText);
    }

    [Fact]
    public void SaveSourceFile_Writes_Source_Lossless_To_Pl0_File()
    {
        var tempDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"pl0-ide-{Guid.NewGuid():N}"));
        try
        {
            var savePathWithoutExtension = Path.Combine(tempDirectory.FullName, "beispiel");
            var expectedPath = $"{savePathWithoutExtension}.pl0";
            var dialogs = new StubIdeFileDialogService(savePath: savePathWithoutExtension);
            var mainView = new IdeMainView(dialogs, new PhysicalIdeFileStorage());
            var source = """
                         var x;
                         begin
                           x := 7
                         end.
                         """;

            mainView.SourceEditor.Text = source;
            var saved = mainView.SaveSourceFile();
            var messagesText = mainView.MessagesOutput.Text?.ToString() ?? string.Empty;

            Assert.True(saved);
            Assert.Equal(expectedPath, mainView.CurrentSourcePath);
            Assert.Equal(source, File.ReadAllText(expectedPath));
            Assert.Contains("Datei gespeichert", messagesText);
            Assert.Equal("Quellcode: beispiel.pl0", mainView.SourceWindowTitle);
            Assert.Equal("P-Code: beispiel.pl0", mainView.PCodeWindowTitle);
        }
        finally
        {
            tempDirectory.Delete(recursive: true);
        }
    }

    [Fact]
    public void OpenSourceFile_Loads_Source_Lossless_From_Pl0_File()
    {
        var tempDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"pl0-ide-{Guid.NewGuid():N}"));
        try
        {
            var sourcePath = Path.Combine(tempDirectory.FullName, "programm.pl0");
            var source = """
                         const x = 3;
                         begin
                           ! x
                         end.
                         """;
            File.WriteAllText(sourcePath, source);

            var dialogs = new StubIdeFileDialogService(openPath: sourcePath);
            var mainView = new IdeMainView(dialogs, new PhysicalIdeFileStorage());

            var opened = mainView.OpenSourceFile();
            var loadedSource = mainView.SourceEditor.Text?.ToString() ?? string.Empty;
            var messagesText = mainView.MessagesOutput.Text?.ToString() ?? string.Empty;

            Assert.True(opened);
            Assert.Equal(source, loadedSource);
            Assert.Equal(sourcePath, mainView.CurrentSourcePath);
            Assert.Contains("Datei geladen", messagesText);
            Assert.Equal("Quellcode: programm.pl0", mainView.SourceWindowTitle);
            Assert.Equal("P-Code: programm.pl0", mainView.PCodeWindowTitle);
        }
        finally
        {
            tempDirectory.Delete(recursive: true);
        }
    }

    [Fact]
    public void OpenSourceFile_Loads_Source_From_Uppercase_Pl0_Extension()
    {
        var tempDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"pl0-ide-{Guid.NewGuid():N}"));
        try
        {
            var sourcePath = Path.Combine(tempDirectory.FullName, "programm.PL0");
            var source = "begin ! 1 end.";
            File.WriteAllText(sourcePath, source);

            var dialogs = new StubIdeFileDialogService(openPath: sourcePath);
            var mainView = new IdeMainView(dialogs, new PhysicalIdeFileStorage());

            var opened = mainView.OpenSourceFile();

            Assert.True(opened);
            Assert.Equal(source, mainView.SourceEditor.Text?.ToString() ?? string.Empty);
        }
        finally
        {
            tempDirectory.Delete(recursive: true);
        }
    }

    [Fact]
    public void WindowTitles_Show_Only_FileName_After_Open()
    {
        var sourcePath = Path.Combine("/tmp", "unterordner", "demo.pl0");
        var dialogs = new StubIdeFileDialogService(openPath: sourcePath);
        var mainView = new IdeMainView(dialogs, new StubIdeFileStorage(contentByPath: new Dictionary<string, string>
        {
            [sourcePath] = "begin end."
        }));

        var opened = mainView.OpenSourceFile();

        Assert.True(opened);
        Assert.Equal("Quellcode: demo.pl0", mainView.SourceWindowTitle);
        Assert.Equal("P-Code: demo.pl0", mainView.PCodeWindowTitle);
    }

    [Fact]
    public void OpenSourceFile_Rejects_NonPl0_File_Extension()
    {
        var tempDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"pl0-ide-{Guid.NewGuid():N}"));
        try
        {
            var sourcePath = Path.Combine(tempDirectory.FullName, "programm.txt");
            File.WriteAllText(sourcePath, "begin end.");

            var dialogs = new StubIdeFileDialogService(openPath: sourcePath);
            var mainView = new IdeMainView(dialogs, new PhysicalIdeFileStorage());

            var opened = mainView.OpenSourceFile();
            var messagesText = mainView.MessagesOutput.Text?.ToString() ?? string.Empty;

            Assert.False(opened);
            Assert.Contains(".pl0", messagesText);
            Assert.Null(mainView.CurrentSourcePath);
        }
        finally
        {
            tempDirectory.Delete(recursive: true);
        }
    }

    [Fact]
    public void MainView_Uses_Classic_Turbo_Pascal_Menu_Structure()
    {
        var mainView = new IdeMainView();
        var menuBar = Assert.Single(mainView.Subviews.OfType<MenuBar>());

        var menuTitles = menuBar.Menus.Select(menu => menu.Title.ToString()).ToArray();
        Assert.Equal(["_Datei", "_Bearbeiten", "_Kompilieren", "_Ausfuehren", "_Debug", "_Hilfe"], menuTitles);
    }

    [Fact]
    public void LookAndFeel_Applies_TurboPascal5_Theme_When_Available()
    {
        var exception = Record.Exception(() => _ = IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());
        Assert.Null(exception);
    }

    [Fact]
    public void LookAndFeel_Uses_TurboPascal5_Theme_Name()
    {
        Assert.Equal("TurboPascal 5", IdeLookAndFeel.TurboPascalThemeName);
    }

    [Fact]
    public void FileDialogFilters_For_Open_Use_Pl0_Default_And_AllFiles_Alternative()
    {
        var filters = TerminalGuiIdeFileDialogService.CreateOpenAllowedTypes();

        Assert.Equal(2, filters.Count);
        Assert.True(filters[0].IsAllowed("programm.pl0"));
        Assert.False(filters[0].IsAllowed("programm.txt"));
        Assert.True(filters[1].IsAllowed("programm.txt"));
    }

    [Fact]
    public void FileDialogFilters_For_Save_Allow_PlainName_And_Pl0_As_Default()
    {
        var filters = TerminalGuiIdeFileDialogService.CreateSaveAllowedTypes();

        Assert.Equal(2, filters.Count);
        Assert.True(filters[0].IsAllowed("programm"));
        Assert.True(filters[0].IsAllowed("programm.pl0"));
        Assert.False(filters[0].IsAllowed("programm.txt"));
        Assert.True(filters[1].IsAllowed("programm.txt"));
    }

    private sealed class StubIdeFileDialogService(string? openPath = null, string? savePath = null) : IIdeFileDialogService
    {
        public string? ShowOpenDialog()
        {
            return openPath;
        }

        public string? ShowSaveDialog(string? currentPath)
        {
            return savePath;
        }
    }

    private sealed class StubIdeFileStorage(Dictionary<string, string>? contentByPath = null) : IIdeFileStorage
    {
        private readonly Dictionary<string, string> contentByPath = contentByPath ?? [];

        public string ReadAllText(string path)
        {
            if (!contentByPath.TryGetValue(path, out var content))
            {
                throw new FileNotFoundException("Datei nicht gefunden.", path);
            }

            return content;
        }

        public void WriteAllText(string path, string content)
        {
            contentByPath[path] = content;
        }
    }
}
