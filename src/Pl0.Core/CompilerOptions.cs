namespace Pl0.Core;

public sealed record CompilerOptions(
    Pl0Dialect Dialect,
    int MaxLevel = 3,
    int MaxAddress = 2047,
    int MaxIdentifierLength = 10,
    int MaxNumberDigits = 14,
    int MaxSymbolCount = 100,
    int MaxCodeLength = 200)
{
    public static CompilerOptions Default { get; } = new(Pl0Dialect.Extended);

    public bool EnableIoStatements => Dialect == Pl0Dialect.Extended;
}
