using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

public sealed class PackageWorkflowContractTests
{
    [Fact]
    public void Ci_Uses_Three_Platforms_And_Package_Consumer_Gate()
    {
        string workflow = File.ReadAllText(Path.Combine(RepoRoot, ".github", "workflows", "ci.yml"));
        Assert.Contains("matrix:", workflow);
        Assert.Contains("ubuntu-24.04", workflow);
        Assert.Contains("windows-2025", workflow);
        Assert.Contains("macos-15", workflow);
        Assert.Contains("Test-NuGetPackages.ps1", workflow);
        Assert.Contains("package-validation.json", workflow);
    }

    [Fact]
    public void Release_Uses_Pinned_Oidc_And_Fail_Closed_Paired_Publishing()
    {
        string workflow = File.ReadAllText(Path.Combine(RepoRoot, ".github", "workflows", "release-please.yml"));
        string[] pins =
        [
            "googleapis/release-please-action@45996ed1f6d02564a971a2fa1b5860e934307cf7",
            "actions/checkout@3d3c42e5aac5ba805825da76410c181273ba90b1",
            "actions/setup-dotnet@d4c94342e560b34958eacfc5d055d21461ed1c5d",
            "actions/upload-artifact@ea165f8d65b6e75b540449e92b4886f43607fa02",
            "actions/download-artifact@d3f86a106a0bac45b974a628896c90dbdf5c8093",
            "NuGet/login@8d196754b4036150537f80ac539e15c2f1028841",
            "actions/attest@508db95dd578ae2727ebd6217d5ba78e4fbda05d"
        ];
        foreach (string pin in pins) Assert.Contains(pin, workflow);
        Assert.Contains("environment: nuget-release", workflow);
        Assert.Contains("id-token: write", workflow);
        Assert.Contains("Partial publication detected", workflow);
        Assert.Contains("TinyPl0.Core", workflow);
        Assert.Contains("TinyPl0.Vm", workflow);
        Assert.DoesNotContain("--skip-duplicate", workflow);
        Assert.DoesNotContain("NUGET_API_KEY:", workflow.Replace("NUGET_API_KEY: ${{ steps.nuget-login.outputs.NUGET_API_KEY }}", string.Empty));
    }

    [Fact]
    public void Package_Projects_Use_One_Version_And_Exact_Dependency()
    {
        string core = File.ReadAllText(Path.Combine(RepoRoot, "src", "Pl0.Core", "Pl0.Core.csproj"));
        string vm = File.ReadAllText(Path.Combine(RepoRoot, "src", "Pl0.Vm", "Pl0.Vm.csproj"));
        string version = File.ReadAllText(Path.Combine(RepoRoot, "eng", "TinyPl0.PackageVersion.props"));
        Assert.Contains("<TinyPl0PackageVersion>0.4.0</TinyPl0PackageVersion>", version);
        Assert.Contains("<PackageId>TinyPl0.Core</PackageId>", core);
        Assert.Contains("<PackageId>TinyPl0.Vm</PackageId>", vm);
        Assert.Contains("[$(TinyPl0PackageVersion)]", vm);
        Assert.DoesNotContain("Terminal.Gui", core);
        Assert.DoesNotContain("Terminal.Gui", vm);
    }

    [Fact]
    public void Package_Readmes_Are_Bilingual_And_Keep_Safety_Contracts()
    {
        string core = File.ReadAllText(Path.Combine(RepoRoot, "docs", "nuget", "TinyPl0.Core.README.md"));
        string vm = File.ReadAllText(Path.Combine(RepoRoot, "docs", "nuget", "TinyPl0.Vm.README.md"));

        Assert.Contains("## Deutsch", core);
        Assert.Contains("## English", core);
        Assert.Contains("## Deutsch", vm);
        Assert.Contains("## English", vm);
        Assert.True(core.IndexOf("## Deutsch", StringComparison.Ordinal) <
                    core.IndexOf("## English", StringComparison.Ordinal));
        Assert.True(vm.IndexOf("## Deutsch", StringComparison.Ordinal) <
                    vm.IndexOf("## English", StringComparison.Ordinal));
        Assert.Contains("CompilationResult.Success", core);
        Assert.Contains("VmCompletionReason.Halted", vm);
        Assert.Contains("InstructionBudget", vm);
        Assert.Contains("CancellationToken", vm);
        Assert.Contains("IPl0Io", vm);
    }

    [Fact]
    public void Package_Metadata_Contains_Discovery_Tags()
    {
        string core = File.ReadAllText(Path.Combine(RepoRoot, "src", "Pl0.Core", "Pl0.Core.csproj"));
        string vm = File.ReadAllText(Path.Combine(RepoRoot, "src", "Pl0.Vm", "Pl0.Vm.csproj"));

        foreach (string tag in new[] { "compiler-construction", "p-code", "teaching" })
        {
            Assert.Contains(tag, core);
            Assert.Contains(tag, vm);
        }

        Assert.Contains("virtual-machine;vm", vm);
    }

    [Fact]
    public void Package_Readme_Examples_Use_Current_Public_Apis()
    {
        const string source = """
            const answer = 42;
            begin
              ! answer
            end.
            """;
        CompilerOptions compilerOptions = new(Pl0Dialect.Extended, Language: "de");
        CompilationResult compilation = new Pl0Compiler().Compile(source, compilerOptions);
        Assert.True(compilation.Success);

        BufferedPl0Io io = new();
        VirtualMachineOptions vmOptions = new(
            StackSize: 500,
            Language: "de",
            InstructionBudget: 10_000,
            MaximumProgramLength: 1_000);
        VmExecutionResult result = new VirtualMachine().Run(compilation.Instructions, io, vmOptions);

        Assert.Equal(VmCompletionReason.Halted, result.Reason);
        Assert.Equal([42], io.Output);

        Instruction[] program = [new(Opcode.Opr, 0, 0)];
        SteppableVirtualMachine debugger = new();
        debugger.Initialize(program, options: vmOptions);
        VmStepResult step = debugger.Step();
        Assert.Equal(VmCompletionReason.Halted, step.Reason);
    }

    private static string RepoRoot
    {
        get
        {
            DirectoryInfo? directory = new(AppContext.BaseDirectory);
            while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "TinyPl0.sln")))
                directory = directory.Parent;
            return directory?.FullName ?? throw new InvalidOperationException("Repository root not found.");
        }
    }
}
