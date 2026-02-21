using Pl0.Core;
using Pl0.Vm;
using Terminal.Gui;

namespace Pl0.Ide;

internal sealed class IdeMainView : Toplevel
{
    private static readonly Action NoOp = static () => { };
    private const string SourceWindowBaseTitle = "Quellcode";
    private const string PCodeWindowBaseTitle = "P-Code";
    private const string AssemblerWindowBaseTitle = "Assembler-Code";
    private const string RuntimeOutputWindowBaseTitle = "Ausgabe";
    private readonly Pl0Compiler compiler = new();
    private readonly VirtualMachine virtualMachine = new();
    private readonly IIdeFileDialogService fileDialogs;
    private readonly IIdeFileStorage fileStorage;
    private readonly IIdeCompilerSettingsDialogService compilerSettingsDialog;
    private readonly IIdeRuntimeDialogService runtimeDialogService;
    private readonly Window sourceWindow;
    private readonly Window pCodeWindow;
    private readonly Pl0SourceEditorView sourceEditor;
    private readonly TextView pCodeOutput;
    private readonly TextView messagesOutput;
    private readonly TextView runtimeOutput;
    private CompilationResult? lastCompilationResult;
    private string? currentSourcePath;
    private CompilerOptions currentCompilerOptions = IdeCompilerOptionsRules.GetResetDefaults();
    private IdeCodeDisplayMode currentCodeDisplayMode = IdeCodeDisplayMode.Assembler;

    public IdeMainView() : this(
        new TerminalGuiIdeFileDialogService(),
        new PhysicalIdeFileStorage(),
        new TerminalGuiIdeCompilerSettingsDialogService(),
        new TerminalGuiIdeRuntimeDialogService())
    {
    }

    internal IdeMainView(IIdeFileDialogService fileDialogs, IIdeFileStorage fileStorage)
        : this(fileDialogs, fileStorage, new TerminalGuiIdeCompilerSettingsDialogService(), new TerminalGuiIdeRuntimeDialogService())
    {
    }

    internal IdeMainView(IIdeFileDialogService fileDialogs, IIdeFileStorage fileStorage, IIdeCompilerSettingsDialogService compilerSettingsDialog)
        : this(fileDialogs, fileStorage, compilerSettingsDialog, new TerminalGuiIdeRuntimeDialogService())
    {
    }

    internal IdeMainView(
        IIdeFileDialogService fileDialogs,
        IIdeFileStorage fileStorage,
        IIdeCompilerSettingsDialogService compilerSettingsDialog,
        IIdeRuntimeDialogService runtimeDialogService)
    {
        this.fileDialogs = fileDialogs;
        this.fileStorage = fileStorage;
        this.compilerSettingsDialog = compilerSettingsDialog;
        this.runtimeDialogService = runtimeDialogService;

        var menuBar = CreateMenuBar();
        Add(menuBar);

        sourceWindow = new Window
        {
            Title = SourceWindowBaseTitle,
            X = 0,
            Y = Pos.Bottom(menuBar),
            Width = Dim.Percent(70),
            Height = Dim.Percent(70)
        };
        sourceEditor = CreateSourceEditor();
        sourceWindow.Add(sourceEditor);

        pCodeWindow = new Window
        {
            Title = GetCodeWindowBaseTitle(),
            X = Pos.Right(sourceWindow),
            Y = Pos.Bottom(menuBar),
            Width = Dim.Fill(),
            Height = Dim.Percent(70)
        };
        pCodeOutput = CreateReadOnlyOutputView();
        pCodeWindow.Add(pCodeOutput);

        var messagesWindow = new Window
        {
            Title = "Meldungen",
            X = 0,
            Y = Pos.Bottom(sourceWindow),
            Width = Dim.Percent(65),
            Height = Dim.Fill()
        };
        messagesOutput = CreateReadOnlyOutputView();
        messagesWindow.Add(messagesOutput);

        var runtimeOutputWindow = new Window
        {
            Title = RuntimeOutputWindowBaseTitle,
            X = Pos.Right(messagesWindow),
            Y = Pos.Bottom(sourceWindow),
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        runtimeOutput = CreateReadOnlyOutputView();
        runtimeOutputWindow.Add(runtimeOutput);

        Add(sourceWindow, pCodeWindow, messagesWindow, runtimeOutputWindow);
    }

    internal Pl0SourceEditorView SourceEditor => sourceEditor;
    internal TextView PCodeOutput => pCodeOutput;
    internal TextView MessagesOutput => messagesOutput;
    internal TextView RuntimeOutput => runtimeOutput;
    internal CompilationResult? LastCompilationResult => lastCompilationResult;
    internal string? CurrentSourcePath => currentSourcePath;
    internal string SourceWindowTitle => sourceWindow.Title?.ToString() ?? string.Empty;
    internal string PCodeWindowTitle => pCodeWindow.Title?.ToString() ?? string.Empty;
    internal CompilerOptions CurrentCompilerOptions => currentCompilerOptions;
    internal IdeCodeDisplayMode CurrentCodeDisplayMode => currentCodeDisplayMode;

    internal void CreateNewSourceFile()
    {
        sourceEditor.Text = string.Empty;
        currentSourcePath = null;
        pCodeOutput.Text = string.Empty;
        runtimeOutput.Text = string.Empty;
        UpdateDocumentTitles();
        messagesOutput.Text = "Neue Datei erstellt.";
    }

    internal bool OpenSourceFile()
    {
        var selectedPath = fileDialogs.ShowOpenDialog();
        if (string.IsNullOrWhiteSpace(selectedPath))
        {
            return false;
        }

        var fullPath = Path.GetFullPath(selectedPath);
        if (!HasPl0Extension(fullPath))
        {
            messagesOutput.Text = "Nur Dateien mit Endung .pl0 koennen geladen werden.";
            return false;
        }

        try
        {
            sourceEditor.Text = fileStorage.ReadAllText(fullPath);
            currentSourcePath = fullPath;
            UpdateDocumentTitles();
            messagesOutput.Text = $"Datei geladen: {fullPath}";
            return true;
        }
        catch (Exception ex)
        {
            messagesOutput.Text = $"Datei konnte nicht geladen werden: {ex.Message}";
            return false;
        }
    }

    internal bool SaveSourceFile()
    {
        var selectedPath = fileDialogs.ShowSaveDialog(currentSourcePath);
        if (string.IsNullOrWhiteSpace(selectedPath))
        {
            return false;
        }

        var targetPath = EnsurePl0Extension(Path.GetFullPath(selectedPath));

        try
        {
            fileStorage.WriteAllText(targetPath, sourceEditor.Text?.ToString() ?? string.Empty);
            currentSourcePath = targetPath;
            UpdateDocumentTitles();
            messagesOutput.Text = $"Datei gespeichert: {targetPath}";
            return true;
        }
        catch (Exception ex)
        {
            messagesOutput.Text = $"Datei konnte nicht gespeichert werden: {ex.Message}";
            return false;
        }
    }

    internal bool FormatSource()
    {
        var source = sourceEditor.Text?.ToString() ?? string.Empty;
        if (!Pl0SourceFormatter.TryFormat(source, out var formattedSource, currentCompilerOptions))
        {
            messagesOutput.Text = "Formatierung uebersprungen: Quelltext enthaelt Fehler.";
            return false;
        }

        sourceEditor.Text = formattedSource;
        messagesOutput.Text = "Quelltext formatiert.";
        return true;
    }

    internal CompilationResult CompileSource()
    {
        var source = SourceEditor.Text?.ToString() ?? string.Empty;
        var result = compiler.Compile(source, currentCompilerOptions);

        lastCompilationResult = result;
        RenderCompilationResult(result);
        return result;
    }

    internal bool OpenCompilerSettingsDialog()
    {
        var dialogResult = compilerSettingsDialog.ShowCompilerSettingsDialog(currentCompilerOptions, currentCodeDisplayMode);
        if (dialogResult is null)
        {
            return false;
        }

        if (!IdeCompilerOptionsRules.TryValidateAndNormalize(dialogResult.CompilerOptions, out var normalized, out var validationError))
        {
            messagesOutput.Text = $"Compiler-Einstellungen ungueltig: {validationError}";
            return false;
        }

        currentCompilerOptions = normalized;
        currentCodeDisplayMode = dialogResult.CodeDisplayMode;
        UpdateDocumentTitles();
        if (lastCompilationResult?.Success == true)
        {
            RenderCompilationResult(lastCompilationResult);
        }

        messagesOutput.Text = $"Compiler-Einstellungen aktualisiert: Dialekt {normalized.Dialect}, MaxNumberDigits {normalized.MaxNumberDigits}, Anzeige {GetCodeWindowBaseTitle()}.";
        return true;
    }

    private MenuBar CreateMenuBar()
    {
        return new MenuBar
        {
            Menus =
            [
                new MenuBarItem("_Datei",
                [
                    new MenuItem("_Neu", string.Empty, CreateNewSourceFileFromMenu, () => true, null, default),
                    new MenuItem("_Oeffnen", string.Empty, OpenSourceFileFromMenu, () => true, null, default),
                    new MenuItem("_Speichern", string.Empty, SaveSourceFileFromMenu, () => true, null, default),
                    new MenuItem("_Beenden", string.Empty, NoOp, () => true, null, default)
                ], null),
                new MenuBarItem("_Bearbeiten",
                [
                    new MenuItem("_Formatieren", string.Empty, FormatSourceFromMenu, () => true, null, default)
                ], null),
                new MenuBarItem("_Kompilieren",
                [
                    new MenuItem("_Einstellungen", string.Empty, OpenCompilerSettingsDialogFromMenu, () => true, null, default),
                    new MenuItem("_Build", string.Empty, CompileSourceFromMenu, () => true, null, default),
                    new MenuItem("Export _Asm", string.Empty, ExportAsmFromMenu, () => true, null, default),
                    new MenuItem("Export _Cod", string.Empty, ExportCodFromMenu, () => true, null, default)
                ], null),
                new MenuBarItem("_Ausfuehren",
                [
                    new MenuItem("Kompilieren _und Run", string.Empty, CompileAndRunFromMenu, () => true, null, default),
                    new MenuItem("_Run", string.Empty, RunCompiledCodeFromMenu, () => true, null, default)
                ], null),
                new MenuBarItem("_Debug",
                [
                    new MenuItem("_Step", string.Empty, NoOp, () => true, null, default)
                ], null),
                new MenuBarItem("_Hilfe",
                [
                    new MenuItem("_Bedienung", string.Empty, NoOp, () => true, null, default),
                    new MenuItem("_PL/0-Sprache", string.Empty, NoOp, () => true, null, default)
                ], null)
            ]
        };
    }

    private void CompileSourceFromMenu()
    {
        _ = CompileSource();
    }

    private void OpenCompilerSettingsDialogFromMenu()
    {
        _ = OpenCompilerSettingsDialog();
    }

    private void ExportAsmFromMenu()
    {
        _ = ExportCompiledCode(IdeEmitMode.Asm);
    }

    private void ExportCodFromMenu()
    {
        _ = ExportCompiledCode(IdeEmitMode.Cod);
    }

    private void RunCompiledCodeFromMenu()
    {
        _ = RunCompiledCode();
    }

    private void CompileAndRunFromMenu()
    {
        _ = CompileAndRun();
    }

    private void CreateNewSourceFileFromMenu()
    {
        CreateNewSourceFile();
    }

    private void OpenSourceFileFromMenu()
    {
        _ = OpenSourceFile();
    }

    private void SaveSourceFileFromMenu()
    {
        _ = SaveSourceFile();
    }

    private void FormatSourceFromMenu()
    {
        _ = FormatSource();
    }

    private static bool HasPl0Extension(string path)
    {
        return string.Equals(Path.GetExtension(path), ".pl0", StringComparison.OrdinalIgnoreCase);
    }

    private static string EnsurePl0Extension(string path)
    {
        return HasPl0Extension(path) ? path : Path.ChangeExtension(path, ".pl0");
    }

    internal bool ExportCompiledCode(IdeEmitMode mode)
    {
        if (lastCompilationResult is null || !lastCompilationResult.Success)
        {
            messagesOutput.Text = "Export nicht moeglich: zuerst erfolgreich kompilieren.";
            return false;
        }

        var suggestedPath = BuildSuggestedExportPath(mode);
        var selectedPath = fileDialogs.ShowExportDialog(mode, suggestedPath);
        if (string.IsNullOrWhiteSpace(selectedPath))
        {
            return false;
        }

        var targetPath = NormalizeExportPath(Path.GetFullPath(selectedPath), mode);
        var payload = mode == IdeEmitMode.Cod
            ? PCodeSerializer.ToCod(lastCompilationResult.Instructions)
            : PCodeSerializer.ToAsm(lastCompilationResult.Instructions);

        try
        {
            fileStorage.WriteAllText(targetPath, payload);
            messagesOutput.Text = $"Export erfolgreich: {targetPath}";
            return true;
        }
        catch (Exception ex)
        {
            messagesOutput.Text = $"Export fehlgeschlagen: {ex.Message}";
            return false;
        }
    }

    internal bool RunCompiledCode()
    {
        if (lastCompilationResult is null || !lastCompilationResult.Success)
        {
            messagesOutput.Text = "Ausfuehrung nicht moeglich: zuerst erfolgreich kompilieren.";
            return false;
        }

        runtimeOutput.Text = string.Empty;
        var io = new IdeRuntimeIo(
            () => runtimeDialogService.ReadInt("Bitte Ganzzahl fuer die Laufzeiteingabe eingeben:"),
            AppendRuntimeOutputValue);

        var vmResult = virtualMachine.Run(lastCompilationResult.Instructions, io);
        if (vmResult.Success)
        {
            messagesOutput.Text = "Ausfuehrung erfolgreich.";
            return true;
        }

        messagesOutput.Text = FormatVmDiagnostics(vmResult.Diagnostics);
        return false;
    }

    internal bool CompileAndRun()
    {
        var compileResult = CompileSource();
        return compileResult.Success && RunCompiledCode();
    }

    private static Pl0SourceEditorView CreateSourceEditor()
    {
        return new Pl0SourceEditorView
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
    }

    private static TextView CreateReadOnlyOutputView()
    {
        return new TextView
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            Multiline = true,
            ReadOnly = true,
            Text = string.Empty
        };
    }

    private void UpdateDocumentTitles()
    {
        var codeWindowBaseTitle = GetCodeWindowBaseTitle();
        if (string.IsNullOrWhiteSpace(currentSourcePath))
        {
            sourceWindow.Title = SourceWindowBaseTitle;
            pCodeWindow.Title = codeWindowBaseTitle;
            return;
        }

        var fileName = Path.GetFileName(currentSourcePath);
        sourceWindow.Title = $"{SourceWindowBaseTitle}: {fileName}";
        pCodeWindow.Title = $"{codeWindowBaseTitle}: {fileName}";
    }

    private void RenderCompilationResult(CompilationResult result)
    {
        if (result.Success)
        {
            var listing = currentCodeDisplayMode == IdeCodeDisplayMode.PCode
                ? PCodeSerializer.ToCod(result.Instructions)
                : PCodeSerializer.ToAsm(result.Instructions);
            pCodeOutput.Text = AddLineNumbers(listing);
            messagesOutput.Text = $"Kompilierung erfolgreich ({result.Instructions.Count} Instruktionen).";
            return;
        }

        pCodeOutput.Text = string.Empty;
        messagesOutput.Text = FormatDiagnostics(result.Diagnostics);
    }

    private static string FormatDiagnostics(IReadOnlyList<CompilerDiagnostic> diagnostics)
    {
        if (diagnostics.Count == 0)
        {
            return "Keine Diagnosen.";
        }

        return string.Join(
            Environment.NewLine,
            diagnostics.Select(d =>
                $"E{d.Code} (Zeile {d.Position.Line}, Spalte {d.Position.Column}): {d.Message}"));
    }

    private static string FormatVmDiagnostics(IReadOnlyList<VmDiagnostic> diagnostics)
    {
        if (diagnostics.Count == 0)
        {
            return "Keine Laufzeitdiagnosen.";
        }

        return string.Join(
            Environment.NewLine,
            diagnostics.Select(d => $"R{d.Code}: {d.Message}"));
    }

    private string GetCodeWindowBaseTitle()
    {
        return currentCodeDisplayMode == IdeCodeDisplayMode.PCode ? PCodeWindowBaseTitle : AssemblerWindowBaseTitle;
    }

    private static string AddLineNumbers(string listing)
    {
        if (string.IsNullOrWhiteSpace(listing))
        {
            return string.Empty;
        }

        var lines = listing.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
        return string.Join(
            Environment.NewLine,
            lines.Select((line, index) => $"{index:D4}: {line}"));
    }

    private string BuildSuggestedExportPath(IdeEmitMode mode)
    {
        var extension = mode == IdeEmitMode.Cod ? ".cod" : ".asm";
        if (!string.IsNullOrWhiteSpace(currentSourcePath))
        {
            return Path.ChangeExtension(currentSourcePath, extension);
        }

        return Path.GetFullPath($"output{extension}");
    }

    private static string NormalizeExportPath(string path, IdeEmitMode mode)
    {
        if (mode == IdeEmitMode.Cod)
        {
            return Path.ChangeExtension(path, ".cod");
        }

        return string.IsNullOrEmpty(Path.GetExtension(path))
            ? Path.ChangeExtension(path, ".asm")
            : path;
    }

    private void AppendRuntimeOutputValue(int value)
    {
        var line = value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        var current = runtimeOutput.Text?.ToString() ?? string.Empty;
        runtimeOutput.Text = string.IsNullOrEmpty(current)
            ? line
            : $"{current}{Environment.NewLine}{line}";
    }
}
