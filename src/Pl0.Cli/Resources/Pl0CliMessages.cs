using System.Resources;

namespace Pl0.Cli;

/// <summary>
/// Bietet Zugriff auf den ResourceManager für CLI-Meldungen.
/// </summary>
internal static class Pl0CliMessages
{
    /// <summary>
    /// Statischer Cache für den ResourceManager.
    /// </summary>
    private static ResourceManager? _resourceManager;

    /// <summary>
    /// Gibt den ResourceManager für Pl0CliMessages zurück.
    /// </summary>
    internal static ResourceManager ResourceManager =>
        _resourceManager ??= new ResourceManager(
            "Pl0.Cli.Pl0CliMessages",
            typeof(Pl0CliMessages).Assembly);
}
