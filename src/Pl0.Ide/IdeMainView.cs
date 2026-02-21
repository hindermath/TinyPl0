using Pl0.Core;
using Terminal.Gui;

namespace Pl0.Ide;

internal sealed class IdeMainView : Toplevel
{
    private static readonly Action NoOp = static () => { };
    private const string SourceWindowBaseTitle = "Quellcode";
    private const string PCodeWindowBaseTitle = "P-Code";
    private readonly Pl0Compiler compiler = new();
    private readonly IIdeFileDialogService fileDialogs;
    private readonly IIdeFileStorage fileStorage;
    private readonly Window sourceWindow;
    private readonly Window pCodeWindow;
    private readonly Pl0SourceEditorView sourceEditor;
    private readonly TextView pCodeOutput;
    private readonly TextView messagesOutput;
    private CompilationResult? lastCompilationResult;
    private string? currentSourcePath;

    public IdeMainView() : this(new TerminalGuiIdeFileDialogService(), new PhysicalIdeFileStorage())
    {
    }

    internal IdeMainView(IIdeFileDialogService fileDialogs, IIdeFileStorage fileStorage)
    {
        this.fileDialogs = fileDialogs;
        this.fileStorage = fileStorage;

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
            Title = PCodeWindowBaseTitle,
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
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        messagesOutput = CreateReadOnlyOutputView();
        messagesWindow.Add(messagesOutput);

        Add(sourceWindow, pCodeWindow, messagesWindow);
    }

    internal Pl0SourceEditorView SourceEditor => sourceEditor;
    internal TextView PCodeOutput => pCodeOutput;
    internal TextView MessagesOutput => messagesOutput;
    internal CompilationResult? LastCompilationResult => lastCompilationResult;
    internal string? CurrentSourcePath => currentSourcePath;
    internal string SourceWindowTitle => sourceWindow.Title?.ToString() ?? string.Empty;
    internal string PCodeWindowTitle => pCodeWindow.Title?.ToString() ?? string.Empty;

    internal void CreateNewSourceFile()
    {
        sourceEditor.Text = string.Empty;
        currentSourcePath = null;
        pCodeOutput.Text = string.Empty;
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

    internal CompilationResult CompileSource()
    {
        var source = SourceEditor.Text?.ToString() ?? string.Empty;
        var result = compiler.Compile(source, CompilerOptions.Default);

        lastCompilationResult = result;
        RenderCompilationResult(result);
        return result;
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
                    new MenuItem("_Formatieren", string.Empty, NoOp, () => true, null, default)
                ], null),
                new MenuBarItem("_Kompilieren",
                [
                    new MenuItem("_Build", string.Empty, CompileSourceFromMenu, () => true, null, default)
                ], null),
                new MenuBarItem("_Ausfuehren",
                [
                    new MenuItem("_Run", string.Empty, NoOp, () => true, null, default)
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

    private static bool HasPl0Extension(string path)
    {
        return string.Equals(Path.GetExtension(path), ".pl0", StringComparison.OrdinalIgnoreCase);
    }

    private static string EnsurePl0Extension(string path)
    {
        return HasPl0Extension(path) ? path : Path.ChangeExtension(path, ".pl0");
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
        if (string.IsNullOrWhiteSpace(currentSourcePath))
        {
            sourceWindow.Title = SourceWindowBaseTitle;
            pCodeWindow.Title = PCodeWindowBaseTitle;
            return;
        }

        var fileName = Path.GetFileName(currentSourcePath);
        sourceWindow.Title = $"{SourceWindowBaseTitle}: {fileName}";
        pCodeWindow.Title = $"{PCodeWindowBaseTitle}: {fileName}";
    }

    private void RenderCompilationResult(CompilationResult result)
    {
        if (result.Success)
        {
            pCodeOutput.Text = PCodeSerializer.ToAsm(result.Instructions);
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
}
