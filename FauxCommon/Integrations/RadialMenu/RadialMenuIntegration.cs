namespace LeFauxMods.Common.Integrations.RadialMenu;

using StardewModdingAPI;

/// <summary>Initializes a new instance of the <see cref="RadialMenuIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
internal sealed class RadialMenuIntegration(IModRegistry modRegistry) : ModIntegration<IRadialMenuApi>(modRegistry)
{
    public override string UniqueId => "focustense.RadialMenu";
}
