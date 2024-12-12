namespace LeFauxMods.Core.Services;

using Common.Integrations.FauxCore;
using Models;

/// <inheritdoc />
public sealed class ModApi(IModInfo mod, IThemeHelper themeHelper) : IFauxCoreApi
{
    /// <inheritdoc />
    public void AddAsset(string path, IRawTextureData data) => themeHelper.AddAsset(path, data);
}
