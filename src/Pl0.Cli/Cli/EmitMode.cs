namespace Pl0.Cli.Cli;

/// <summary>
/// Emit modes for CLI output.
/// </summary>
public enum EmitMode
{
    /// <summary>
    /// No emission requested.
    /// </summary>
    None = 0,
    /// <summary>
    /// Emit mnemonic assembly.
    /// </summary>
    Asm,
    /// <summary>
    /// Emit numeric opcode format.
    /// </summary>
    Cod,
}
