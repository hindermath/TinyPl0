namespace Pl0.Core;

public readonly record struct Instruction(Opcode Op, int Level, int Argument);
