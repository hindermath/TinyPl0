namespace Pl0.Core;

/// <summary>
/// A single P-Code instruction with opcode, lexical level, and argument.
/// </summary>
/// <param name="Op">The operation code.</param>
/// <param name="Level">The lexical level argument.</param>
/// <param name="Argument">The instruction argument or address.</param>
public readonly record struct Instruction(Opcode Op, int Level, int Argument);
