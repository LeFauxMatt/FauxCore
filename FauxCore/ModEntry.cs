namespace LeFauxMods.Core;

using LeFauxMods.Core.Services;

internal sealed class ModEntry : Mod
{
    public override void Entry(IModHelper helper)
    {
        // Init
        _ = new Log(this.Monitor);
    }
}
