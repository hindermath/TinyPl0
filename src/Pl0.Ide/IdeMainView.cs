using Pl0.Core;
using Pl0.Vm;
using Terminal.Gui;
using System.Globalization;
using System.Text;

namespace Pl0.Ide;

internal sealed class IdeMainView : Toplevel
{
    private static readonly Action NoOp = static () => { };
    private const string SourceWindowBaseTitle = "Quellcode";
    private const string PCodeWindowBaseTitle = "P-Code";
    private const string AssemblerWindowBaseTitle = "Assembler-Code";
    private const string RuntimeOutputWindowBaseTitle = "Ausgabe";
    private const string DebugWindowBaseTitle = "Debug";
    private readonly Pl0Compiler compiler = new();
    private readonly VirtualMachine virtualMachine = new();
    private readonly SteppableVirtualMachine steppableVirtualMachine = new();
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
    private readonly IdeDebugView debugOutput;
    private CompilationResult? lastCompilationResult;
    private VmStepResult? lastDebugStepResult;
    private string? currentSourcePath;
    private CompilerOptions currentCompilerOptions = IdeCompilerOptionsRules.GetResetDefaults();
    private IdeCodeDisplayMode currentCodeDisplayMode = IdeCodeDisplayMode.Assembler;
    private bool isDebugSessionActive;

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
            Width = Dim.Percent(15),
            Height = Dim.Percent(70)
        };
        pCodeOutput = CreateReadOnlyOutputView();
        pCodeWindow.Add(pCodeOutput);

        var debugWindow = new Window
        {
            Title = DebugWindowBaseTitle,
            X = Pos.Right(pCodeWindow),
            Y = Pos.Bottom(menuBar),
            Width = Dim.Fill(),
            Height = Dim.Percent(70)
        };
        debugOutput = CreateDebugOutputView();
        debugWindow.Add(debugOutput);

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

        Add(sourceWindow, pCodeWindow, messagesWindow, runtimeOutputWindow, debugWindow);
    }

    internal Pl0SourceEditorView SourceEditor => sourceEditor;
    internal TextView PCodeOutput => pCodeOutput;
    internal TextView MessagesOutput => messagesOutput;
    internal TextView RuntimeOutput => runtimeOutput;
    internal TextView DebugOutput => debugOutput;
    internal CompilationResult? LastCompilationResult => lastCompilationResult;
    internal string? CurrentSourcePath => currentSourcePath;
    internal string SourceWindowTitle => sourceWindow.Title?.ToString() ?? string.Empty;
    internal string PCodeWindowTitle => pCodeWindow.Title?.ToString() ?? string.Empty;
    internal CompilerOptions CurrentCompilerOptions => currentCompilerOptions;
    internal IdeCodeDisplayMode CurrentCodeDisplayMode => currentCodeDisplayMode;
    internal bool IsDebugSessionActive => isDebugSessionActive;

    internal void CreateNewSourceFile()
    {
        sourceEditor.Text = string.Empty;
        currentSourcePath = null;
        pCodeOutput.Text = string.Empty;
        runtimeOutput.Text = string.Empty;
        debugOutput.Text = string.Empty;
        isDebugSessionActive = false;
        lastDebugStepResult = null;
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
            debugOutput.Text = string.Empty;
            isDebugSessionActive = false;
            lastDebugStepResult = null;
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

        isDebugSessionActive = false;
        debugOutput.Text = string.Empty;
        lastDebugStepResult = null;
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
            if (lastDebugStepResult is not null)
            {
                RenderDebugState(lastDebugStepResult);
            }
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
                    new MenuItem("_Step", string.Empty, StepDebugFromMenu, () => true, null, default),
                    new MenuItem("_Abbrechen", string.Empty, AbortDebugFromMenu, () => true, null, default)
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

    private void StepDebugFromMenu()
    {
        _ = StepDebug();
    }

    private void AbortDebugFromMenu()
    {
        AbortDebug();
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

    internal VmStepResult? StepDebug()
    {
        if (lastCompilationResult is null || !lastCompilationResult.Success)
        {
            messagesOutput.Text = "Debug nicht moeglich: zuerst erfolgreich kompilieren.";
            return null;
        }

        if (!isDebugSessionActive)
        {
            runtimeOutput.Text = string.Empty;
            var io = new IdeRuntimeIo(
                () => runtimeDialogService.ReadInt("Bitte Ganzzahl fuer die Laufzeiteingabe eingeben:"),
                AppendRuntimeOutputValue);
            steppableVirtualMachine.Initialize(lastCompilationResult.Instructions, io);
            isDebugSessionActive = true;
        }

        var stepResult = steppableVirtualMachine.Step();
        lastDebugStepResult = stepResult;
        RenderDebugState(stepResult);
        RenderCodeOutput(lastCompilationResult.Instructions, stepResult.State.P);

        switch (stepResult.Status)
        {
            case VmStepStatus.Running:
                messagesOutput.Text = $"Debug-Step ausgefuehrt (P={stepResult.State.P}, B={stepResult.State.B}, T={stepResult.State.T}).";
                break;
            case VmStepStatus.Halted:
                isDebugSessionActive = false;
                messagesOutput.Text = "Debug-Ausfuehrung beendet.";
                break;
            case VmStepStatus.Error:
                isDebugSessionActive = false;
                messagesOutput.Text = FormatVmDiagnostics(stepResult.Diagnostics);
                break;
        }

        return stepResult;
    }

    internal bool AbortDebug()
    {
        if (!isDebugSessionActive)
        {
            messagesOutput.Text = "Keine laufende Debug-Ausfuehrung zum Abbrechen.";
            return false;
        }

        isDebugSessionActive = false;
        messagesOutput.Text = "Debug-Ausfuehrung abgebrochen. Letzter VM-Zustand bleibt sichtbar.";
        return true;
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

    private static IdeDebugView CreateDebugOutputView()
    {
        return new IdeDebugView
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
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
            RenderCodeOutput(result.Instructions, null);
            messagesOutput.Text = $"Kompilierung erfolgreich ({result.Instructions.Count} Instruktionen).";
            return;
        }

        pCodeOutput.Text = string.Empty;
        messagesOutput.Text = FormatDiagnostics(result.Diagnostics);
    }

    private void RenderCodeOutput(IReadOnlyList<Instruction> instructions, int? pointer)
    {
        var listing = currentCodeDisplayMode == IdeCodeDisplayMode.PCode
            ? PCodeSerializer.ToCod(instructions)
            : PCodeSerializer.ToAsm(instructions);
        pCodeOutput.Text = AddLineNumbers(listing, pointer);
    }

    private void RenderDebugState(VmStepResult stepResult)
    {
        var state = stepResult.State;
        var lines = new List<string>();
        var spans = new List<IdeDebugHighlightSpan>();

        lines.Add($"Status: {stepResult.Status}");

        var registerRow = lines.Count;
        var registerLine = BuildRegisterLine(state, registerRow, spans);
        lines.Add(registerLine);

        lines.Add($"Instruktion: {FormatCurrentInstruction(state.CurrentInstruction, currentCodeDisplayMode)}");
        lines.Add("Stack:");

        if (state.T < 1)
        {
            lines.Add("  <leer>");
        }
        else
        {
            for (var index = 1; index <= state.T && index < state.Stack.Length; index++)
            {
                var stackRow = lines.Count;
                var stackLine = BuildStackLine(state, index, stackRow, spans);
                lines.Add(stackLine);
            }
        }

        if (stepResult.Diagnostics.Count > 0)
        {
            lines.Add("Diagnosen:");
            foreach (var diagnostic in stepResult.Diagnostics)
            {
                lines.Add($"  R{diagnostic.Code}: {diagnostic.Message}");
            }
        }

        debugOutput.SetDebugContent(string.Join(Environment.NewLine, lines), spans);
    }

    private static string BuildRegisterLine(VmState state, int row, ICollection<IdeDebugHighlightSpan> spans)
    {
        var displayBasePointer = ToDisplayStackIndex(state.B);
        var displayTopPointer = ToDisplayStackIndex(state.T);

        var builder = new StringBuilder();
        builder.Append("Register: IP=");
        builder.Append(state.P.ToString(CultureInfo.InvariantCulture));
        builder.Append(", BP=");
        var baseStart = builder.Length;
        var baseText = displayBasePointer.ToString(CultureInfo.InvariantCulture);
        builder.Append(baseText);
        builder.Append(", SP=");
        var topStart = builder.Length;
        var topText = displayTopPointer.ToString(CultureInfo.InvariantCulture);
        builder.Append(topText);

        spans.Add(new IdeDebugHighlightSpan(row, baseStart, baseText.Length, IdeDebugHighlightKind.BasePointer));
        spans.Add(new IdeDebugHighlightSpan(row, topStart, topText.Length, IdeDebugHighlightKind.StackPointer));

        return builder.ToString();
    }

    private static int ToDisplayStackIndex(int internalIndex)
    {
        return Math.Max(0, internalIndex - 1);
    }

    private static string BuildStackLine(VmState state, int stackIndex, int row, ICollection<IdeDebugHighlightSpan> spans)
    {
        var isBasePointerLine = state.B == stackIndex;
        var isTopPointerLine = state.T == stackIndex;

        var builder = new StringBuilder();
        builder.Append("  BP");
        var baseIndicatorColumn = builder.Length;
        builder.Append(isBasePointerLine ? ">>" : "  ");
        builder.Append(" SP");
        var topIndicatorColumn = builder.Length;
        builder.Append(isTopPointerLine ? ">>" : "  ");
        builder.Append(" [");
        builder.Append((stackIndex - 1).ToString("D3", CultureInfo.InvariantCulture));
        builder.Append("] = ");
        builder.Append(state.Stack[stackIndex].ToString(CultureInfo.InvariantCulture));

        if (isBasePointerLine)
        {
            spans.Add(new IdeDebugHighlightSpan(row, baseIndicatorColumn, 2, IdeDebugHighlightKind.BasePointer));
        }

        if (isTopPointerLine)
        {
            spans.Add(new IdeDebugHighlightSpan(row, topIndicatorColumn, 2, IdeDebugHighlightKind.StackPointer));
        }

        return builder.ToString();
    }

    private static string FormatCurrentInstruction(Instruction? instruction, IdeCodeDisplayMode displayMode)
    {
        if (!instruction.HasValue)
        {
            return "<keine>";
        }

        if (displayMode == IdeCodeDisplayMode.PCode)
        {
            return $"{(int)instruction.Value.Op} {instruction.Value.Level} {instruction.Value.Argument}";
        }

        var mnemonic = instruction.Value.Op.ToString().ToLowerInvariant();
        return $"{mnemonic} {instruction.Value.Level} {instruction.Value.Argument}";
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

    private static string AddLineNumbers(string listing, int? pointer)
    {
        if (string.IsNullOrWhiteSpace(listing))
        {
            return string.Empty;
        }

        var lines = listing.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
        return string.Join(
            Environment.NewLine,
            lines.Select((line, index) =>
            {
                var marker = pointer.HasValue && pointer.Value == index ? ">> " : "   ";
                return $"{marker}{index:D4}: {line}";
            }));
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
