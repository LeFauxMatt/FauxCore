namespace LeFauxMods.Common.Integrations.BetterChests;

/// <summary>Initializes a new instance of the <see cref="BetterChestsIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
internal sealed class BetterChestsIntegration(IModRegistry modRegistry) : ModIntegration<IBetterChestsApi>(modRegistry)
{
    /// <inheritdoc />
    public override string UniqueId => "furyx639.BetterChests";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
}
