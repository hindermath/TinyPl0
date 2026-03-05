using System.Resources;

namespace Pl0.Core;

/// <summary>
/// Bietet Zugriff auf den ResourceManager für Compiler-Diagnosetexte.
/// </summary>
internal static class Pl0CoreMessages
{
    /// <summary>
    /// Statischer Cache für den ResourceManager.
    /// </summary>
    private static ResourceManager? _resourceManager;

    /// <summary>
    /// Gibt den ResourceManager für Pl0CoreMessages zurück.
    /// </summary>
    internal static ResourceManager ResourceManager =>
        _resourceManager ??= new ResourceManager(
            "Pl0.Core.Pl0CoreMessages",
            typeof(Pl0CoreMessages).Assembly);
}
