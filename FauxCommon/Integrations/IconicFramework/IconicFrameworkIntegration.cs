namespace LeFauxMods.Common.Integrations.IconicFramework;

/// <summary>Initializes a new instance of the <see cref="IconicFrameworkIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
/// <param name="required">Determines if the mod is required.</param>
internal sealed class IconicFrameworkIntegration(IModRegistry modRegistry, bool required = false)
    : ModIntegration<IIconicFrameworkApi>(modRegistry, required)
{
    /// <inheritdoc />
    public override string UniqueId => "furyx639.ToolbarIcons";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(3, 0, 2);
}