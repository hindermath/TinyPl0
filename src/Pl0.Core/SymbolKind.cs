namespace Pl0.Core;

/// <summary>
/// Symbol table entry kinds used by the parser and compiler.
/// </summary>
public enum SymbolKind
{
    /// <summary>
    /// A constant declaration.
    /// </summary>
    Constant = 0,
    /// <summary>
    /// A variable declaration.
    /// </summary>
    Variable = 1,
    /// <summary>
    /// A procedure declaration.
    /// </summary>
    Procedure = 2,
}
