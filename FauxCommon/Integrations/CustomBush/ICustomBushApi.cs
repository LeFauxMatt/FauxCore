using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.GameData;
using StardewValley.TerrainFeatures;

namespace LeFauxMods.Common.Integrations.CustomBush;

#pragma warning disable

/// <summary>Mod API for Custom Bush.</summary>
public interface ICustomBushApi
{
    /// <summary>Determine if the bush is a custom bush.</summary>
    /// <param name="bush">The bush to check.</param>
    /// <returns>True if the bush is a custom bush.</returns>
    public bool IsCustomBush(Bush bush);

    /// <summary>Try to get the custom bush instance associated with the given bush.</summary>
    /// <param name="bush">The bush to check.</param>
    /// <param name="customBush">The resulting custom bush, if applicable.</param>
    /// <returns>True if a custom bush was found.</returns>
    public bool TryGetBush(Bush bush, [NotNullWhen(true)] out ICustomBush? customBush);

    /// <summary>Try to get the custom bush model associated with the given bush.</summary>
    /// <param name="bush">The bush to check.</param>
    /// <param name="customBushData">The resulting custom bush, if applicable.</param>
    /// <returns>True if a custom bush was found.</returns>
    public bool TryGetData(Bush bush, [NotNullWhen(true)] out ICustomBushData? customBushData);
}

/// <summary>Represents a distinct instance of a custom bush.</summary>
public interface ICustomBush
{
    /// <summary>Gets the condition pertaining to the bush's current season.</summary>
    public string? Condition { get; }

    /// <summary>Gets the bush data.</summary>
    public ICustomBushData Data { get; }

    /// <summary>Gets the custom bush's id.</summary>
    public string Id { get; }

    /// <summary>Gets a value indicating whether the bush is in season.</summary>
    public bool IsInSeason { get; }

    /// <summary>Gets the current shake off item (if any).</summary>
    public Item? Item { get; }

    /// <summary>Gets an offset to the bush sprite.</summary>
    public int SpriteOffset { get; }

    /// <summary>Gets the current bush stage.</summary>
    public ICustomBushStage Stage { get; }

    /// <summary>Gets the number of counted days in the stage.</summary>
    public int StageCounter { get; }

    /// <summary>Gets the bush's stage id.</summary>
    public string StageId { get; }

    /// <summary>Gets the bush's texture.</summary>
    public Texture2D Texture { get; }

    /// <summary>Tests a Game State Query condition, passing the relevant parameters.</summary>
    /// <param name="condition">The condition to test.</param>
    /// <returns>True if the condition is null or empty, or if the condition passes.</returns>
    bool TestCondition(string? condition);

    /// <summary>Try to produce an item drop.</summary>
    /// <param name="drop">The drop to produce.</param>
    /// <param name="item">The item produced from the drop.</param>
    /// <returns>True if the drop could be produced.</returns>
    bool TryProduceDrop(ICustomBushDrop drop, [NotNullWhen(true)] out Item? item);
}

/// <summary>Model used for custom bush data.</summary>
public interface ICustomBushData
{
    /// <summary>Gets a list of conditions where any have to match for the bush to produce items.</summary>
    public List<string> ConditionsToProduce { get; }

    /// <summary>Gets the description of the bush.</summary>
    public string Description { get; }

    /// <summary>Gets the display name of the bush.</summary>
    public string DisplayName { get; }

    /// <summary>Gets a unique identifier for the custom bush.</summary>
    public string Id { get; }

    /// <summary>Gets the initial bush stage.</summary>
    public string InitialStage { get; }

    /// <summary>Gets the rules which override the locations that custom bushes can be planted in.</summary>
    public List<PlantableRule> PlantableLocationRules { get; }

    /// <summary>Gets all the growth stages.</summary>
    public ICustomBushStages Stages { get; }

    /// <summary>Try to get the age to mature.</summary>
    /// <returns>The age in which <see cref="ConditionsToProduce" /> will return true; otherwise, 0.</returns>
    public int GetAgeToMature();

    /// <summary>Try to get the seasons.</summary>
    /// <returns>The seasons in which <see cref="ConditionsToProduce" /> will return true.</returns>
    public List<Season> GetSeasons();
}

/// <inheritdoc />
public interface ICustomBushStages : IDictionary<string, ICustomBushStage>
{
    /// <summary>Try to get sequential stages starting from an initial stage.</summary>
    /// <returns>Stages that go from initial to last without any repeated elements.</returns>
    public IEnumerable<(ICustomBushStage Stage, string Id, int Counter)> GetSequentialStages();
}

/// <summary>Represents a stage that a custom bush can change into.</summary>
public interface ICustomBushStage
{
    /// <summary>Gets the bush type for the growth stage.</summary>
    public BushType BushType { get; }

    /// <summary>A game state query which indicates whether the bush should increment the days in its current stage.</summary>
    public string? ConditionToProgress { get; }

    /// <summary>Gets the default texture used when planted indoors.</summary>
    public string IndoorTexture { get; }

    /// <summary>Gets or sets the items produced by this custom bush.</summary>
    public ICustomBushDrops ItemsProduced { get; }

    /// <summary>Gets the rules for progressing.</summary>
    public ICustomBushProgressRules ProgressRules { get; }

    /// <summary>Gets the coordinates for the sprite at this stage.</summary>
    public Point SpritePosition { get; }

    /// <summary>Gets the texture of the tea bush.</summary>
    public string Texture { get; }
}

/// <inheritdoc />
public interface ICustomBushProgressRules : IList<ICustomBushProgressRule>
{
    /// <summary>Try to get the minimum stage counter for any condition with the given stage id.</summary>
    /// <param name="id">The stage id, or any.</param>
    /// <returns>Returns the stage counter or -1 if the stage id cannot be reached.</returns>
    public int GetCounterTo(string? id);
}

/// <summary>Represents rules for a custom bush to progress into a different stage.</summary>
public interface ICustomBushProgressRule
{
    /// <summary>A game state query which determines whether the bush should grow to the indicated stage.</summary>
    public string? Condition { get; }

    /// <summary>Gets a unique ID for this entry within the current list.</summary>
    public string? Id { get; }

    /// <summary>Gets the items dropped when the conditions for this rule are met.</summary>
    public ICustomBushDrops ItemsDropped { get; }

    /// <summary>Gets the stage that the bush should grow to.</summary>
    public string StageId { get; }

    /// <summary>Get the stage counter condition for this rule.</summary>
    /// <returns>Returns the stage counter.</returns>
    public int GetStageCounter();
}

/// <inheritdoc />
public interface ICustomBushDrops : IList<ICustomBushDrop>
{
}

/// <inheritdoc />
public interface ICustomBushDrop : ISpawnItemData
{
    /// <summary>A game state query which indicates whether the item should be added. Defaults to always added.</summary>
    public string? Condition { get; }

    /// <summary>Gets a unique ID for this entry within the current list.</summary>
    public string? Id { get; }

    /// <summary>Gets a value indicating whether the drop can replace an existing item.</summary>
    public bool ReplaceItem { get; }

    /// <summary>Gets an offset to the bush sprite when this item is produced.</summary>
    public int SpriteOffset { get; }

    /// <summary>Try to get the chance for the drop based on its condition.</summary>
    /// <returns>Returns the chance of the drop being produced.</returns>
    public float GetChance();

    /// <summary>Try to get the earliest day for the drop based on its condition.</summary>
    /// <returns>Returns the first day of the drop being produced.</returns>
    public int GetDay();
}

/// <summary>Represents the bush types.</summary>
public enum BushType
{
    /// <summary>Small bush</summary>
    Small = Bush.smallBush, // 0

    /// <summary>Medium bush</summary>
    Medium = Bush.mediumBush, // 1

    /// <summary>Large bush</summary>
    Large = Bush.largeBush, // 2

    /// <summary>Tea bush</summary>
    Tea = Bush.greenTeaBush, // 3

    /// <summary>Walnut bush</summary>
    Walnut = Bush.walnutBush // 4
}