using LeFauxMods.Common.Utilities;
using LeFauxMods.Core.Services;

namespace LeFauxMods.Core;

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
