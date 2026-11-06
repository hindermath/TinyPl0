using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

public sealed class VirtualMachineResourceBoundaryTests
{
    [Fact]
    public void Batch_Run_Does_Not_Clone_The_Full_Stack_Per_Instruction()
    {
        Instruction[] program =
        [
            new(Opcode.Jmp, 0, 1),
            new(Opcode.Jmp, 0, 1),
        ];
        var options = new VirtualMachineOptions(
            StackSize: 100_000,
            InstructionBudget: 1_000);
        long before = GC.GetAllocatedBytesForCurrentThread();

        VmExecutionResult result = new VirtualMachine().Run(program, options: options);

        long allocated = GC.GetAllocatedBytesForCurrentThread() - before;
        Assert.Equal(VmCompletionReason.InstructionBudgetExceeded, result.Reason);
        Assert.True(allocated < 20_000_000, $"Batch execution allocated {allocated} bytes.");
    }

    [Fact]
    public void Batch_Run_Preserves_Pre_Dispatch_State_When_Store_Trace_Fails()
    {
        Instruction[] program =
        [
            new(Opcode.Int, 0, 3),
            new(Opcode.Lit, 0, 7),
            new(Opcode.Sto, 0, 2),
            new(Opcode.Opr, 0, 0),
        ];
        var options = new VirtualMachineOptions(EnableStoreTrace: true);

        VmExecutionResult result = new VirtualMachine().Run(
            program,
            new FailingWriteIo(),
            options);

        Assert.Equal(VmCompletionReason.IoFault, result.Reason);
        Assert.Equal(0, result.State.Stack[3]);
        Assert.Equal(7, result.State.Stack[4]);
    }

    private sealed class FailingWriteIo : IPl0Io
    {
        public int ReadInt() => 0;

        public void WriteInt(int value) => throw new IOException("fixture failure");
    }
}
