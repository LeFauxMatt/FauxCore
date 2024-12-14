namespace LeFauxMods.Common.Integrations.BetterChests;

#pragma warning disable

/// <summary>Mod API for Better Chests.</summary>
public interface IBetterChestsApi
{
    /// <summary>Adds config options for the storage type.</summary>
    /// <param name="manifest">Dependency for accessing mod manifest.</param>
    /// <param name="pageId">The page id if a new page should be added, or null.</param>
    /// <param name="getTitle">A function to return the page title, or null.</param>
    /// <param name="options">The options to configure.</param>
    public void AddConfigOptions(IManifest manifest, string? pageId, Func<string>? getTitle, IStorageOptions options);
}

/// <summary>Configurable options for a storage container.</summary>
public interface IStorageOptions
{
    /// <summary>Gets the description of the container.</summary>
    string Description { get; }

    /// <summary>Gets the name of the container.</summary>
    string DisplayName { get; }

    /// <summary>Gets or sets a value indicate if chests can be remotely accessed.</summary>
    public RangeOption AccessChest { get; set; }

    /// <summary>Gets or sets a value indicating the priority that chests will be accessed.</summary>
    public int AccessChestPriority { get; set; }

    /// <summary>Gets or sets a value indicating if the chest can be automatically organized overnight.</summary>
    public FeatureOption AutoOrganize { get; set; }

    /// <summary>Gets or sets a value indicating if the chest can be carried by the player.</summary>
    public FeatureOption CarryChest { get; set; }

    /// <summary>Gets or sets a value indicating if can have categories added to it, and which items can be added.</summary>
    public FeatureOption CategorizeChest { get; set; }

    /// <summary>Gets or sets a value indicating whether uncategorized items will be blocked.</summary>
    public FeatureOption CategorizeChestBlockItems { get; set; }

    /// <summary>Gets or sets a value indicating whether categorization includes existing stacks by default.</summary>
    public FeatureOption CategorizeChestIncludeStacks { get; set; }

    /// <summary>Gets or sets the search term for categorizing items in the chest.</summary>
    public string CategorizeChestSearchTerm { get; set; }

    /// <summary>Gets or sets a value indicating whether chests  in the current location can be searched for.</summary>
    public FeatureOption ChestFinder { get; set; }

    /// <summary>Gets or sets a value indicating if the chest can collect dropped items.</summary>
    public FeatureOption CollectItems { get; set; }

    /// <summary>Gets or sets a value indicating whether chests can be configured.</summary>
    public FeatureOption ConfigureChest { get; set; }

    /// <summary>Gets or sets a value indicating if the chest can be remotely cooked from.</summary>
    public RangeOption CookFromChest { get; set; }

    /// <summary>Gets or sets a value indicating if the chest can be remotely crafted from.</summary>
    public RangeOption CraftFromChest { get; set; }

    /// <summary>Gets or sets a value indicating the distance in tiles that the chest can be remotely crafted from.</summary>
    public int CraftFromChestDistance { get; set; }

    /// <summary>Gets or sets a value indicating if the color picker will be replaced by an hsl color picker.</summary>
    public FeatureOption HslColorPicker { get; set; }

    /// <summary>Gets or sets a value indicating if inventory tabs will be added to the chest menu.</summary>
    public FeatureOption InventoryTabs { get; set; }

    /// <summary>Gets or sets a value indicating if the chest can be opened while it's being carried in the players inventory.</summary>
    public FeatureOption OpenHeldChest { get; set; }

    /// <summary>Gets or sets the menu for the chest.</summary>
    public ChestMenuOption ResizeChest { get; set; }

    /// <summary>Gets or sets the chest's carrying capacity.</summary>
    public int ResizeChestCapacity { get; set; }

    /// <summary>Gets or sets a value indicating if a search bar will be added to the chest menu.</summary>
    public FeatureOption SearchItems { get; set; }

    /// <summary>Gets or sets a value indicating if the shops can use items from the chest.</summary>
    public FeatureOption ShopFromChest { get; set; }

    /// <summary>Gets or sets a value indicating if storage can be sorted using a custom key.</summary>
    public FeatureOption SortInventory { get; set; }

    /// <summary>Gets or sets what the storage will be sorted by.</summary>
    public string SortInventoryBy { get; set; }

    /// <summary>Gets or sets a value indicating if the chest can be remotely stashed into.</summary>
    public RangeOption StashToChest { get; set; }

    /// <summary>Gets or sets a value indicating the distance in tiles that the chest can be remotely stashed into.</summary>
    public int StashToChestDistance { get; set; }

    /// <summary>Gets or sets a value indicating the priority that chests will be stashed into.</summary>
    public StashPriority StashToChestPriority { get; set; }

    /// <summary>Gets or sets an icon to use for the storage.</summary>
    public string StorageIcon { get; set; }

    /// <summary>Gets or sets a value indicating whether info will be displayed about the chest.</summary>
    public FeatureOption StorageInfo { get; set; }

    /// <summary>Gets or sets a value indicating whether info will be displayed on hovering over a storage.</summary>
    public FeatureOption StorageInfoHover { get; set; }

    /// <summary>Gets or sets the name of the chest.</summary>
    public string StorageName { get; set; }
}

/// <summary>Indicates at what range a feature will be enabled.</summary>
public enum RangeOption
{
    /// <summary>Range is inherited from a parent config.</summary>
    Default = 0,

    /// <summary>Feature is disabled.</summary>
    Disabled = 1,

    /// <summary>Feature is enabled for storages in the player's inventory.</summary>
    Inventory = 2,

    /// <summary>Feature is enabled for storages in the player's current location.</summary>
    Location = 3,

    /// <summary>Feature is enabled for any storage in an accessible location to the player.</summary>
    World = 9
}

/// <summary>Indicates if a feature is enabled, disabled, or will inherit from a parent config.</summary>
public enum FeatureOption
{
    /// <summary>Option is inherited from a parent config.</summary>
    Default = 0,

    /// <summary>Feature is disabled.</summary>
    Disabled = 1,

    /// <summary>Feature is enabled.</summary>
    Enabled = 2
}

/// <summary>The possible values for Chest capacity.</summary>
public enum ChestMenuOption
{
    /// <summary>Capacity is inherited by a parent config.</summary>
    Default = 0,

    /// <summary>Vanilla slots.</summary>
    Disabled = 1,

    /// <summary>Resize to 9 item slots.</summary>
    Small = 9,

    /// <summary>Resize to 36 item slots.</summary>
    Medium = 36,

    /// <summary>Resize to 70 item slots.</summary>
    Large = 70
}

/// <summary>The possible values for Stash to Chest Priority.</summary>
public enum StashPriority
{
    /// <summary>Represents the default priority.</summary>
    Default = 0,

    /// <summary>Represents the lowest priority.</summary>
    Lowest = -3,

    /// <summary>Represents a lower priority.</summary>
    Lower = -2,

    /// <summary>Represents a low priority.</summary>
    Low = -1,

    /// <summary>Represents a high priority.</summary>
    High = 1,

    /// <summary>Represents a higher priority.</summary>
    Higher = 2,

    /// <summary>Represents the highest priority.</summary>
    Highest = 3
}

/// <summary>
///     Extensions for the <see cref="IStorageOptions" /> interface.
/// </summary>
internal static class BetterChestsExtensions
{
    /// <summary>Copy storage options.</summary>
    /// <param name="from">The storage options to copy from.</param>
    /// <param name="to">The storage options to copy to.</param>
    public static void CopyTo(this IStorageOptions from, IStorageOptions to) =>
        from.ForEachOption(
            (name, option) =>
            {
                switch (option)
                {
                    case FeatureOption featureOption:
                        to.SetOption(name, featureOption);
                        return;

                    case RangeOption rangeOption:
                        to.SetOption(name, rangeOption);
                        return;

                    case ChestMenuOption chestMenuOption:
                        to.SetOption(name, chestMenuOption);
                        return;

                    case StashPriority stashPriority:
                        to.SetOption(name, stashPriority);
                        return;

                    case string stringValue:
                        to.SetOption(name, stringValue);
                        return;

                    case int intValue:
                        to.SetOption(name, intValue);
                        return;
                }
            });

    /// <summary>Executes the specified action for each option in the class.</summary>
    /// <param name="options">The storage options.</param>
    /// <param name="action">The action to be performed for each option.</param>
    public static void ForEachOption(this IStorageOptions options, Action<string, object> action)
    {
        action(nameof(options.AccessChest), options.AccessChest);
        action(nameof(options.AccessChestPriority), options.AccessChestPriority);
        action(nameof(options.AutoOrganize), options.AutoOrganize);
        action(nameof(options.CarryChest), options.CarryChest);
        action(nameof(options.CategorizeChest), options.CategorizeChest);
        action(nameof(options.CategorizeChestBlockItems), options.CategorizeChestBlockItems);
        action(nameof(options.CategorizeChestSearchTerm), options.CategorizeChestSearchTerm);
        action(nameof(options.CategorizeChestIncludeStacks), options.CategorizeChestIncludeStacks);
        action(nameof(options.ChestFinder), options.ChestFinder);
        action(nameof(options.CollectItems), options.CollectItems);
        action(nameof(options.ConfigureChest), options.ConfigureChest);
        action(nameof(options.CookFromChest), options.CookFromChest);
        action(nameof(options.CraftFromChest), options.CraftFromChest);
        action(nameof(options.CraftFromChestDistance), options.CraftFromChestDistance);
        action(nameof(options.HslColorPicker), options.HslColorPicker);
        action(nameof(options.InventoryTabs), options.InventoryTabs);
        action(nameof(options.OpenHeldChest), options.OpenHeldChest);
        action(nameof(options.ResizeChest), options.ResizeChest);
        action(nameof(options.ResizeChestCapacity), options.ResizeChestCapacity);
        action(nameof(options.SearchItems), options.SearchItems);
        action(nameof(options.ShopFromChest), options.ShopFromChest);
        action(nameof(options.SortInventory), options.SortInventory);
        action(nameof(options.SortInventoryBy), options.SortInventoryBy);
        action(nameof(options.StashToChest), options.StashToChest);
        action(nameof(options.StashToChestDistance), options.StashToChestDistance);
        action(nameof(options.StashToChestPriority), options.StashToChestPriority);
        action(nameof(options.StorageIcon), options.StorageIcon);
        action(nameof(options.StorageInfo), options.StorageInfo);
        action(nameof(options.StorageInfoHover), options.StorageInfoHover);
        action(nameof(options.StorageName), options.StorageName);
    }

    /// <summary>Sets the value of a specific option in the storage options.</summary>
    /// <param name="options">The storage options.</param>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value to set.</param>
    public static void SetOption(this IStorageOptions options, string name, FeatureOption value)
    {
        switch (name)
        {
            case nameof(options.AutoOrganize):
                options.AutoOrganize = value;
                return;

            case nameof(options.CarryChest):
                options.CarryChest = value;
                return;

            case nameof(options.CategorizeChest):
                options.CategorizeChest = value;
                return;

            case nameof(options.CategorizeChestBlockItems):
                options.CategorizeChestBlockItems = value;
                return;

            case nameof(options.CategorizeChestIncludeStacks):
                options.CategorizeChestIncludeStacks = value;
                return;

            case nameof(options.ChestFinder):
                options.ChestFinder = value;
                return;

            case nameof(options.CollectItems):
                options.CollectItems = value;
                return;

            case nameof(options.ConfigureChest):
                options.ConfigureChest = value;
                return;

            case nameof(options.HslColorPicker):
                options.HslColorPicker = value;
                return;

            case nameof(options.InventoryTabs):
                options.InventoryTabs = value;
                return;

            case nameof(options.OpenHeldChest):
                options.OpenHeldChest = value;
                return;

            case nameof(options.SearchItems):
                options.SearchItems = value;
                return;

            case nameof(options.ShopFromChest):
                options.ShopFromChest = value;
                return;

            case nameof(options.SortInventory):
                options.SortInventory = value;
                return;

            case nameof(options.StorageInfo):
                options.StorageInfo = value;
                return;

            case nameof(options.StorageInfoHover):
                options.StorageInfoHover = value;
                return;

            default:
                throw new ArgumentOutOfRangeException(name);
        }
    }

    /// <summary>Sets the value of a specific option in the storage options.</summary>
    /// <param name="options">The storage options.</param>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value to set.</param>
    public static void SetOption(this IStorageOptions options, string name, RangeOption value)
    {
        switch (name)
        {
            case nameof(options.AccessChest):
                options.AccessChest = value;
                return;

            case nameof(options.CookFromChest):
                options.CookFromChest = value;
                return;

            case nameof(options.CraftFromChest):
                options.CraftFromChest = value;
                return;

            case nameof(options.StashToChest):
                options.StashToChest = value;
                return;

            default:
                throw new ArgumentOutOfRangeException(name);
        }
    }

    /// <summary>Sets the value of a specific option in the storage options.</summary>
    /// <param name="options">The storage options.</param>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value to set.</param>
    public static void SetOption(this IStorageOptions options, string name, string value)
    {
        switch (name)
        {
            case nameof(options.CategorizeChestSearchTerm):
                options.CategorizeChestSearchTerm = value;
                return;

            case nameof(options.SortInventoryBy):
                options.SortInventoryBy = value;
                return;

            case nameof(options.StorageIcon):
                options.StorageIcon = value;
                return;

            case nameof(options.StorageName):
                options.StorageName = value;
                return;

            default:
                throw new ArgumentOutOfRangeException(name);
        }
    }

    /// <summary>Sets the value of a specific option in the storage options.</summary>
    /// <param name="options">The storage options.</param>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value to set.</param>
    public static void SetOption(this IStorageOptions options, string name, int value)
    {
        switch (name)
        {
            case nameof(options.AccessChestPriority):
                options.AccessChestPriority = value;
                return;

            case nameof(options.CraftFromChestDistance):
                options.CraftFromChestDistance = value;
                return;

            case nameof(options.ResizeChestCapacity):
                options.ResizeChestCapacity = value;
                return;

            case nameof(options.StashToChestDistance):
                options.StashToChestDistance = value;
                return;

            default:
                throw new ArgumentOutOfRangeException(name);
        }
    }

    /// <summary>Sets the value of a specific option in the storage options.</summary>
    /// <param name="options">The storage options.</param>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value to set.</param>
    public static void SetOption(this IStorageOptions options, string name, ChestMenuOption value)
    {
        switch (name)
        {
            case nameof(options.ResizeChest):
                options.ResizeChest = value;
                return;

            default:
                throw new ArgumentOutOfRangeException(name);
        }
    }

    /// <summary>Sets the value of a specific option in the storage options.</summary>
    /// <param name="options">The storage options.</param>
    /// <param name="name">The name of the option.</param>
    /// <param name="value">The value to set.</param>
    public static void SetOption(this IStorageOptions options, string name, StashPriority value)
    {
        switch (name)
        {
            case nameof(options.StashToChestPriority):
                options.StashToChestPriority = value;
                return;

            default:
                throw new ArgumentOutOfRangeException(name);
        }
    }
}
