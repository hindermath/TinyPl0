using Pl0.Core;

namespace Pl0.Vm;

/// <summary>
/// Snapshot of the current VM state.
/// </summary>
/// <param name="P">Program counter.</param>
/// <param name="B">Base pointer.</param>
/// <param name="T">Top-of-stack pointer.</param>
/// <param name="Stack">Stack snapshot.</param>
/// <param name="CurrentInstruction">Instruction at the current program counter.</param>
public sealed record VmState(
    int P,
    int B,
    int T,
    int[] Stack,
    Instruction? CurrentInstruction);
