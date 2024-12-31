namespace LeFauxMods.Common.Integrations.RadialMenu;

/// <summary>Initializes a new instance of the <see cref="RadialMenuIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
/// <param name="required">Determines if the mod is required.</param>
internal sealed class RadialMenuIntegration(IModRegistry modRegistry, bool required = false)
    : ModIntegration<IRadialMenuApi>(modRegistry, required)
{
    /// <inheritdoc />
    public override string UniqueId => "focustense.RadialMenu";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(0, 2, 2);
}