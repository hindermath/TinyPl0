using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

public sealed class VirtualMachineParityTests
{
    [Fact]
    public void Run_And_Step_Have_Parity_For_Success_And_Halt()
    {
        Instruction[] program = [new(Opcode.Lit, 0, 7), new(Opcode.Opr, 0, 15), new(Opcode.Opr, 0, 0)];
        AssertParity(program, () => new BufferedPl0Io(), VmCompletionReason.Halted, [7]);
    }

    [Fact]
    public void Run_And_Step_Have_Parity_For_Arithmetic_And_Stack_Faults()
    {
        AssertParity([new(Opcode.Lit, 0, 1), new(Opcode.Lit, 0, 0), new(Opcode.Opr, 0, 5)],
            () => new BufferedPl0Io(), VmCompletionReason.ArithmeticFault, []);
        AssertParity([new(Opcode.Opr, 0, 1)],
            () => new BufferedPl0Io(), VmCompletionReason.StackFault, []);
    }

    [Fact]
    public void Run_And_Step_Have_Parity_For_Input_And_Host_Io_Faults()
    {
        AssertParity([new(Opcode.Opr, 0, 14)], () => new BufferedPl0Io(),
            VmCompletionReason.InputEndOfStream, []);
        AssertParity([new(Opcode.Opr, 0, 14)], () => new ThrowingIo(new FormatException("private input")),
            VmCompletionReason.InputFormatError, []);
        (VmExecutionResult batch, VmStepResult stepped, _, _) = ExecuteBoth(
            [new(Opcode.Lit, 0, 5), new(Opcode.Opr, 0, 15)],
            () => new ThrowingIo(new IOException("private host path")));
        Assert.Equal(VmCompletionReason.IoFault, batch.Reason);
        Assert.Equal(batch.Reason, stepped.Reason);
        Assert.DoesNotContain("private", Assert.Single(batch.Diagnostics).Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Invalid_PCode_Is_Rejected_Before_Execution_With_Parity()
    {
        AssertParity([new((Opcode)99, 0, 0)], () => new BufferedPl0Io(),
            VmCompletionReason.InvalidProgram, []);
        AssertParity([new(Opcode.Jmp, 0, 1)], () => new BufferedPl0Io(),
            VmCompletionReason.InvalidProgram, []);
    }

    [Fact]
    public void Cancellation_Before_Start_Has_Zero_Count_And_No_Side_Effects()
    {
        using var source = new CancellationTokenSource();
        source.Cancel();
        Instruction[] program = [new(Opcode.Lit, 0, 7), new(Opcode.Opr, 0, 15), new(Opcode.Opr, 0, 0)];
        (VmExecutionResult batch, VmStepResult stepped, IInspectableIo batchIo, IInspectableIo stepIo) =
            ExecuteBoth(program, () => new InspectableIo(), cancellationToken: source.Token);
        Assert.Equal(VmCompletionReason.Cancelled, batch.Reason);
        Assert.Equal(batch.Reason, stepped.Reason);
        Assert.Equal(0, batch.ExecutedInstructions);
        Assert.Equal(0, stepped.ExecutedInstructions);
        Assert.Empty(batchIo.Output);
        Assert.Empty(stepIo.Output);
    }

    [Fact]
    public void Cancellation_During_Run_Is_Observed_At_The_Next_Boundary()
    {
        Instruction[] program =
        [
            new(Opcode.Lit, 0, 7), new(Opcode.Opr, 0, 15),
            new(Opcode.Lit, 0, 8), new(Opcode.Opr, 0, 15), new(Opcode.Opr, 0, 0)
        ];
        using var batchCancellation = new CancellationTokenSource();
        using var stepCancellation = new CancellationTokenSource();
        var batchIo = new CancellingIo(batchCancellation);
        var stepIo = new CancellingIo(stepCancellation);
        VmExecutionResult batch = new VirtualMachine().Run(program, batchIo, null, batchCancellation.Token);
        var vm = new SteppableVirtualMachine();
        vm.Initialize(program, stepIo, null, stepCancellation.Token);
        VmStepResult stepped = StepToTerminal(vm);
        Assert.Equal(VmCompletionReason.Cancelled, batch.Reason);
        Assert.Equal(batch.Reason, stepped.Reason);
        Assert.Equal(2, batch.ExecutedInstructions);
        Assert.Equal(batch.ExecutedInstructions, stepped.ExecutedInstructions);
        Assert.Equal([7], batchIo.Output);
        Assert.Equal(batchIo.Output, stepIo.Output);
    }

    [Fact]
    public void Budget_Stops_N_Plus_One_And_Terminal_Steps_Are_Idempotent()
    {
        Instruction[] program = [new(Opcode.Lit, 0, 7), new(Opcode.Opr, 0, 15), new(Opcode.Jmp, 0, 0)];
        var options = new VirtualMachineOptions(InstructionBudget: 2);
        (VmExecutionResult batch, VmStepResult terminal, _, _) = ExecuteBoth(program,
            () => new InspectableIo(), options);
        Assert.Equal(VmCompletionReason.InstructionBudgetExceeded, batch.Reason);
        Assert.Equal(2, batch.ExecutedInstructions);
        Assert.Equal(batch.ExecutedInstructions, terminal.ExecutedInstructions);

        var vm = new SteppableVirtualMachine();
        vm.Initialize(program, new BufferedPl0Io(), options);
        _ = vm.Step();
        _ = vm.Step();
        VmStepResult firstTerminal = vm.Step();
        VmStepResult repeated = vm.Step();
        Assert.Same(firstTerminal, repeated);
        Assert.Equal(firstTerminal.State, repeated.State);
        Assert.Equal(firstTerminal.Diagnostics, repeated.Diagnostics);
    }

    [Fact]
    public void Result_And_State_Expose_Defensive_Stack_Copies()
    {
        VmExecutionResult result = new VirtualMachine().Run(
            [new(Opcode.Lit, 0, 7), new(Opcode.Opr, 0, 0)], new BufferedPl0Io());
        int[] stack = result.StackSnapshot;
        int[] stateStack = result.State.Stack;
        stack[0] = 123;
        stateStack[0] = 456;
        Assert.NotEqual(123, result.StackSnapshot[0]);
        Assert.NotEqual(456, result.State.Stack[0]);

        VmExecutionResult fault = new VirtualMachine().Run([new(Opcode.Opr, 0, 1)]);
        Assert.Throws<NotSupportedException>(() => ((IList<VmDiagnostic>)fault.Diagnostics).Clear());
    }

    [Fact]
    public void Trust_Boundaries_Reject_Configuration_And_Program_Before_Dispatch()
    {
        VmExecutionResult invalidBudget = new VirtualMachine().Run(
            [new(Opcode.Opr, 0, 0)], options: new VirtualMachineOptions(InstructionBudget: 10_000_001));
        Assert.Equal(VmCompletionReason.InvalidConfiguration, invalidBudget.Reason);
        Assert.Equal(0, invalidBudget.ExecutedInstructions);
        Assert.Empty(invalidBudget.StackSnapshot);

        VmExecutionResult invalidLimit = new VirtualMachine().Run(
            [new(Opcode.Opr, 0, 0)], options: new VirtualMachineOptions(MaximumProgramLength: 100_001));
        Assert.Equal(VmCompletionReason.InvalidConfiguration, invalidLimit.Reason);

        VmExecutionResult nullProgram = new VirtualMachine().Run(null!);
        Assert.Equal(VmCompletionReason.InvalidProgram, nullProgram.Reason);
        Assert.Equal(0, nullProgram.ExecutedInstructions);

        VmExecutionResult invalidArgument = new VirtualMachine().Run([new(Opcode.Int, 0, -1)]);
        Assert.Equal(VmCompletionReason.InvalidProgram, invalidArgument.Reason);
        Assert.Equal(0, invalidArgument.ExecutedInstructions);
    }

    [Fact]
    public void Previous_Constructors_And_Deconstructors_Remain_Available()
    {
        Assert.Contains(typeof(VirtualMachineOptions).GetConstructors(),
            constructor => constructor.GetParameters().Length == 5);
        var state = new VmState(0, 1, 0, [], null);
        var legacyExecution = new VmExecutionResult([], 0, []);
        var legacyStep = new VmStepResult(state, VmStepStatus.Halted, []);
        (VmState deconstructedState, VmStepStatus status, IReadOnlyList<VmDiagnostic> diagnostics) = legacyStep;
        Assert.True(legacyExecution.Success);
        Assert.Same(state, deconstructedState);
        Assert.Equal(VmStepStatus.Halted, status);
        Assert.Empty(diagnostics);
    }

    private static void AssertParity(Instruction[] program, Func<IPl0Io> ioFactory,
        VmCompletionReason reason, int[] expectedOutput)
    {
        (VmExecutionResult batch, VmStepResult stepped, IInspectableIo batchIo, IInspectableIo stepIo) =
            ExecuteBoth(program, ioFactory);
        Assert.Equal(reason, batch.Reason);
        Assert.Equal(batch.Reason, stepped.Reason);
        Assert.Equal(batch.ExecutedInstructions, stepped.ExecutedInstructions);
        Assert.Equal(batch.Diagnostics.Select(item => item.Code), stepped.Diagnostics.Select(item => item.Code));
        Assert.Equal(batch.State.P, stepped.State.P);
        Assert.Equal(batch.State.B, stepped.State.B);
        Assert.Equal(batch.State.T, stepped.State.T);
        Assert.Equal(batch.State.Stack, stepped.State.Stack);
        Assert.Equal(expectedOutput, batchIo.Output);
        Assert.Equal(batchIo.Output, stepIo.Output);
    }

    private static (VmExecutionResult Batch, VmStepResult Stepped, IInspectableIo BatchIo, IInspectableIo StepIo)
        ExecuteBoth(Instruction[] program, Func<IPl0Io> ioFactory,
            VirtualMachineOptions? options = null, CancellationToken cancellationToken = default)
    {
        IPl0Io rawBatchIo = ioFactory();
        IPl0Io rawStepIo = ioFactory();
        IInspectableIo batchIo = rawBatchIo as IInspectableIo ?? new IoView(rawBatchIo);
        IInspectableIo stepIo = rawStepIo as IInspectableIo ?? new IoView(rawStepIo);
        VmExecutionResult batch = new VirtualMachine().Run(program, rawBatchIo, options, cancellationToken);
        var vm = new SteppableVirtualMachine();
        vm.Initialize(program, rawStepIo, options, cancellationToken);
        VmStepResult stepped = StepToTerminal(vm);
        return (batch, stepped, batchIo, stepIo);
    }

    private static VmStepResult StepToTerminal(SteppableVirtualMachine vm)
    {
        VmStepResult result;
        do result = vm.Step(); while (result.Status == VmStepStatus.Running);
        return result;
    }

    private interface IInspectableIo : IPl0Io { IReadOnlyList<int> Output { get; } }

    private sealed class InspectableIo : IInspectableIo
    {
        private readonly List<int> output = [];
        public IReadOnlyList<int> Output => output;
        public int ReadInt() => throw new EndOfStreamException();
        public void WriteInt(int value) => output.Add(value);
    }

    private sealed class CancellingIo(CancellationTokenSource source) : IInspectableIo
    {
        private readonly List<int> output = [];
        public IReadOnlyList<int> Output => output;
        public int ReadInt() => throw new EndOfStreamException();
        public void WriteInt(int value) { output.Add(value); source.Cancel(); }
    }

    private sealed class ThrowingIo(Exception exception) : IInspectableIo
    {
        public IReadOnlyList<int> Output => [];
        public int ReadInt() => throw exception;
        public void WriteInt(int value) => throw exception;
    }

    private sealed class IoView(IPl0Io inner) : IInspectableIo
    {
        public IReadOnlyList<int> Output => inner is BufferedPl0Io buffered ? buffered.Output : [];
        public int ReadInt() => inner.ReadInt();
        public void WriteInt(int value) => inner.WriteInt(value);
    }
}
