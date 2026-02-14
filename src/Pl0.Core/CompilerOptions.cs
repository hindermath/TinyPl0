namespace Pl0.Core;

/// <summary>
/// Compiler configuration limits and language dialect selection.
/// </summary>
/// <param name="Dialect">Selected language dialect.</param>
/// <param name="MaxLevel">Maximum block nesting depth.</param>
/// <param name="MaxAddress">Maximum address/value for literals.</param>
/// <param name="MaxIdentifierLength">Maximum identifier length.</param>
/// <param name="MaxNumberDigits">Maximum digits in numeric literals.</param>
/// <param name="MaxSymbolCount">Maximum number of symbols in the table.</param>
/// <param name="MaxCodeLength">Maximum number of generated instructions.</param>
public sealed record CompilerOptions(
    Pl0Dialect Dialect,
    int MaxLevel = 3,
    int MaxAddress = 2047,
    int MaxIdentifierLength = 10,
    int MaxNumberDigits = 14,
    int MaxSymbolCount = 100,
    int MaxCodeLength = 200)
{
    /// <summary>
    /// Default compiler options using the extended dialect.
    /// </summary>
    public static CompilerOptions Default { get; } = new(Pl0Dialect.Extended);

    /// <summary>
    /// Gets a value indicating whether I/O statements are enabled for the dialect.
    /// </summary>
    public bool EnableIoStatements => Dialect == Pl0Dialect.Extended;
}
