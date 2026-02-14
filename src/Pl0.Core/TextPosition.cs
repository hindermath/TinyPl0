namespace Pl0.Core;

/// <summary>
/// Represents a position in the source text.
/// </summary>
/// <param name="Line">1-based line number.</param>
/// <param name="Column">1-based column number.</param>
/// <param name="Offset">0-based character offset.</param>
public readonly record struct TextPosition(int Line, int Column, int Offset);
