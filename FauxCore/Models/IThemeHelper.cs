﻿namespace LeFauxMods.Core.Models;

/// <summary>Handles palette swaps for theme compatibility.</summary>
public interface IThemeHelper
{
    /// <summary>Adds a new asset to theme helper using the provided texture data and asset name.</summary>
    /// <param name="path">The game content path for the asset.</param>
    /// <param name="data">The raw texture data for the asset.</param>
    public void AddAsset(string path, IRawTextureData data);
}
