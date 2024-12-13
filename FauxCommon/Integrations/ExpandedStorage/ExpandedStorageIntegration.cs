namespace LeFauxMods.Common.Integrations.ExpandedStorage;

/// <summary>Initializes a new instance of the <see cref="ExpandedStorageIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
internal sealed class ExpandedStorageIntegration(IModRegistry modRegistry)
    : ModIntegration<IExpandedStorageApi>(modRegistry)
{
    /// <inheritdoc />
    public override string UniqueId => "furyx639.ExpandedStorage";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(3, 0, 0);
}
