using LeFauxMods.Common.Integrations.FauxCore;

namespace LeFauxMods.Core.Services;

/// <inheritdoc />
public sealed class ModApi : IFauxCoreApi
{
    public ModApi(IModInfo mod) { }

    public void LoadLastSave(IConfigWithLastSave config) => ModState.ConfigWithLastSave = config;
}