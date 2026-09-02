using Pl0.Core;

namespace Pl0.Vm;

/// <summary>Unveränderliche Momentaufnahme des VM-Zustands. / Immutable snapshot of VM state.</summary>
public sealed record VmState
{
    private readonly int[] stack;
    /// <summary>Erstellt die bisherige Zustandsform. / Creates the previous state shape.</summary>
    /// <param name="P">Befehlszeiger. / Program counter.</param>
    /// <param name="B">Basiszeiger. / Base pointer.</param>
    /// <param name="T">Stackzeiger. / Stack pointer.</param>
    /// <param name="Stack">Zu kopierender Stack. / Stack to copy.</param>
    /// <param name="CurrentInstruction">Nächste Instruktion. / Next instruction.</param>
    public VmState(int P, int B, int T, int[] Stack, Instruction? CurrentInstruction)
        : this(P, B, T, Stack, CurrentInstruction, 0) { }
    /// <summary>Erstellt einen Zustand mit Zähler. / Creates a state with a counter.</summary>
    /// <param name="P">Befehlszeiger. / Program counter.</param>
    /// <param name="B">Basiszeiger. / Base pointer.</param>
    /// <param name="T">Stackzeiger. / Stack pointer.</param>
    /// <param name="Stack">Zu kopierender Stack. / Stack to copy.</param>
    /// <param name="CurrentInstruction">Nächste Instruktion. / Next instruction.</param>
    /// <param name="ExecutedInstructions">Begonnene Instruktionen. / Started instructions.</param>
    public VmState(int P, int B, int T, int[] Stack, Instruction? CurrentInstruction, int ExecutedInstructions)
    {
        this.P = P; this.B = B; this.T = T; stack = (int[])Stack.Clone();
        this.CurrentInstruction = CurrentInstruction; this.ExecutedInstructions = ExecutedInstructions;
    }
    /// <summary>Ruft P ab. / Gets P.</summary>
    public int P { get; }
    /// <summary>Ruft B ab. / Gets B.</summary>
    public int B { get; }
    /// <summary>Ruft T ab. / Gets T.</summary>
    public int T { get; }
    /// <summary>Ruft eine defensive Stackkopie ab. / Gets a defensive stack copy.</summary>
    public int[] Stack => (int[])stack.Clone();
    /// <summary>Ruft die nächste Instruktion ab. / Gets the next instruction.</summary>
    public Instruction? CurrentInstruction { get; }
    /// <summary>Ruft den Instruktionszähler ab. / Gets the instruction counter.</summary>
    public int ExecutedInstructions { get; }
    /// <summary>Ruft den Befehlszeiger unter beschreibendem Namen ab. / Gets the program counter by its descriptive name.</summary>
    public int ProgramCounter => P;
    /// <summary>Ruft den Basiszeiger unter beschreibendem Namen ab. / Gets the base pointer by its descriptive name.</summary>
    public int BasePointer => B;
    /// <summary>Ruft den Stackzeiger unter beschreibendem Namen ab. / Gets the stack top by its descriptive name.</summary>
    public int StackTop => T;
    /// <summary>Zerlegt die bisherige Zustandsform. / Deconstructs the previous state shape.</summary>
    /// <param name="P">Befehlszeiger. / Program counter.</param>
    /// <param name="B">Basiszeiger. / Base pointer.</param>
    /// <param name="T">Stackzeiger. / Stack pointer.</param>
    /// <param name="Stack">Defensive Stackkopie. / Defensive stack copy.</param>
    /// <param name="CurrentInstruction">Nächste Instruktion. / Next instruction.</param>
    public void Deconstruct(out int P, out int B, out int T, out int[] Stack, out Instruction? CurrentInstruction)
    { P = this.P; B = this.B; T = this.T; Stack = this.Stack; CurrentInstruction = this.CurrentInstruction; }
}
