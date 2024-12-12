namespace LeFauxMods.Common.Integrations.StardewUI;

/// <summary>Initializes a new instance of the <see cref="StardewUiIntegration" /> class.</summary>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
internal sealed class StardewUiIntegration(IModRegistry modRegistry) : ModIntegration<IViewEngine>(modRegistry)
{
    public override string UniqueId => "focustense.StardewUI";
}
