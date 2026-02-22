using Terminal.Gui;

namespace Pl0.Ide;

internal interface IIdeMessageDialogService
{
    void ShowInfo(string title, string message);
}

internal sealed class TerminalGuiIdeMessageDialogService : IIdeMessageDialogService
{
    public void ShowInfo(string title, string message)
    {
        _ = MessageBox.Query(title, message, "OK");
    }
}
