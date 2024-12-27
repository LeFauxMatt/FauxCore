using Microsoft.Xna.Framework;
using StardewValley.Objects;

namespace LeFauxMods.Common.Integrations.ColorfulChests;

#pragma warning disable

/// <summary>Mod API for Colorful Chest.</summary>
public interface IColorfulChestsApi
{
    /// <summary>Adds a method for replacing the color palette for a given chest.</summary>
    /// <param name="handler">The handler.</param>
    public void AddHandler(PaletteHandler handler);

    /// <summary>Removes a method for replacing the color palette for a given chest.</summary>
    /// <param name="handler">The handler.</param>
    public void RemoveHandler(PaletteHandler handler);
}

/// <summary>Represents a handler for adding a custom color palette for a chest.</summary>
/// <param name="chest">The chest instance of the color picker.</param>
/// <param name="palette">The custom color palette.</param>
/// <returns>Returns true if the chest was handled.</returns>
public delegate bool PaletteHandler(Chest chest, [NotNullWhen(true)] out Color[]? palette);
