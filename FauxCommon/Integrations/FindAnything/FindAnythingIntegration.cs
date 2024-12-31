namespace LeFauxMods.Common.Integrations.FindAnything;

/// <summary>Initializes a new instance of the <see cref="FindAnythingIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
/// <param name="required">Determines if the mod is required.</param>
internal sealed class FindAnythingIntegration(IModRegistry modRegistry, bool required = false)
    : ModIntegration<IFindAnythingApi>(modRegistry, required)
{
    /// <inheritdoc />
    public override string UniqueId => "furyx639.FindAnything";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(1, 0, 0, "beta.1");
}