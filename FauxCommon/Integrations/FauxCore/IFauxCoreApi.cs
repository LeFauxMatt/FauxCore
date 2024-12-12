namespace LeFauxMods.Common.Integrations.FauxCore;

/// <summary>Api for shared functionality between mods.</summary>
public interface IFauxCoreApi
{
    /// <summary>Adds a new asset to theme helper using the provided texture data and asset name.</summary>
    /// <param name="path">The game content path for the asset.</param>
    /// <param name="data">The raw texture data for the asset.</param>
    public void AddAsset(string path, IRawTextureData data);
}
