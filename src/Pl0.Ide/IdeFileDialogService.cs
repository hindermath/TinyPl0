using Terminal.Gui;

namespace Pl0.Ide;

internal interface IIdeFileDialogService
{
    string? ShowOpenDialog();
    string? ShowSaveDialog(string? currentPath);
    string? ShowExportDialog(IdeEmitMode mode, string? suggestedPath);
}

internal sealed class TerminalGuiIdeFileDialogService : IIdeFileDialogService
{
    public string? ShowOpenDialog()
    {
        var allowedTypes = CreateOpenAllowedTypes();
        var dialog = new OpenDialog
        {
            Title = "Datei oeffnen",
            OpenMode = OpenMode.File,
            MustExist = true,
            AllowsMultipleSelection = false,
            AllowedTypes = allowedTypes
        };

        Application.Run(dialog);
        if (dialog.Canceled)
        {
            return null;
        }

        return ResolveSelectedPath(dialog.Path, dialog.FilePaths.FirstOrDefault(), basePath: null);
    }

    public string? ShowSaveDialog(string? currentPath)
    {
        var allowedTypes = CreateSaveAllowedTypes();
        var dialog = new SaveDialog
        {
            Title = "Datei speichern",
            OpenMode = OpenMode.File,
            MustExist = false,
            AllowedTypes = allowedTypes
        };

        if (!string.IsNullOrWhiteSpace(currentPath))
        {
            dialog.Path = currentPath;
        }

        Application.Run(dialog);
        if (dialog.Canceled)
        {
            return null;
        }

        return ResolveSelectedPath(dialog.Path, dialog.FileName, currentPath);
    }

    public string? ShowExportDialog(IdeEmitMode mode, string? suggestedPath)
    {
        var allowedTypes = mode == IdeEmitMode.Cod
            ? CreateExportCodAllowedTypes()
            : CreateExportAsmAllowedTypes();

        var dialog = new SaveDialog
        {
            Title = mode == IdeEmitMode.Cod ? "P-Code exportieren (Cod)" : "P-Code exportieren (Asm)",
            OpenMode = OpenMode.File,
            MustExist = false,
            AllowedTypes = allowedTypes
        };

        if (!string.IsNullOrWhiteSpace(suggestedPath))
        {
            dialog.Path = suggestedPath;
        }

        Application.Run(dialog);
        if (dialog.Canceled)
        {
            return null;
        }

        return ResolveSelectedPath(dialog.Path, dialog.FileName, suggestedPath);
    }

    private static string? ResolveSelectedPath(string? selectedPath, string? fallbackFileName, string? basePath)
    {
        if (string.IsNullOrWhiteSpace(selectedPath))
        {
            selectedPath = fallbackFileName;
        }

        if (string.IsNullOrWhiteSpace(selectedPath))
        {
            return null;
        }

        var fullSelectedPath = Path.IsPathRooted(selectedPath)
            ? selectedPath
            : CombineWithBasePathOrCurrentDirectory(selectedPath, basePath);

        return fullSelectedPath;
    }

    private static string CombineWithBasePathOrCurrentDirectory(string selectedPath, string? basePath)
    {
        var baseDirectory = DetermineBaseDirectory(basePath);
        if (!string.IsNullOrWhiteSpace(baseDirectory))
        {
            return Path.Combine(baseDirectory, selectedPath);
        }

        return Path.GetFullPath(selectedPath);
    }

    private static string? DetermineBaseDirectory(string? basePath)
    {
        if (string.IsNullOrWhiteSpace(basePath))
        {
            return null;
        }

        var fullBasePath = Path.GetFullPath(basePath);
        if (Directory.Exists(fullBasePath))
        {
            return fullBasePath;
        }

        return Path.GetDirectoryName(fullBasePath);
    }

    internal static List<IAllowedType> CreateOpenAllowedTypes()
    {
        return
        [
            new AllowedType("PL/0 (*.pl0)", ".pl0"),
            new AllowedTypeAny()
        ];
    }

    internal static List<IAllowedType> CreateSaveAllowedTypes()
    {
        return
        [
            new Pl0SaveAllowedType(),
            new AllowedTypeAny()
        ];
    }

    internal static List<IAllowedType> CreateExportAsmAllowedTypes()
    {
        return
        [
            new AllowedType("Assembler (*.asm)", ".asm"),
            new AllowedTypeAny()
        ];
    }

    internal static List<IAllowedType> CreateExportCodAllowedTypes()
    {
        return
        [
            new AllowedType("P-Code (*.cod)", ".cod"),
            new AllowedTypeAny()
        ];
    }

    private sealed class Pl0SaveAllowedType : IAllowedType
    {
        public bool IsAllowed(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            var extension = Path.GetExtension(path);
            return string.IsNullOrEmpty(extension) ||
                   string.Equals(extension, ".pl0", StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return "PL/0 (*.pl0)";
        }
    }
}
