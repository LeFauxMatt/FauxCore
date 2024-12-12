namespace LeFauxMods.Core;

using Common.Integrations.ContentPatcher;
using Common.Utilities;
using Utilities;

/// <inheritdoc />
internal sealed class ModEntry : Mod
{
    /// <inheritdoc />
    public override void Entry(IModHelper helper)
    {
        // Init
        Log.Init(this.Monitor);
        _ = new ContentPatcherIntegration(helper);
        _ = new ThemeHelper(helper);
    }
}
