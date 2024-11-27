namespace LeFauxMods.Core;

using LeFauxMods.Core.Utilities;

internal sealed class ModEntry : Mod
{
    public override void Entry(IModHelper helper)
    {
        // Init
        _ = new Log(this.Monitor);
        _ = new ThemeHelper(helper);
    }
}
