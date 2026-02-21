using Pl0.Core;
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

        Assert.Equal(4, windows.Length);
        Assert.Contains(windows, window => window.Title.ToString() == "Quellcode");
        Assert.Contains(windows, window => window.Title.ToString() == "Assembler-Code");
        Assert.Contains(windows, window => window.Title.ToString() == "Meldungen");
        Assert.Contains(windows, window => window.Title.ToString() == "Ausgabe");
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
        Assert.Contains("0000: ", pCodeText);
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
    public void CompileSource_Shows_All_Collected_Diagnostics_In_Messages_Window()
    {
        var mainView = new IdeMainView();
        mainView.SourceEditor.Text = """
                                     begin
                                       y := ;
                                       if then ;
                                       !
                                     end.
                                     """;

        var result = mainView.CompileSource();
        var messagesText = mainView.MessagesOutput.Text?.ToString() ?? string.Empty;

        Assert.False(result.Success);
        Assert.True(result.Diagnostics.Count > 1);
        Assert.Contains("E11", messagesText);
        Assert.Contains("E99", messagesText);
        Assert.Contains(Environment.NewLine, messagesText);
    }

    [Fact]
    public void OpenCompilerSettingsDialog_Changes_Options_For_Next_Compile()
    {
        var mainView = new IdeMainView(
            new StubIdeFileDialogService(),
            new StubIdeFileStorage(),
            new StubCompilerSettingsDialogService([
                new IdeCompilerSettingsDialogResult(
                    new CompilerOptions(Pl0Dialect.Classic, 3, 2047, 10, 10, 100, 200),
                    IdeCodeDisplayMode.PCode)
            ]));

        mainView.SourceEditor.Text = "begin ! 1 end.";
        var firstResult = mainView.CompileSource();

        var changed = mainView.OpenCompilerSettingsDialog();
        var secondResult = mainView.CompileSource();

        Assert.True(firstResult.Success);
        Assert.True(changed);
        Assert.Equal(Pl0Dialect.Classic, mainView.CurrentCompilerOptions.Dialect);
        Assert.Equal(IdeCompilerOptionsRules.MaxNumberDigitsClassic, mainView.CurrentCompilerOptions.MaxNumberDigits);
        Assert.Equal(IdeCodeDisplayMode.PCode, mainView.CurrentCodeDisplayMode);
        Assert.False(secondResult.Success);
    }

    [Fact]
    public void OpenCompilerSettingsDialog_Rejects_Invalid_Values()
    {
        var mainView = new IdeMainView(
            new StubIdeFileDialogService(),
            new StubIdeFileStorage(),
            new StubCompilerSettingsDialogService([
                new IdeCompilerSettingsDialogResult(
                    new CompilerOptions(Pl0Dialect.Extended, 42, 2047, 10, 99, 100, 200),
                    IdeCodeDisplayMode.Assembler)
            ]));

        var changed = mainView.OpenCompilerSettingsDialog();
        var messagesText = mainView.MessagesOutput.Text?.ToString() ?? string.Empty;

        Assert.False(changed);
        Assert.Equal(Pl0Dialect.Extended, mainView.CurrentCompilerOptions.Dialect);
        Assert.Equal(IdeCompilerOptionsRules.MaxNumberDigitsExtended, mainView.CurrentCompilerOptions.MaxNumberDigits);
        Assert.Equal(IdeCodeDisplayMode.Assembler, mainView.CurrentCodeDisplayMode);
        Assert.Contains("MaxLevel", messagesText);
    }

    [Fact]
    public void CompileSource_Can_Switch_Between_PCode_And_Assembler_Display()
    {
        var mainView = new IdeMainView(
            new StubIdeFileDialogService(),
            new StubIdeFileStorage(),
            new StubCompilerSettingsDialogService([
                new IdeCompilerSettingsDialogResult(
                    new CompilerOptions(Pl0Dialect.Extended, 3, 2047, 10, 10, 100, 200),
                    IdeCodeDisplayMode.PCode),
                new IdeCompilerSettingsDialogResult(
                    new CompilerOptions(Pl0Dialect.Extended, 3, 2047, 10, 10, 100, 200),
                    IdeCodeDisplayMode.Assembler)
            ]));
        mainView.SourceEditor.Text = """
                                     var x;
                                     begin
                                       x := 1
                                     end.
                                     """;

        _ = mainView.CompileSource();
        var assemblerTextBeforeSwitch = mainView.PCodeOutput.Text?.ToString() ?? string.Empty;

        var changedToPCode = mainView.OpenCompilerSettingsDialog();
        _ = mainView.CompileSource();
        var pCodeText = mainView.PCodeOutput.Text?.ToString() ?? string.Empty;

        Assert.Contains("0000: ", assemblerTextBeforeSwitch);
        Assert.True(char.IsLetter(GetInstructionTokenFromFirstLine(assemblerTextBeforeSwitch)[0]));
        Assert.True(changedToPCode);
        Assert.Equal(IdeCodeDisplayMode.PCode, mainView.CurrentCodeDisplayMode);
        Assert.Equal("P-Code", mainView.PCodeWindowTitle);
        Assert.Contains("0000: ", pCodeText);
        Assert.True(char.IsDigit(GetInstructionTokenFromFirstLine(pCodeText)[0]));

        var changedToAssembler = mainView.OpenCompilerSettingsDialog();
        _ = mainView.CompileSource();
        var assemblerTextAfterSwitch = mainView.PCodeOutput.Text?.ToString() ?? string.Empty;

        Assert.True(changedToAssembler);
        Assert.Equal(IdeCodeDisplayMode.Assembler, mainView.CurrentCodeDisplayMode);
        Assert.Equal("Assembler-Code", mainView.PCodeWindowTitle);
        Assert.Contains("0000: ", assemblerTextAfterSwitch);
        Assert.True(char.IsLetter(GetInstructionTokenFromFirstLine(assemblerTextAfterSwitch)[0]));
    }

    [Fact]
    public void RunCompiledCode_Requires_Successful_Compilation()
    {
        var runtimeDialogs = new StubIdeRuntimeDialogService([]);
        var mainView = new IdeMainView(
            new StubIdeFileDialogService(),
            new StubIdeFileStorage(),
            new StubCompilerSettingsDialogService([]),
            runtimeDialogs);

        var executed = mainView.RunCompiledCode();

        Assert.False(executed);
        Assert.Contains("zuerst erfolgreich kompilieren", mainView.MessagesOutput.Text?.ToString() ?? string.Empty);
    }

    [Fact]
    public void RunCompiledCode_Executes_Program_And_Writes_Runtime_Output()
    {
        var runtimeDialogs = new StubIdeRuntimeDialogService([]);
        var mainView = new IdeMainView(
            new StubIdeFileDialogService(),
            new StubIdeFileStorage(),
            new StubCompilerSettingsDialogService([]),
            runtimeDialogs);
        mainView.SourceEditor.Text = """
                                     var x;
                                     begin
                                       x := 7;
                                       ! x
                                     end.
                                     """;

        var compiled = mainView.CompileSource();
        var executed = mainView.RunCompiledCode();

        Assert.True(compiled.Success);
        Assert.True(executed);
        Assert.Equal("7", mainView.RuntimeOutput.Text?.ToString());
        Assert.Contains("Ausfuehrung erfolgreich", mainView.MessagesOutput.Text?.ToString());
        Assert.Equal(0, runtimeDialogs.ReadCount);
    }

    [Fact]
    public void RunCompiledCode_Uses_Ide_Input_Dialog_For_Question_Operator()
    {
        var runtimeDialogs = new StubIdeRuntimeDialogService([42]);
        var mainView = new IdeMainView(
            new StubIdeFileDialogService(),
            new StubIdeFileStorage(),
            new StubCompilerSettingsDialogService([]),
            runtimeDialogs);
        mainView.SourceEditor.Text = """
                                     var x;
                                     begin
                                       ? x;
                                       ! x
                                     end.
                                     """;

        var compiled = mainView.CompileSource();
        var executed = mainView.RunCompiledCode();

        Assert.True(compiled.Success);
        Assert.True(executed);
        Assert.Equal("42", mainView.RuntimeOutput.Text?.ToString());
        Assert.Equal(1, runtimeDialogs.ReadCount);
    }

    [Fact]
    public void RunCompiledCode_Shows_Runtime_Diagnostics_On_Error()
    {
        var runtimeDialogs = new StubIdeRuntimeDialogService([]);
        var mainView = new IdeMainView(
            new StubIdeFileDialogService(),
            new StubIdeFileStorage(),
            new StubCompilerSettingsDialogService([]),
            runtimeDialogs);
        mainView.SourceEditor.Text = """
                                     begin
                                       ! 1 / 0
                                     end.
                                     """;

        var compiled = mainView.CompileSource();
        var executed = mainView.RunCompiledCode();
        var messagesText = mainView.MessagesOutput.Text?.ToString() ?? string.Empty;

        Assert.True(compiled.Success);
        Assert.False(executed);
        Assert.Contains("R206", messagesText);
    }

    [Fact]
    public void CompileAndRun_Compiles_And_Executes_In_One_Step()
    {
        var runtimeDialogs = new StubIdeRuntimeDialogService([]);
        var mainView = new IdeMainView(
            new StubIdeFileDialogService(),
            new StubIdeFileStorage(),
            new StubCompilerSettingsDialogService([]),
            runtimeDialogs);
        mainView.SourceEditor.Text = """
                                     var x;
                                     begin
                                       x := 9;
                                       ! x
                                     end.
                                     """;

        var succeeded = mainView.CompileAndRun();

        Assert.True(succeeded);
        Assert.NotNull(mainView.LastCompilationResult);
        Assert.True(mainView.LastCompilationResult!.Success);
        Assert.Equal("9", mainView.RuntimeOutput.Text?.ToString());
        Assert.Contains("Ausfuehrung erfolgreich", mainView.MessagesOutput.Text?.ToString());
    }

    [Fact]
    public void ExportCompiledCode_Requires_Successful_Compilation()
    {
        var mainView = new IdeMainView(
            new StubIdeFileDialogService(exportPath: "/tmp/ignored.asm"),
            new StubIdeFileStorage());

        var exported = mainView.ExportCompiledCode(IdeEmitMode.Asm);
        var messagesText = mainView.MessagesOutput.Text?.ToString() ?? string.Empty;

        Assert.False(exported);
        Assert.Contains("zuerst erfolgreich kompilieren", messagesText);
    }

    [Fact]
    public void ExportCompiledCode_Writes_Asm_Output()
    {
        var tempDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"pl0-ide-export-{Guid.NewGuid():N}"));
        try
        {
            var exportPath = Path.Combine(tempDirectory.FullName, "programm.asm");
            var mainView = new IdeMainView(
                new StubIdeFileDialogService(exportPath: exportPath),
                new PhysicalIdeFileStorage());
            mainView.SourceEditor.Text = """
                                         var x;
                                         begin
                                           x := 1
                                         end.
                                         """;

            var compiled = mainView.CompileSource();
            var exported = mainView.ExportCompiledCode(IdeEmitMode.Asm);
            var payload = File.ReadAllText(exportPath);

            Assert.True(compiled.Success);
            Assert.True(exported);
            Assert.Contains("jmp", payload);
            Assert.DoesNotContain("0000:", payload);
        }
        finally
        {
            tempDirectory.Delete(recursive: true);
        }
    }

    [Fact]
    public void ExportCompiledCode_Cod_Uses_Cod_Extension_Only()
    {
        var tempDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"pl0-ide-export-{Guid.NewGuid():N}"));
        try
        {
            var enteredPath = Path.Combine(tempDirectory.FullName, "programm.txt");
            var expectedPath = Path.Combine(tempDirectory.FullName, "programm.cod");
            var mainView = new IdeMainView(
                new StubIdeFileDialogService(exportPath: enteredPath),
                new PhysicalIdeFileStorage());
            mainView.SourceEditor.Text = """
                                         var x;
                                         begin
                                           x := 1
                                         end.
                                         """;

            var compiled = mainView.CompileSource();
            var exported = mainView.ExportCompiledCode(IdeEmitMode.Cod);
            var payload = File.ReadAllText(expectedPath);

            Assert.True(compiled.Success);
            Assert.True(exported);
            Assert.False(File.Exists(enteredPath));
            Assert.True(File.Exists(expectedPath));
            Assert.True(char.IsDigit(payload.TrimStart()[0]));
        }
        finally
        {
            tempDirectory.Delete(recursive: true);
        }
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
            Assert.Equal("Assembler-Code: beispiel.pl0", mainView.PCodeWindowTitle);
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
            Assert.Equal("Assembler-Code: programm.pl0", mainView.PCodeWindowTitle);
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
        Assert.Equal("Assembler-Code: demo.pl0", mainView.PCodeWindowTitle);
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
    public void FormatSource_Normalizes_Indentation_Spacing_And_LineBreaks()
    {
        var mainView = new IdeMainView();
        mainView.SourceEditor.Text = "procedure p;var x;begin x:=1+2;end;begin call p;end.";

        var formatted = mainView.FormatSource();
        var formattedText = mainView.SourceEditor.Text?.ToString() ?? string.Empty;
        var expected = string.Join(
            Environment.NewLine,
            [
                "procedure p;",
                "  var x;",
                "  begin",
                "    x := 1 + 2;",
                "  end;",
                "begin",
                "  call p;",
                "end."
            ]);

        Assert.True(formatted);
        Assert.Equal(expected, formattedText);
        Assert.Equal("Quelltext formatiert.", mainView.MessagesOutput.Text?.ToString());
    }

    [Fact]
    public void FormatSource_Is_Idempotent()
    {
        var mainView = new IdeMainView();
        var expected = string.Join(
            Environment.NewLine,
            [
                "procedure p;",
                "  var x;",
                "  begin",
                "    x := 1 + 2;",
                "  end;",
                "begin",
                "  call p;",
                "end."
            ]);
        mainView.SourceEditor.Text = expected;

        var formatted = mainView.FormatSource();
        var formattedAgain = mainView.FormatSource();

        Assert.True(formatted);
        Assert.True(formattedAgain);
        Assert.Equal(expected, mainView.SourceEditor.Text?.ToString());
    }

    [Fact]
    public void FormatSource_Does_Not_Require_Semicolon_Before_End()
    {
        var mainView = new IdeMainView();
        mainView.SourceEditor.Text = "begin x:=1 end.";

        var formatted = mainView.FormatSource();
        var formattedText = mainView.SourceEditor.Text?.ToString() ?? string.Empty;
        var expected = string.Join(
            Environment.NewLine,
            [
                "begin",
                "  x := 1",
                "end."
            ]);

        Assert.True(formatted);
        Assert.Equal(expected, formattedText);
        Assert.DoesNotContain("1;", formattedText);
    }

    [Fact]
    public void FormatSource_Indents_Procedure_Header_In_Containing_Begin_End_Block()
    {
        var mainView = new IdeMainView();
        mainView.SourceEditor.Text = "procedure p;begin x:=2 end;begin call p end.";

        var formatted = mainView.FormatSource();
        var formattedText = mainView.SourceEditor.Text?.ToString() ?? string.Empty;
        var expected = string.Join(
            Environment.NewLine,
            [
                "procedure p;",
                "  begin",
                "    x := 2",
                "  end;",
                "begin",
                "  call p",
                "end."
            ]);

        Assert.True(formatted);
        Assert.Equal(expected, formattedText);
    }

    [Fact]
    public void FormatSource_Respects_Ebnf_Block_Nesting_For_Procedure_Declarations()
    {
        var mainView = new IdeMainView();
        mainView.SourceEditor.Text = "const a=10,b=20;var x,y,z;procedure pa;const c=30,d=40;var m;procedure pc;begin ! m := c*d;end;begin call pc;x:=a end;procedure pb;begin y:=b end;begin call pa;call pb;z:=x+y;! z end.";

        var formatted = mainView.FormatSource();
        var formattedText = mainView.SourceEditor.Text?.ToString() ?? string.Empty;
        var expected = string.Join(
            Environment.NewLine,
            [
                "const a = 10, b = 20;",
                "var x, y, z;",
                "procedure pa;",
                "  const c = 30, d = 40;",
                "  var m;",
                "  procedure pc;",
                "    begin",
                "      ! m := c * d;",
                "    end;",
                "  begin",
                "    call pc;",
                "    x := a",
                "  end;",
                "procedure pb;",
                "  begin",
                "    y := b",
                "  end;",
                "begin",
                "  call pa;",
                "  call pb;",
                "  z := x + y;",
                "  ! z",
                "end."
            ]);

        Assert.True(formatted);
        Assert.Equal(expected, formattedText);
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

    [Fact]
    public void FileDialogFilters_For_Export_Expose_Asm_And_Cod()
    {
        var asmFilters = TerminalGuiIdeFileDialogService.CreateExportAsmAllowedTypes();
        var codFilters = TerminalGuiIdeFileDialogService.CreateExportCodAllowedTypes();

        Assert.Equal(2, asmFilters.Count);
        Assert.True(asmFilters[0].IsAllowed("programm.asm"));
        Assert.False(asmFilters[0].IsAllowed("programm.cod"));

        Assert.Equal(2, codFilters.Count);
        Assert.True(codFilters[0].IsAllowed("programm.cod"));
        Assert.False(codFilters[0].IsAllowed("programm.asm"));
    }

    [Fact]
    public void CompilerSettingsState_ResetToDefaults_Uses_CompilerOptionsDefault_And_Derived_MaxNumberDigits()
    {
        var state = new IdeCompilerSettingsDialogState(
            new CompilerOptions(Pl0Dialect.Classic, 4, 2000, 12, 14, 120, 220),
            IdeCodeDisplayMode.Assembler);
        var applied = state.TryApplyValues(4, 2000, 12, 120, 220, out _);

        state.ResetToDefaults();

        Assert.True(applied);
        Assert.Equal(CompilerOptions.Default.Dialect, state.Options.Dialect);
        Assert.Equal(CompilerOptions.Default.MaxLevel, state.Options.MaxLevel);
        Assert.Equal(CompilerOptions.Default.MaxAddress, state.Options.MaxAddress);
        Assert.Equal(CompilerOptions.Default.MaxIdentifierLength, state.Options.MaxIdentifierLength);
        Assert.Equal(CompilerOptions.Default.MaxSymbolCount, state.Options.MaxSymbolCount);
        Assert.Equal(CompilerOptions.Default.MaxCodeLength, state.Options.MaxCodeLength);
        Assert.Equal(IdeCompilerOptionsRules.MaxNumberDigitsExtended, state.Options.MaxNumberDigits);
        Assert.Equal(IdeCodeDisplayMode.Assembler, state.CodeDisplayMode);
    }

    [Fact]
    public void WindowTitles_Use_Code_Display_Mode_After_File_Open()
    {
        var sourcePath = Path.Combine("/tmp", "unterordner", "demo.pl0");
        var mainView = new IdeMainView(
            new StubIdeFileDialogService(openPath: sourcePath),
            new StubIdeFileStorage(contentByPath: new Dictionary<string, string>
            {
                [sourcePath] = "begin end."
            }),
            new StubCompilerSettingsDialogService([
                new IdeCompilerSettingsDialogResult(
                    CompilerOptions.Default,
                    IdeCodeDisplayMode.Assembler)
            ]));

        var opened = mainView.OpenSourceFile();
        var changed = mainView.OpenCompilerSettingsDialog();

        Assert.True(opened);
        Assert.True(changed);
        Assert.Equal("Assembler-Code: demo.pl0", mainView.PCodeWindowTitle);
    }

    private sealed class StubIdeFileDialogService(string? openPath = null, string? savePath = null, string? exportPath = null) : IIdeFileDialogService
    {
        public string? ShowOpenDialog()
        {
            return openPath;
        }

        public string? ShowSaveDialog(string? currentPath)
        {
            return savePath;
        }

        public string? ShowExportDialog(IdeEmitMode mode, string? suggestedPath)
        {
            return exportPath;
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

    private static string GetInstructionTokenFromFirstLine(string listingWithLineNumbers)
    {
        var firstLine = listingWithLineNumbers.Replace("\r\n", "\n", StringComparison.Ordinal)
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .First();
        var separatorIndex = firstLine.IndexOf(':', StringComparison.Ordinal);
        return firstLine[(separatorIndex + 1)..].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
    }

    private sealed class StubCompilerSettingsDialogService(IEnumerable<IdeCompilerSettingsDialogResult?> optionsToReturn) : IIdeCompilerSettingsDialogService
    {
        private readonly Queue<IdeCompilerSettingsDialogResult?> queuedOptions = new(optionsToReturn);

        public IdeCompilerSettingsDialogResult? ShowCompilerSettingsDialog(CompilerOptions currentOptions, IdeCodeDisplayMode currentCodeDisplayMode)
        {
            return queuedOptions.Count > 0 ? queuedOptions.Dequeue() : null;
        }
    }

    private sealed class StubIdeRuntimeDialogService(IEnumerable<int?> values) : IIdeRuntimeDialogService
    {
        private readonly Queue<int?> values = new(values);

        internal int ReadCount { get; private set; }

        public int? ReadInt(string prompt)
        {
            ReadCount++;
            return values.Count > 0 ? values.Dequeue() : null;
        }
    }
}
