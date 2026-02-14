namespace Pl0.Core;

/// <summary>
/// P-Code operation codes understood by the virtual machine.
/// </summary>
public enum Opcode
{
    /// <summary>
    /// Load literal constant onto the stack.
    /// </summary>
    Lit = 0,
    /// <summary>
    /// Execute arithmetic, comparison, or return operations.
    /// </summary>
    Opr = 1,
    /// <summary>
    /// Load a variable from the stack frame.
    /// </summary>
    Lod = 2,
    /// <summary>
    /// Store a value into a stack frame variable.
    /// </summary>
    Sto = 3,
    /// <summary>
    /// Call a procedure.
    /// </summary>
    Cal = 4,
    /// <summary>
    /// Allocate stack space.
    /// </summary>
    Int = 5,
    /// <summary>
    /// Unconditional jump.
    /// </summary>
    Jmp = 6,
    /// <summary>
    /// Conditional jump based on top-of-stack.
    /// </summary>
    Jpc = 7,
}
