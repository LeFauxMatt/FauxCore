using Microsoft.Xna.Framework.Graphics;
using StardewValley.GameData;
using StardewValley.TerrainFeatures;

namespace LeFauxMods.Common.Integrations.CustomBush;

#pragma warning disable

/// <summary>Mod API for Custom Bush.</summary>
public interface ICustomBushApi
{
    /// <summary>Retrieves all the custom bush data.</summary>
    /// <returns>Each object represents an instance of the <see cref="ICustomBushData" /> model.</returns>
    public IEnumerable<ICustomBushData> GetAllBushes();

    /// <summary>Determines if the given Bush instance is a custom bush.</summary>
    /// <param name="bush">The bush instance to check.</param>
    /// <returns><c>true</c> if the bush is a custom bush.</returns>
    public bool IsCustomBush(Bush bush);

    /// <summary>Determines if the given Bush instance is a custom bush and in season.</summary>
    /// <param name="bush">The bush instance to check.</param>
    /// <returns><c>true</c> if the bush is a custom bush and in season.</returns>
    public bool IsInSeason(Bush bush);

    /// <summary>Tries to get the custom bush model associated with the given bush.</summary>
    /// <param name="bush">The bush.</param>
    /// <param name="customBush">
    ///     When this method returns, contains the custom bush associated with the given bush, if found;
    ///     otherwise, it contains null.
    /// </param>
    /// <param name="id">When this method returns, contains the id of the custom bush, if found; otherwise, it contains null.</param>
    /// <returns><c>true</c> if the custom bush associated with the given bush is found.</returns>
    public bool TryGetBush(Bush bush, [NotNullWhen(true)] out ICustomBushData? customBush,
        [NotNullWhen(true)] out string? id);

    /// <summary>Tries to get the shake off item.</summary>
    /// <param name="bush">The bush.</param>
    /// <param name="item">The shake off item.</param>
    /// <returns>Returns <c>true</c> if the custom bush currently has an item to collect.</returns>
    public bool TryGetShakeOffItem(Bush bush, [NotNullWhen(true)] out Item? item);

    /// <summary>Tries to get the cached mod data for the given bush.</summary>
    /// <param name="bush">The bush.</param>
    /// <param name="itemId">The cached id of the item to be produced.</param>
    /// <param name="itemQuality">The cached quality of the item to be produced.</param>
    /// <param name="itemStack">The cached stack size of the item to be produced.</param>
    /// <param name="condition">The cached condition that determines how long the item can be collected for.</param>
    /// <returns><c>true</c> if there is valid cached data for the given bush.</returns>
    public bool TryGetModData(
        Bush bush,
        [NotNullWhen(true)] out string? itemId,
        out int itemQuality,
        out int itemStack,
        out string? condition);

    /// <summary>Tries to get the currently relevant texture for the given bush.</summary>
    /// <param name="bush">The bush.</param>
    /// <param name="texture">The bush's texture.</param>
    /// <returns><c>true</c> if a custom bush is associated with the given bush and a texture is found.</returns>
    public bool TryGetTexture(Bush bush, [NotNullWhen(true)] out Texture2D? texture);
}

/// <summary>Model used for custom bushes.</summary>
public interface ICustomBushData
{
    /// <summary>Gets the age needed to produce.</summary>
    public int AgeToProduce { get; }

    public BushType BushType { get; set; }

    /// <summary>Gets a list of conditions where any have to match for the bush to produce items.</summary>
    public List<string> ConditionsToProduce { get; }

    /// <summary>Gets the day of month to begin producing.</summary>
    public int DayToBeginProducing { get; }

    /// <summary>Gets the description of the bush.</summary>
    public string Description { get; }

    /// <summary>Gets the display name of the bush.</summary>
    public string DisplayName { get; }

    /// <summary>Gets a unique identifier for the custom bush.</summary>
    public string Id { get; }

    /// <summary>Gets the default texture used when planted indoors.</summary>
    public string IndoorTexture { get; }

    /// <summary>Gets or sets the items produced by this custom bush.</summary>
    public List<ICustomBushDrop> ItemsProduced { get; set; }

    /// <summary>Gets the rules which override the locations that custom bushes can be planted in.</summary>
    public List<PlantableRule> PlantableLocationRules { get; }

    /// <summary>Gets the season in which this bush will produce its drops.</summary>
    public List<Season> Seasons { get; }

    /// <summary>Gets the texture of the tea bush.</summary>
    public string Texture { get; }

    /// <summary>Gets the row index for the custom bush's sprites.</summary>
    public int TextureSpriteRow { get; }
}

/// <inheritdoc />
public interface ICustomBushDrop : ISpawnItemData
{
    /// <summary>Gets the probability that the item will be produced.</summary>
    public float Chance { get; }

    /// <summary>A game state query which indicates whether the item should be added. Defaults to always added.</summary>
    public string? Condition { get; }

    /// <summary>
    ///     An ID for this entry within the current list (not the item itself, which is
    ///     <see cref="P:StardewValley.GameData.GenericSpawnItemData.ItemId" />). This only needs to be unique within the
    ///     current
    ///     list. For a custom entry, you should use a globally unique ID which includes your mod ID like
    ///     <c>ExampleMod.Id_ItemName</c>.
    /// </summary>
    public string? Id { get; }

    /// <summary>Gets the specific season when the item can be produced.</summary>
    public Season? Season { get; }

    /// <summary>Gets or sets an offset to the texture sprite which the item is produced.</summary>
    public int SpriteOffset { get; set; }
}

/// <summary>Represents the bush sizes.</summary>
public enum BushType
{
    /// <summary>Small bush</summary>
    Small = Bush.smallBush,

    /// <summary>Medium bush</summary>
    Medium = Bush.mediumBush,

    /// <summary>Large bush</summary>
    Large = Bush.largeBush,

    /// <summary>Tea bush</summary>
    Tea = Bush.greenTeaBush,

    /// <summary>Walnut bush</summary>
    Walnut = Bush.walnutBush
}