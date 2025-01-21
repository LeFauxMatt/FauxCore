namespace LeFauxMods.Common.Integrations.ColorfulChests;

/// <summary>Initializes a new instance of the <see cref="ColorfulChestsIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
/// <param name="required">Determines if the mod is required.</param>
internal sealed class ColorfulChestsIntegration(IModRegistry modRegistry, bool required = false)
    : ModIntegration<IColorfulChestsApi>(modRegistry, required)
{
    /// <inheritdoc />
    public override string UniqueId => "furyx639.ColorfulChests";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(1, 1, 0);
}