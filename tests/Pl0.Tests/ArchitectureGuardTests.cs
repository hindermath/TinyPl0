using System.Xml.Linq;

namespace Pl0.Tests;

public sealed class ArchitectureGuardTests
{
    [Fact]
    public void Solution_Contains_Pl0_Ide_Project()
    {
        var solutionContent = File.ReadAllText(Path.Combine(FindRepoRoot(), "TinyPl0.sln"));
        Assert.Contains("\"Pl0.Ide\", \"src\\Pl0.Ide\\Pl0.Ide.csproj\"", solutionContent);
    }

    [Fact]
    public void Project_Reference_Graph_Matches_Architecture_Rules()
    {
        var expectedReferences = new Dictionary<string, string[]>(StringComparer.Ordinal)
        {
            ["Pl0.Core.csproj"] = [],
            ["Pl0.Vm.csproj"] = ["Pl0.Core.csproj"],
            ["Pl0.Cli.csproj"] = ["Pl0.Core.csproj", "Pl0.Vm.csproj"],
            ["Pl0.Ide.csproj"] = ["Pl0.Core.csproj", "Pl0.Vm.csproj"],
            ["Pl0.Tests.csproj"] = ["Pl0.Cli.csproj", "Pl0.Core.csproj", "Pl0.Ide.csproj", "Pl0.Vm.csproj"],
        };

        var projectFiles = FindProjectFiles();
        Assert.Equal(
            expectedReferences.Keys.OrderBy(x => x, StringComparer.Ordinal),
            projectFiles.Keys.OrderBy(x => x, StringComparer.Ordinal));

        foreach (var (projectName, expectedTargets) in expectedReferences)
        {
            var actualTargets = ReadProjectReferences(projectFiles[projectName]);
            Assert.Equal(expectedTargets, actualTargets);
        }
    }

    [Fact]
    public void Pl0_Ide_Package_References_Match_Architecture_Rules()
    {
        var projectFiles = FindProjectFiles();
        var packageReferences = ReadPackageReferences(projectFiles["Pl0.Ide.csproj"]);
        Assert.Equal(["Terminal.Gui"], packageReferences);
    }

    private static IReadOnlyDictionary<string, string> FindProjectFiles()
    {
        var repoRoot = FindRepoRoot();
        var projectPaths = Directory
            .EnumerateFiles(Path.Combine(repoRoot, "src"), "*.csproj", SearchOption.AllDirectories)
            .Concat(Directory.EnumerateFiles(Path.Combine(repoRoot, "tests"), "*.csproj", SearchOption.AllDirectories));

        return projectPaths.ToDictionary(
            path => Path.GetFileName(path) ?? throw new InvalidOperationException($"Invalid project path: {path}"),
            StringComparer.Ordinal);
    }

    private static string[] ReadProjectReferences(string projectPath)
    {
        var document = XDocument.Load(projectPath);
        return document
            .Descendants()
            .Where(node => node.Name.LocalName == "ProjectReference")
            .Select(node => node.Attribute("Include")?.Value)
            .Where(include => !string.IsNullOrWhiteSpace(include))
            .Select(include => Path.GetFileName(include!.Replace('\\', '/')))
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();
    }

    private static string[] ReadPackageReferences(string projectPath)
    {
        var document = XDocument.Load(projectPath);
        return document
            .Descendants()
            .Where(node => node.Name.LocalName == "PackageReference")
            .Select(node => node.Attribute("Include")?.Value)
            .Where(include => !string.IsNullOrWhiteSpace(include))
            .Select(include => include!)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();
    }

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

        throw new InvalidOperationException("Could not locate repository root from test context.");
    }
}
