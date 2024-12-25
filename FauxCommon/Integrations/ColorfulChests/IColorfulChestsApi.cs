using Microsoft.Xna.Framework;
using StardewValley.Objects;

namespace LeFauxMods.Common.Integrations.ColorfulChests;

#pragma warning disable

/// <summary>Mod API for Colorful Chest.</summary>
public interface IColorfulChestsApi
{
    /// <summary>
    ///     Adds a custom color palette for a chest.
    /// </summary>
    /// <param name="handler">The handler.</param>
    public void AddHandler(PaletteHandler handler);
}

public delegate bool PaletteHandler(Chest chest, [NotNullWhen(true)] out Color[]? palette);
