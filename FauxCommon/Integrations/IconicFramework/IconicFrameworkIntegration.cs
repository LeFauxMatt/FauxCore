namespace LeFauxMods.Common.Integrations.IconicFramework;

using StardewModdingAPI;

/// <summary>Initializes a new instance of the <see cref="IconicFrameworkIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
internal sealed class IconicFrameworkIntegration(IModRegistry modRegistry) : ModIntegration<IIconicFrameworkApi>(modRegistry)
{
    public override string UniqueId => "furyx639.ToolbarIcons";
}
