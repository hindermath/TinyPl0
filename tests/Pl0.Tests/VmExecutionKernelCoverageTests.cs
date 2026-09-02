using System.Globalization;
using System.Resources;
using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

public sealed class VmExecutionKernelCoverageTests
{
    [Theory]
    [InlineData("de", "Stack-Überlauf")]
    [InlineData("en", "Stack overflow")]
    public void Missing_Resources_Use_Language_Specific_Safe_Fallbacks(string language, string expected)
    {
        var options = new VirtualMachineOptions(
            StackSize: 3,
            Language: language,
            Messages: new MissingResourceManager());

        VmExecutionResult result = new VirtualMachine().Run(
            [new(Opcode.Lit, 0, 1), new(Opcode.Lit, 0, 2), new(Opcode.Lit, 0, 3), new(Opcode.Lit, 0, 4)],
            options: options);

        Assert.Equal(VmCompletionReason.StackFault, result.Reason);
        Assert.Contains(expected, Assert.Single(result.Diagnostics).Message);
    }

    [Theory]
    [InlineData("de", "Ungültiges")]
    [InlineData("en", "Invalid")]
    public void Missing_Resources_Also_Cover_Preflight_Fallbacks(string language, string expected)
    {
        var options = new VirtualMachineOptions(
            Language: language,
            Messages: new MissingResourceManager(),
            MaximumProgramLength: 1);

        VmExecutionResult result = new VirtualMachine().Run(
            [new(Opcode.Lit, 0, 1), new(Opcode.Opr, 0, 0)],
            options: options);

        Assert.Equal(VmCompletionReason.InvalidProgram, result.Reason);
        Assert.Contains(expected, Assert.Single(result.Diagnostics).Message);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    public void Binary_Operations_With_One_Operand_Report_Stack_Underflow(int operation)
    {
        VmExecutionResult result = new VirtualMachine().Run(
            [new(Opcode.Lit, 0, 1), new(Opcode.Opr, 0, operation)]);

        Assert.Equal(VmCompletionReason.StackFault, result.Reason);
        Assert.Equal(2, result.ExecutedInstructions);
    }

    [Theory]
    [MemberData(nameof(FatalHostExceptions))]
    public void Fatal_Host_Exceptions_Are_Not_Misreported_As_Recoverable(Exception exception)
    {
        var io = new FatalIo(exception);

        Exception actual = Assert.Throws(exception.GetType(), () =>
            new VirtualMachine().Run([new(Opcode.Opr, 0, 14)], io));

        Assert.Same(exception, actual);
    }

    public static TheoryData<Exception> FatalHostExceptions => new()
    {
        new OutOfMemoryException("fatal"),
        new StackOverflowException("fatal"),
        new AccessViolationException("fatal")
    };

    [Fact]
    public void Return_Address_Can_Produce_A_Checked_Out_Of_Range_State()
    {
        Instruction[] program =
        [
            new(Opcode.Lit, 0, 1),
            new(Opcode.Lit, 0, 1),
            new(Opcode.Lit, 0, 99),
            new(Opcode.Opr, 0, 0)
        ];

        VmExecutionResult result = new VirtualMachine().Run(program);

        Assert.Equal(VmCompletionReason.RuntimeFault, result.Reason);
        Assert.Equal(4, result.ExecutedInstructions);
    }

    private sealed class MissingResourceManager : ResourceManager
    {
        public override string? GetString(string name, CultureInfo? culture) => null;
    }

    private sealed class FatalIo(Exception exception) : IPl0Io
    {
        public int ReadInt() => throw exception;
        public void WriteInt(int value) => throw exception;
    }
}
