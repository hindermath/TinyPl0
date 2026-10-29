using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

public sealed class VmExecutionBoundaryTests
{
    public static TheoryData<Instruction[]> CorruptedReturnFrames => new()
    {
        {
            [
                new Instruction(Opcode.Cal, 0, 2),
                new Instruction(Opcode.Opr, 0, 0),
                new Instruction(Opcode.Lit, 0, -100),
                new Instruction(Opcode.Sto, 0, 1),
                new Instruction(Opcode.Opr, 0, 0)
            ]
        },
        {
            [
                new Instruction(Opcode.Cal, 0, 2),
                new Instruction(Opcode.Opr, 0, 0),
                new Instruction(Opcode.Lit, 0, -100),
                new Instruction(Opcode.Lit, 0, -100),
                new Instruction(Opcode.Opr, 0, 0)
            ]
        }
    };

    public static TheoryData<Instruction[]> InvalidPrograms => new()
    {
        { [new Instruction(Opcode.Lit, -1, 0)] },
        { [new Instruction(Opcode.Lit, 4, 0)] },
        { [new Instruction(Opcode.Lod, 0, -1)] },
        { [new Instruction(Opcode.Sto, 0, -1)] },
        { [new Instruction(Opcode.Cal, 0, 1)] },
        { [new Instruction(Opcode.Jpc, 0, 1)] }
    };

    [Theory]
    [MemberData(nameof(InvalidPrograms))]
    public void Invalid_Instruction_Boundaries_Stop_Before_Dispatch(Instruction[] program)
    {
        VmExecutionResult result = new VirtualMachine().Run(program);

        Assert.Equal(VmCompletionReason.InvalidProgram, result.Reason);
        Assert.Equal(0, result.ExecutedInstructions);
        Assert.Empty(result.StackSnapshot);
    }

    [Fact]
    public void Empty_And_Overlong_Programs_Stop_Before_Dispatch()
    {
        VmExecutionResult empty = new VirtualMachine().Run([]);
        VmExecutionResult overlong = new VirtualMachine().Run(
            [new(Opcode.Lit, 0, 1), new(Opcode.Opr, 0, 0)],
            options: new VirtualMachineOptions(MaximumProgramLength: 1));

        Assert.Equal(VmCompletionReason.InvalidProgram, empty.Reason);
        Assert.Equal(VmCompletionReason.InvalidProgram, overlong.Reason);
        Assert.Equal(0, empty.ExecutedInstructions);
        Assert.Equal(0, overlong.ExecutedInstructions);
    }

    [Fact]
    public void Falling_Off_Program_Returns_A_Safe_Runtime_Fault()
    {
        VmExecutionResult result = new VirtualMachine().Run([new(Opcode.Lit, 0, 1)]);

        Assert.Equal(VmCompletionReason.RuntimeFault, result.Reason);
        Assert.Equal(1, result.ExecutedInstructions);
        Assert.Contains("Befehlszeiger", Assert.Single(result.Diagnostics).Message);
    }

    [Fact]
    public void Oversized_Stack_Accesses_Fail_Without_Indexing_Outside_The_Stack()
    {
        VmExecutionResult load = new VirtualMachine().Run(
            [new(Opcode.Int, 0, 3), new(Opcode.Lod, 0, int.MaxValue)]);
        VmExecutionResult store = new VirtualMachine().Run(
            [new(Opcode.Int, 0, 3), new(Opcode.Lit, 0, 1), new(Opcode.Sto, 0, int.MaxValue)]);

        Assert.Equal(VmCompletionReason.StackFault, load.Reason);
        Assert.Equal(VmCompletionReason.StackFault, store.Reason);
    }

    [Theory]
    [MemberData(nameof(CorruptedReturnFrames))]
    public void Corrupted_Return_Frames_Are_Contained_Without_Host_Exceptions(Instruction[] program)
    {
        VmExecutionResult result = new VirtualMachine().Run(program);

        Assert.Equal(VmCompletionReason.StackFault, result.Reason);
        Assert.Equal(4, result.ExecutedInstructions);
        Assert.Equal(99, Assert.Single(result.Diagnostics).Code);
        Assert.DoesNotContain("IndexOutOfRangeException", result.Diagnostics[0].Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(501)]
    [InlineData(int.MaxValue)]
    public void Invalid_Dynamic_Links_Are_Rejected_Before_Register_Mutation(int dynamicLink)
    {
        Instruction[] program =
        [
            new(Opcode.Cal, 0, 2),
            new(Opcode.Opr, 0, 0),
            new(Opcode.Lit, 0, dynamicLink),
            new(Opcode.Sto, 0, 1),
            new(Opcode.Opr, 0, 0)
        ];

        VmExecutionResult result = new VirtualMachine().Run(program);

        Assert.Equal(VmCompletionReason.StackFault, result.Reason);
        Assert.Equal(1, result.State.B);
        Assert.Equal(0, result.State.T);
        Assert.Equal(4, result.State.P);
    }

    [Fact]
    public void Root_Return_Allows_Historical_Operand_Use_Of_The_Dynamic_Link_Cell()
    {
        var io = new BufferedPl0Io();
        Instruction[] program =
        [
            new(Opcode.Lit, 0, 7),
            new(Opcode.Lit, 0, 2),
            new(Opcode.Opr, 0, 2),
            new(Opcode.Opr, 0, 15),
            new(Opcode.Opr, 0, 0)
        ];

        VmExecutionResult result = new VirtualMachine().Run(program, io);

        Assert.Equal(VmCompletionReason.Halted, result.Reason);
        Assert.Equal([9], io.Output);
    }
}
