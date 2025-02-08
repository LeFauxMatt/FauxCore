namespace LeFauxMods.Common.Integrations.StarControl;

/// <summary>Initializes a new instance of the <see cref="StarControlIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
/// <param name="required">Determines if the mod is required.</param>
internal sealed class StarControlIntegration(IModRegistry modRegistry, bool required = false)
    : ModIntegration<IStarControlApi>(modRegistry, required)
{
    /// <inheritdoc />
    public override string UniqueId => "focustense.RadialMenu";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(0, 2, 2);
}