namespace LeFauxMods.Common.Integrations.FauxCore;

/// <summary>Initializes a new instance of the <see cref="FauxCoreIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
internal sealed class FauxCoreIntegration(IModRegistry modRegistry) : ModIntegration<IFauxCoreApi>(modRegistry)
{
    /// <inheritdoc />
    public override string UniqueId => "furyx639.FauxCore";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(2, 0, 0);
}
