using Pl0.Core;
using Terminal.Gui;

namespace Pl0.Ide;

internal sealed class IdeMainView : Toplevel
{
    private static readonly Action NoOp = static () => { };
    private readonly Pl0Compiler compiler = new();
    private readonly Pl0SourceEditorView sourceEditor;
    private readonly TextView pCodeOutput;
    private readonly TextView messagesOutput;
    private CompilationResult? lastCompilationResult;

    public IdeMainView()
    {
        var menuBar = CreateMenuBar();
        Add(menuBar);

        var editorWindow = new Window
        {
            Title = "Quellcode",
            X = 0,
            Y = Pos.Bottom(menuBar),
            Width = Dim.Percent(70),
            Height = Dim.Percent(70)
        };
        sourceEditor = CreateSourceEditor();
        editorWindow.Add(sourceEditor);

        var pCodeWindow = new Window
        {
            Title = "P-Code",
            X = Pos.Right(editorWindow),
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
            Y = Pos.Bottom(editorWindow),
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        messagesOutput = CreateReadOnlyOutputView();
        messagesWindow.Add(messagesOutput);

        Add(editorWindow, pCodeWindow, messagesWindow);
    }

    internal Pl0SourceEditorView SourceEditor => sourceEditor;
    internal TextView PCodeOutput => pCodeOutput;
    internal TextView MessagesOutput => messagesOutput;
    internal CompilationResult? LastCompilationResult => lastCompilationResult;

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
                    new MenuItem("_Neu", string.Empty, NoOp, () => true, null, default),
                    new MenuItem("_Oeffnen", string.Empty, NoOp, () => true, null, default),
                    new MenuItem("_Speichern", string.Empty, NoOp, () => true, null, default),
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
