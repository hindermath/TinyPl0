using Pl0.Core;

namespace Pl0.Ide;

internal enum IdeCodeDisplayMode
{
    PCode = 0,
    Assembler = 1
}

internal sealed record IdeCompilerSettingsDialogResult(CompilerOptions CompilerOptions, IdeCodeDisplayMode CodeDisplayMode);
