using System.Globalization;
using System.Resources;
using Pl0.Cli.Cli;
using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

/// <summary>
/// Tests für die L10N-Backend-Infrastruktur (001-l10n-backend).
/// Deckt alle ~75 Resource-Keys in EN und DE ab sowie Fallback-Ketten und Extensibility.
/// </summary>
public sealed class L10nTests
{
    // ── Hilfseigenschaften ─────────────────────────────────────────────────

    private static ResourceManager CoreRm =>
        new("Pl0.Core.Pl0CoreMessages", typeof(Pl0Compiler).Assembly);

    private static ResourceManager VmRm =>
        new("Pl0.Vm.Pl0VmMessages", typeof(VirtualMachine).Assembly);

    private static ResourceManager CliRm =>
        new("Pl0.Cli.Pl0CliMessages", typeof(CliOptionsParser).Assembly);

    private static CultureInfo En => CultureInfo.GetCultureInfo("en");
    private static CultureInfo De => CultureInfo.GetCultureInfo("de");

    private static string RepoRoot => FindRepoRoot();

    private static string L10nPl0(string name) =>
        Path.Combine(RepoRoot, "tests", "data", "pl0", "l10n", name);

    private static string FindRepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "TinyPl0.sln")))
            {
                return dir.FullName;
            }

            dir = dir.Parent;
        }

        throw new InvalidOperationException("Repository root not found.");
    }

    private static CompilationResult CompileEn(string source) =>
        new Pl0Compiler().Compile(source, new CompilerOptions(Pl0Dialect.Extended, Language: "en"));

    private static CompilationResult CompileDe(string source) =>
        new Pl0Compiler().Compile(source, new CompilerOptions(Pl0Dialect.Extended, Language: "de"));

    // ── T014: Cli_Help_* EN — CliHelpPrinter.GetUsageLines ────────────────

    [Fact]
    public void Cli_Help_UsageHeader_En()
    {
        var lines = CliHelpPrinter.GetUsageLines("pl0c", CliRm, En);
        Assert.Equal("Usage:", lines[0]);
    }

    [Fact]
    public void Cli_Help_HelpLine_En()
    {
        var lines = CliHelpPrinter.GetUsageLines("pl0c", CliRm, En);
        Assert.Equal("pl0c [-|/]h | [-|/]? | --help", lines[1]);
    }

    [Fact]
    public void Cli_Help_CompileLine_En()
    {
        var lines = CliHelpPrinter.GetUsageLines("pl0c", CliRm, En);
        Assert.Equal("pl0c compile <file.pl0> [--out <file.pcode>] [--emit asm|cod] [--list-code]", lines[2]);
    }

    [Fact]
    public void Cli_Help_RunLine_En()
    {
        var lines = CliHelpPrinter.GetUsageLines("pl0c", CliRm, En);
        Assert.Equal("pl0c run <file.pl0> [--emit asm|cod] [--list-code]", lines[3]);
    }

    [Fact]
    public void Cli_Help_RunPcodeLine_En()
    {
        var lines = CliHelpPrinter.GetUsageLines("pl0c", CliRm, En);
        Assert.Equal("pl0c run-pcode <file.pcode> [--list-code]", lines[4]);
    }

    [Fact]
    public void Cli_Help_RunPcodeDirectLine_En()
    {
        var lines = CliHelpPrinter.GetUsageLines("pl0c", CliRm, En);
        Assert.Equal("pl0c <file.pcode> | <file.cod> [run P-Code directly]", lines[5]);
    }

    [Fact]
    public void Cli_Help_ApiLine_En()
    {
        var lines = CliHelpPrinter.GetUsageLines("pl0c", CliRm, En);
        Assert.Equal("pl0c --api", lines[6]);
    }

    [Fact]
    public void Cli_Help_LegacyLine_En()
    {
        var lines = CliHelpPrinter.GetUsageLines("pl0c", CliRm, En);
        Assert.Equal("pl0c <file.pl0> [legacy mode; compile and run]", lines[7]);
    }

    [Fact]
    public void Cli_Help_SwitchesHeader_En()
    {
        var lines = CliHelpPrinter.GetUsageLines("pl0c", CliRm, En);
        Assert.Equal("Switches:", lines[9]);
    }

    [Fact]
    public void Cli_Help_SwitchErrmsg_En()
    {
        var actual = CliRm.GetString("Cli_Help_SwitchErrmsg", En);
        Assert.Equal("  [-|/]errmsg | --errmsg                show long compiler error messages", actual);
    }

    [Fact]
    public void Cli_Help_SwitchWopcod_En()
    {
        var actual = CliRm.GetString("Cli_Help_SwitchWopcod", En);
        Assert.Equal("  [-|/]wopcod | --wopcod                numeric opcodes in listing output", actual);
    }

    [Fact]
    public void Cli_Help_SwitchListCode_En()
    {
        var actual = CliRm.GetString("Cli_Help_SwitchListCode", En);
        Assert.Equal("  --list-code                            output generated/loaded code listing", actual);
    }

    [Fact]
    public void Cli_Help_SwitchApi_En()
    {
        var actual = CliRm.GetString("Cli_Help_SwitchApi", En);
        Assert.Equal("  --api                                  start documentation web server", actual);
    }

    [Fact]
    public void Cli_Help_SwitchConly_En()
    {
        var actual = CliRm.GetString("Cli_Help_SwitchConly", En);
        Assert.Equal("  [-|/]conly | --conly | --compile-only  compile only, do not run VM", actual);
    }

    [Fact]
    public void Cli_Help_SwitchEmitFormat_En()
    {
        var actual = CliRm.GetString("Cli_Help_SwitchEmitFormat", En);
        Assert.Equal("  [-|/]emit {[-|/]asm | [-|/]cod}", actual);
    }

    [Fact]
    public void Cli_Help_SwitchEmit_En()
    {
        var actual = CliRm.GetString("Cli_Help_SwitchEmit", En);
        Assert.Equal("  --emit asm|cod                         output PL/0 code to STDOUT", actual);
    }

    [Fact]
    public void Cli_Help_SwitchOut_En()
    {
        var actual = CliRm.GetString("Cli_Help_SwitchOut", En);
        Assert.Equal("  --out <file>                            output path for 'compile' command", actual);
    }

    [Fact]
    public void Cli_Help_SwitchLang_En()
    {
        var actual = CliRm.GetString("Cli_Help_SwitchLang", En);
        Assert.Equal("  --lang <code>                           set output language (e.g. de, en)", actual);
    }

    // ── T015: Cli_Err_* + Cli_Status_* EN ─────────────────────────────────

    [Fact]
    public void Cli_Err_UnknownSwitch_En()
    {
        var actual = CliRm.GetString("Cli_Err_UnknownSwitch", En);
        Assert.Equal("Unknown switch: '{0}'.", actual);
    }

    [Fact]
    public void Cli_Err_UnexpectedPositional_En()
    {
        var actual = CliRm.GetString("Cli_Err_UnexpectedPositional", En);
        Assert.Equal("Unexpected argument: '{0}'.", actual);
    }

    [Fact]
    public void Cli_Err_MissingValueForOut_En()
    {
        var actual = CliRm.GetString("Cli_Err_MissingValueForOut", En);
        Assert.Equal("Missing value for '--out'.", actual);
    }

    [Fact]
    public void Cli_Err_NoEmitMode_En()
    {
        var actual = CliRm.GetString("Cli_Err_NoEmitMode", En);
        Assert.Equal("No emit mode specified. Use '--emit asm' or '--emit cod'.", actual);
    }

    [Fact]
    public void Cli_Err_ConflictingEmitModes_En()
    {
        var actual = CliRm.GetString("Cli_Err_ConflictingEmitModes", En);
        Assert.Equal("Conflicting emit modes. Specify only one of 'asm' or 'cod'.", actual);
    }

    [Fact]
    public void Cli_Err_UnknownLanguage_En()
    {
        var actual = CliRm.GetString("Cli_Err_UnknownLanguage", En);
        Assert.Equal("Unknown language code '{0}', using fallback 'de'.", actual);
    }

    [Fact]
    public void Cli_Status_CompileSuccess_En()
    {
        var actual = CliRm.GetString("Cli_Status_CompileSuccess", En);
        Assert.Equal("Compilation successful.", actual);
    }

    [Fact]
    public void Cli_Status_CompileError_En()
    {
        var actual = CliRm.GetString("Cli_Status_CompileError", En);
        Assert.Equal("Compilation failed.", actual);
    }

    // ── T016: Fallback-Ketten ──────────────────────────────────────────────

    [Fact]
    public void LangEnUs_FallsBackToEn()
    {
        var result = CompileEn("begin ! x end.");
        var enUsResult = new Pl0Compiler().Compile(
            "begin ! x end.",
            new CompilerOptions(Pl0Dialect.Extended, Language: "en-US"));

        Assert.False(enUsResult.Success);
        Assert.Contains(enUsResult.Diagnostics, d => d.Message.Contains("Undeclared identifier"));
    }

    [Fact]
    public void LangFr_FallsBackToGerman()
    {
        var result = new Pl0Compiler().Compile(
            "begin ! x end.",
            new CompilerOptions(Pl0Dialect.Extended, Language: "fr"));

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Nicht deklarierter Bezeichner"));
    }

    [Fact]
    public void EmptyLanguage_FallsBackToGerman()
    {
        var result = new Pl0Compiler().Compile(
            "begin ! x end.",
            new CompilerOptions(Pl0Dialect.Extended, Language: ""));

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Nicht deklarierter Bezeichner"));
    }

    [Fact]
    public void UnknownLanguage_FallsBackToGerman_WithStderrWarning()
    {
        // Codes die ausschließlich aus Ziffern bestehen sind keine gültigen BCP-47-Tags
        // und werfen CultureNotFoundException auf allen .NET-Plattformen (ICU, NLS).
        const string invalidCode = "123";
        var stderr = new StringWriter();
        var parser = new CliOptionsParser(errorOutput: stderr);
        var result = parser.Parse(["--lang", invalidCode, "file.pl0"]);

        Assert.Equal("de", result.Options.Language);
        Assert.Contains(invalidCode, stderr.ToString());
    }

    // ── T023: Lexer-Keys EN — via Kompilierung ─────────────────────────────

    [Fact]
    public void Lexer_NumberTooManyDigits_En()
    {
        var source = File.ReadAllText(L10nPl0("number_too_many_digits.pl0"));
        var result = CompileEn(source);

        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Number has too many digits"));
    }

    [Fact]
    public void Lexer_NumberTooLarge_En()
    {
        var source = File.ReadAllText(L10nPl0("number_too_large_lexer.pl0"));
        var result = CompileEn(source);

        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Number is too large"));
    }

    [Fact]
    public void Lexer_IdentifierTooLong_En()
    {
        var source = File.ReadAllText(L10nPl0("ident_too_long.pl0"));
        var result = CompileEn(source);

        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Identifier too long"));
    }

    [Fact]
    public void Lexer_UnexpectedColon_En()
    {
        var source = File.ReadAllText(L10nPl0("unexpected_colon.pl0"));
        var result = CompileEn(source);

        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Unexpected ':'"));
    }

    [Fact]
    public void Lexer_UnexpectedChar_En()
    {
        var source = File.ReadAllText(L10nPl0("unexpected_char.pl0"));
        var result = CompileEn(source);

        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Unexpected character"));
    }

    // ── T024: Parser-Keys EN — ResourceManager-Inhaltsvalidierung ──────────

    [Theory]
    [InlineData("Parser_E01_UseEqualNotAssign", "Use '=' instead of ':='.")]
    [InlineData("Parser_E02_NumberAfterEquals", "'=' must be followed by a number.")]
    [InlineData("Parser_E03_EqualAfterIdent", "Identifier must be followed by '='.")]
    [InlineData("Parser_E04_IdentAfterConst", "CONST must be followed by an identifier.")]
    [InlineData("Parser_E04_IdentAfterVar", "VAR must be followed by an identifier.")]
    [InlineData("Parser_E04_IdentAfterProc", "PROCEDURE must be followed by an identifier.")]
    [InlineData("Parser_E04_IdentAfterInput", "An identifier must follow '?'.")]
    [InlineData("Parser_E05_SemiOrComma", "Semicolon or comma missing.")]
    [InlineData("Parser_E09_PeriodExpected", "Period expected.")]
    [InlineData("Parser_E11_UndeclaredIdent", "Undeclared identifier.")]
    [InlineData("Parser_E12_AssignToConst", "Assignment to CONST or PROCEDURE not allowed.")]
    [InlineData("Parser_E12_InputTargetMustBeVar", "Input target must be a variable.")]
    [InlineData("Parser_E13_AssignOpExpected", "Assignment operator ':=' expected.")]
    [InlineData("Parser_E14_CallNeedsIdent", "CALL must be followed by an identifier.")]
    [InlineData("Parser_E15_CallConstOrVar", "Calling a constant or variable is not meaningful.")]
    [InlineData("Parser_E16_ThenExpected", "THEN expected.")]
    [InlineData("Parser_E17_SemiOrEndExpected", "Semicolon or END expected.")]
    [InlineData("Parser_E18_DoExpected", "DO expected.")]
    [InlineData("Parser_E19_IncorrectSymbol", "Incorrect symbol after statement.")]
    [InlineData("Parser_E19_InputNotInClassic", "Input '?' is not available in classic mode.")]
    [InlineData("Parser_E19_OutputNotInClassic", "Output '!' is not available in classic mode.")]
    [InlineData("Parser_E20_RelOpExpected", "Relational operator expected.")]
    [InlineData("Parser_E21_ProcInExpr", "Procedure identifier not allowed in expression.")]
    [InlineData("Parser_E22_RightParenMissing", "Closing parenthesis missing.")]
    [InlineData("Parser_E24_BadExprStart", "Expression cannot start with this symbol.")]
    [InlineData("Parser_E30_NumberTooLarge", "This number is too large.")]
    [InlineData("Parser_E32_NestingTooDeep", "Maximum nesting level exceeded.")]
    [InlineData("Parser_E34_SymbolTableOverflow", "Symbol table full (max {0}).")]
    [InlineData("Parser_E35_ProgramTooLong", "Program too long (max {0} instructions).")]
    [InlineData("Parser_E98_UnexpectedEndOfInput", "Program incomplete: unexpected end of input.")]
    [InlineData("Parser_E99_InvalidLexLevel", "Invalid lexical level reference.")]
    [InlineData("Parser_E99_UnexpectedTermination", "Unexpected end of program.")]
    public void Parser_EN_Key_ReturnsEnglishString(string key, string expected)
    {
        var actual = CoreRm.GetString(key, En);
        Assert.Equal(expected, actual);
    }

    // ── T025: DE-Baseline — Compiler-Diagnosen auf Deutsch ────────────────

    [Fact]
    public void CompilerDiagnostic_De_ContainsGermanMessage()
    {
        var source = File.ReadAllText(L10nPl0("undeclared_ident.pl0"));
        var result = CompileDe(source);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Nicht deklarierter Bezeichner"));
    }

    [Fact]
    public void LexerDiagnostic_De_ContainsGermanMessage()
    {
        var source = File.ReadAllText(L10nPl0("ident_too_long.pl0"));
        var result = CompileDe(source);

        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Bezeichner zu lang"));
    }

    // ── T030: PL/0-auslösbare VM-Keys EN — via Kompilierung + BufferedPl0Io ─

    [Fact]
    public void Vm_DivisionByZero_En()
    {
        var source = File.ReadAllText(L10nPl0("division_by_zero.pl0"));
        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var vmOptions = new VirtualMachineOptions(Language: "en");
        var result = new VirtualMachine().Run(compilation.Instructions, new BufferedPl0Io(), vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Division by zero"));
    }

    [Fact]
    public void Vm_EndOfInput_En()
    {
        var source = File.ReadAllText(L10nPl0("end_of_input.pl0"));
        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var vmOptions = new VirtualMachineOptions(Language: "en");
        var result = new VirtualMachine().Run(compilation.Instructions, new BufferedPl0Io([]), vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("End of input while reading"));
    }

    [Fact]
    public void Vm_InputFormatError_En()
    {
        var source = File.ReadAllText(L10nPl0("end_of_input.pl0"));
        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var vmOptions = new VirtualMachineOptions(Language: "en");
        var result = new VirtualMachine().Run(compilation.Instructions, new FormatExceptionIo(), vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Invalid input format"));
    }

    // ── T031: VM-interne Keys EN — via direkter VirtualMachine-Konstruktion ─

    [Fact]
    public void Vm_IPOutOfRange_En()
    {
        var vmOptions = new VirtualMachineOptions(Language: "en");
        var result = new VirtualMachine().Run([], null, vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Instruction pointer out of range"));
    }

    [Fact]
    public void Vm_StackOverflow_En()
    {
        var program = Enumerable.Repeat(new Instruction(Opcode.Lit, 0, 0), 7).ToArray();
        var vmOptions = new VirtualMachineOptions(StackSize: 5, Language: "en");
        var result = new VirtualMachine().Run(program, null, vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Stack overflow"));
    }

    [Fact]
    public void Vm_StackOverflowInt_En()
    {
        var program = new[] { new Instruction(Opcode.Int, 0, 20) };
        var vmOptions = new VirtualMachineOptions(StackSize: 10, Language: "en");
        var result = new VirtualMachine().Run(program, null, vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Stack overflow on INT"));
    }

    [Fact]
    public void Vm_StackOverflowCallFrame_En()
    {
        var program = new[]
        {
            new Instruction(Opcode.Int, 0, 8),
            new Instruction(Opcode.Cal, 0, 1),
        };
        var vmOptions = new VirtualMachineOptions(StackSize: 10, Language: "en");
        var result = new VirtualMachine().Run(program, null, vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Stack overflow while creating call frame"));
    }

    [Fact]
    public void Vm_StackUnderflow_En()
    {
        var program = new[] { new Instruction(Opcode.Opr, 0, 1) };
        var vmOptions = new VirtualMachineOptions(Language: "en");
        var result = new VirtualMachine().Run(program, null, vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Stack underflow"));
    }

    [Fact]
    public void Vm_UnsupportedOpcode_En()
    {
        var program = new[] { new Instruction((Opcode)99, 0, 0) };
        var vmOptions = new VirtualMachineOptions(Language: "en");
        var result = new VirtualMachine().Run(program, null, vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Unsupported opcode"));
    }

    [Fact]
    public void Vm_UnsupportedOpr_En()
    {
        var program = new[]
        {
            new Instruction(Opcode.Lit, 0, 1),
            new Instruction(Opcode.Lit, 0, 1),
            new Instruction(Opcode.Opr, 0, 99),
        };
        var vmOptions = new VirtualMachineOptions(Language: "en");
        var result = new VirtualMachine().Run(program, null, vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Unsupported OPR code"));
    }

    [Fact]
    public void Vm_InvalidLodIndex_En()
    {
        var program = new[]
        {
            new Instruction(Opcode.Int, 0, 3),
            new Instruction(Opcode.Lod, 0, 5000),
        };
        var vmOptions = new VirtualMachineOptions(Language: "en");
        var result = new VirtualMachine().Run(program, null, vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Invalid LOD access"));
    }

    [Fact]
    public void Vm_InvalidStoIndex_En()
    {
        var program = new[]
        {
            new Instruction(Opcode.Lit, 0, 42),
            new Instruction(Opcode.Sto, 0, 5000),
        };
        var vmOptions = new VirtualMachineOptions(Language: "en");
        var result = new VirtualMachine().Run(program, null, vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Invalid STO access"));
    }

    [Fact]
    public void Vm_InvalidBasePointer_En()
    {
        // Level=2 verursacht zwei Traversierungen: stack[b]=0, dann 0<1 → InvalidBasePointer
        var program = new[]
        {
            new Instruction(Opcode.Int, 0, 3),
            new Instruction(Opcode.Lod, 2, 0),
        };
        var vmOptions = new VirtualMachineOptions(Language: "en");
        var result = new VirtualMachine().Run(program, null, vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Invalid base pointer"));
    }

    // ── T032: VM DE-Baseline ──────────────────────────────────────────────

    [Fact]
    public void VmDiagnostic_De_ContainsGermanMessage()
    {
        var source = File.ReadAllText(L10nPl0("division_by_zero.pl0"));
        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var vmOptions = new VirtualMachineOptions(Language: "de");
        var result = new VirtualMachine().Run(compilation.Instructions, new BufferedPl0Io(), vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Division durch null"));
    }

    // ── T037: SC-004 Extensibility — Schwedische Dummy-Fixtures ───────────

    [Fact]
    public void NewLocale_Se_InjectedResourceManager_Core_ReturnsSwedishMessage()
    {
        var seRm = new ResourceManager(
            "Pl0.Tests.Resources.Pl0CoreMessages",
            typeof(L10nTests).Assembly);
        var se = CultureInfo.GetCultureInfo("se");

        var options = new CompilerOptions(
            Pl0Dialect.Extended,
            Language: "se",
            Messages: seRm);

        var result = new Pl0Compiler().Compile("begin ! x end.", options);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Odefinierad identifierare"));
    }

    [Fact]
    public void NewLocale_Se_InjectedResourceManager_Vm_ReturnsSwedishMessage()
    {
        var seRm = new ResourceManager(
            "Pl0.Tests.Resources.Pl0VmMessages",
            typeof(L10nTests).Assembly);

        var source = File.ReadAllText(L10nPl0("division_by_zero.pl0"));
        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var vmOptions = new VirtualMachineOptions(Language: "se", Messages: seRm);
        var result = new VirtualMachine().Run(compilation.Instructions, new BufferedPl0Io(), vmOptions);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Message.Contains("Division med noll"));
    }

    [Fact]
    public void NewLocale_Se_InjectedResourceManager_Cli_ReturnsSwedishMessage()
    {
        var seRm = new ResourceManager(
            "Pl0.Tests.Resources.Pl0CliMessages",
            typeof(L10nTests).Assembly);
        var se = CultureInfo.GetCultureInfo("se");

        var actual = seRm.GetString("Cli_Help_UsageHeader", se);

        Assert.Equal("Användning:", actual);
    }

    // ── Hilfeklasse für InputFormatError-Test ─────────────────────────────

    private sealed class FormatExceptionIo : IPl0Io
    {
        public int ReadInt() => throw new FormatException("Not a number.");
        public void WriteInt(int value) { }
    }
}
