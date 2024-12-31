namespace LeFauxMods.Common.Integrations.StardewUi;

/// <summary>Initializes a new instance of the <see cref="StardewUiIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
/// <param name="required">Determines if the mod is required.</param>
internal sealed class StardewUiIntegration(IModRegistry modRegistry, bool required = false)
    : ModIntegration<IViewEngine>(modRegistry, required)
{
    /// <inheritdoc />
    public override string UniqueId => "focustense.StardewUI";

    /// <inheritdoc />
    public override ISemanticVersion? Version { get; } = new SemanticVersion(0, 5, 0);
}