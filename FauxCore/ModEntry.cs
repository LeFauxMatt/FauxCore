namespace LeFauxMods.Core;

using Common.Utilities;
using Services;
using Utilities;

/// <inheritdoc />
internal sealed class ModEntry : Mod
{
    private ThemeHelper themeHelper = null!;

    /// <inheritdoc />
    public override void Entry(IModHelper helper)
    {
        // Init
        Log.Init(this.Monitor);
        this.themeHelper = ThemeHelper.Init(helper);
    }

    /// <inheritdoc />
    public override object GetApi(IModInfo mod) => new ModApi(mod, this.themeHelper);
}
