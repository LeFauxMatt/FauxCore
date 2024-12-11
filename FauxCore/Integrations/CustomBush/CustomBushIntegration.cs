namespace LeFauxMods.Core.Integrations.CustomBush;

/// <summary>Initializes a new instance of the <see cref="CustomBushIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
internal sealed class CustomBushIntegration(IModRegistry modRegistry) : ModIntegration<ICustomBushApi>(modRegistry)
{
    /// <inheritdoc />
    public override string UniqueId => "furyx639.CustomBush";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
}