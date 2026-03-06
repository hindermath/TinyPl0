using System.Resources;

namespace Pl0.Vm;

/// <summary>
/// Bietet Zugriff auf den ResourceManager für VM-Fehlertexte.
/// </summary>
internal static class Pl0VmMessages
{
    /// <summary>
    /// Statischer Cache für den ResourceManager.
    /// </summary>
    private static ResourceManager? _resourceManager;

    /// <summary>
    /// Gibt den ResourceManager für Pl0VmMessages zurück.
    /// </summary>
    internal static ResourceManager ResourceManager =>
        _resourceManager ??= new ResourceManager(
            "Pl0.Vm.Pl0VmMessages",
            typeof(Pl0VmMessages).Assembly);
}
