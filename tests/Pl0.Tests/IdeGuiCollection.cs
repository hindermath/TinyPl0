using Xunit;

namespace Pl0.Tests;

// Terminal.Gui nutzt einen globalen Theme-Builder; diese Tests duerfen ihn nicht parallel aendern.
// Terminal.Gui uses a global theme builder, so these tests must not change it in parallel.
[CollectionDefinition("IDE GUI", DisableParallelization = true)]
public sealed class IdeGuiCollection;
