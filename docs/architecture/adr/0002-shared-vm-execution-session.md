# ADR 0002: Gemeinsame VM-Ausführungssession / Shared VM Execution Session

## Status

Accepted — 2026-09-02

## Deutsch

Batch- und Step-Ausführung verwendeten zwei Decoder und konnten bei Fehlern,
Budget oder I/O auseinanderlaufen. VmExecutionSession ist deshalb der einzige
Opcode-/OPR-Decoder. VirtualMachine.Run ist eine Schleife über denselben
Step-Pfad. Optionen und vollständiger P-Code werden vor Allokation validiert;
terminale Projektionen werden gespeichert.

Folgen: Parität entsteht strukturell und wird zusätzlich getestet. Die Session
bleibt intern. Cancellation ist kooperativ und bietet keine Zeit- oder
OS-Sandbox-Garantie. Der bewusste Trade-off ist eine vollständige defensive
Stackkopie pro sichtbarem Step zugunsten eines einfachen, sicheren Lehr-API.

## English

Batch and step execution previously used two decoders and could diverge on
errors, budgets, or I/O. VmExecutionSession is now the only opcode and OPR
decoder, while VirtualMachine.Run loops over the same step path. Options and
the complete P-Code program are validated before allocation, and terminal
projections are cached.

Parity is structural and additionally tested. The session remains internal.
Cancellation is cooperative and is not a time or operating-system sandbox.
The accepted trade-off is a complete defensive stack copy per visible step in
favour of a simple and safe teaching API.
