namespace LeFauxMods.Core.Integrations.GenericModConfigMenu;

using StardewModdingAPI;

/// <summary>Initializes a new instance of the <see cref="GenericModConfigMenuIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
internal sealed class GenericModConfigMenuIntegration(IModRegistry modRegistry) : ModIntegration<IGenericModConfigMenuApi>(modRegistry)
{
    public override string UniqueId => "spacechase0.GenericModConfigMenu";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(1, 14, 0);
}
