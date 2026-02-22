using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

public sealed class SteppableVirtualMachineTests
{
    [Fact]
    public void Initialize_Sets_Start_Registers_And_Running_State()
    {
        var vm = new SteppableVirtualMachine();
        vm.Initialize([new Instruction(Opcode.Opr, 0, 0)]);

        Assert.True(vm.IsRunning);
        Assert.Equal(0, vm.State.P);
        Assert.Equal(1, vm.State.B);
        Assert.Equal(0, vm.State.T);
    }

    [Fact]
    public void Step_Executes_Exactly_One_Instruction_And_Updates_State()
    {
        var vm = new SteppableVirtualMachine();
        vm.Initialize(
        [
            new Instruction(Opcode.Lit, 0, 7),
            new Instruction(Opcode.Opr, 0, 0)
        ]);

        var result = vm.Step();

        Assert.Equal(VmStepStatus.Running, result.Status);
        Assert.True(vm.IsRunning);
        Assert.Equal(1, result.State.P);
        Assert.Equal(1, result.State.B);
        Assert.Equal(1, result.State.T);
        Assert.Equal(7, result.State.Stack[1]);
    }

    [Fact]
    public void Step_After_Program_End_Remains_Halted_Without_Additional_Execution()
    {
        var vm = new SteppableVirtualMachine();
        vm.Initialize([new Instruction(Opcode.Opr, 0, 0)]);

        var first = vm.Step();
        var second = vm.Step();

        Assert.Equal(VmStepStatus.Halted, first.Status);
        Assert.Equal(VmStepStatus.Halted, second.Status);
        Assert.False(vm.IsRunning);
        Assert.Equal(first.State.P, second.State.P);
        Assert.Equal(first.State.B, second.State.B);
        Assert.Equal(first.State.T, second.State.T);
    }

    [Fact]
    public void Step_On_Runtime_Error_Returns_Error_And_Preserves_Last_Valid_State()
    {
        var vm = new SteppableVirtualMachine();
        vm.Initialize(
        [
            new Instruction(Opcode.Lit, 0, 1),
            new Instruction(Opcode.Lit, 0, 0),
            new Instruction(Opcode.Opr, 0, 5)
        ]);

        _ = vm.Step();
        _ = vm.Step();
        var stateBeforeError = vm.State;
        var stackBeforeError = stateBeforeError.Stack.ToArray();

        var errorStep = vm.Step();
        var stepAfterError = vm.Step();

        Assert.Equal(VmStepStatus.Error, errorStep.Status);
        Assert.Contains(errorStep.Diagnostics, d => d.Code == 206);
        Assert.False(vm.IsRunning);
        Assert.Equal(stateBeforeError.P, vm.State.P);
        Assert.Equal(stateBeforeError.B, vm.State.B);
        Assert.Equal(stateBeforeError.T, vm.State.T);
        Assert.Equal(stackBeforeError, vm.State.Stack);
        Assert.Equal(VmStepStatus.Error, stepAfterError.Status);
    }
}
