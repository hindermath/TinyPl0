namespace Pl0.Core;

public sealed record CompilerOptions(
    Pl0Dialect Dialect,
    int MaxLevel = 3,
    int MaxAddress = 2047)
{
    public static CompilerOptions Default { get; } = new(Pl0Dialect.Extended);

    public bool EnableIoStatements => Dialect == Pl0Dialect.Extended;
}
