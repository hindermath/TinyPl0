namespace Pl0.Core;

/// <summary>
/// Supported PL/0 language dialects.
/// </summary>
public enum Pl0Dialect
{
    /// <summary>
    /// Historical dialect without input/output statements.
    /// </summary>
    Classic = 0,
    /// <summary>
    /// Extended dialect with ? input and ! output statements.
    /// </summary>
    Extended = 1,
}
