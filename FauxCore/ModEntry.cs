namespace LeFauxMods.Core;

using Common.Utilities;
using Services;

/// <inheritdoc />
internal sealed class ModEntry : Mod
{
    /// <inheritdoc />
    public override void Entry(IModHelper helper) =>
        // Init
        Log.Init(this.Monitor);

    /// <inheritdoc />
    public override object GetApi(IModInfo mod) => new ModApi(mod);
}
