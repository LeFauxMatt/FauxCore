using LeFauxMods.Common.Interface;
using LeFauxMods.Common.Models;

namespace LeFauxMods.Common.Integrations.BetterChests;

/// <summary>Initializes a new instance of the <see cref="StorageOptions" /> class.</summary>
/// <param name="dictionaryModel">The backing dictionary.</param>
internal sealed class StorageOptions(IDictionaryModel? dictionaryModel = null)
    : DictionaryDataModel(dictionaryModel ?? new DictionaryModel()), IStorageOptions
{
    /// <inheritdoc />
    public RangeOption AccessChest
    {
        get => this.Get(nameof(this.AccessChest), StringToRangeOption);
        set => this.Set(nameof(this.AccessChest), value, RangeOptionToString);
    }

    /// <inheritdoc />
    public int AccessChestPriority
    {
        get => this.Get(nameof(this.AccessChestPriority), StringToInt);
        set => this.Set(nameof(this.AccessChestPriority), value, IntToString);
    }

    /// <inheritdoc />
    public FeatureOption AutoOrganize
    {
        get => this.Get(nameof(this.AutoOrganize), StringToFeatureOption);
        set => this.Set(nameof(this.AutoOrganize), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption CarryChest
    {
        get => this.Get(nameof(this.CarryChest), StringToFeatureOption);
        set => this.Set(nameof(this.CarryChest), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption CategorizeChest
    {
        get => this.Get(nameof(this.CategorizeChest), StringToFeatureOption);
        set => this.Set(nameof(this.CategorizeChest), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption CategorizeChestBlockItems
    {
        get => this.Get(nameof(this.CategorizeChestBlockItems), StringToFeatureOption);
        set => this.Set(nameof(this.CategorizeChestBlockItems), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption CategorizeChestIncludeStacks
    {
        get => this.Get(nameof(this.CategorizeChestIncludeStacks), StringToFeatureOption);
        set => this.Set(nameof(this.CategorizeChestIncludeStacks), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public string CategorizeChestSearchTerm
    {
        get => this.Get(nameof(this.CategorizeChestSearchTerm));
        set => this.Set(nameof(this.CategorizeChestSearchTerm), value);
    }

    /// <inheritdoc />
    public FeatureOption ChestFinder
    {
        get => this.Get(nameof(this.ChestFinder), StringToFeatureOption);
        set => this.Set(nameof(this.ChestFinder), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption CollectItems
    {
        get => this.Get(nameof(this.CollectItems), StringToFeatureOption);
        set => this.Set(nameof(this.CollectItems), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption ConfigureChest
    {
        get => this.Get(nameof(this.ConfigureChest), StringToFeatureOption);
        set => this.Set(nameof(this.ConfigureChest), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public RangeOption CookFromChest
    {
        get => this.Get(nameof(this.CookFromChest), StringToRangeOption);
        set => this.Set(nameof(this.CookFromChest), value, RangeOptionToString);
    }

    /// <inheritdoc />
    public RangeOption CraftFromChest
    {
        get => this.Get(nameof(this.CraftFromChest), StringToRangeOption);
        set => this.Set(nameof(this.CraftFromChest), value, RangeOptionToString);
    }

    /// <inheritdoc />
    public int CraftFromChestDistance
    {
        get => this.Get(nameof(this.CraftFromChestDistance), StringToInt);
        set => this.Set(nameof(this.CraftFromChestDistance), value, IntToString);
    }

    /// <inheritdoc />
    public string Description
    {
        get => this.Get(nameof(this.Description));
        set => this.Set(nameof(this.Description), value);
    }

    /// <inheritdoc />
    public string DisplayName
    {
        get => this.Get(nameof(this.DisplayName));
        set => this.Set(nameof(this.DisplayName), value);
    }

    /// <inheritdoc />
    public FeatureOption HslColorPicker
    {
        get => this.Get(nameof(this.HslColorPicker), StringToFeatureOption);
        set => this.Set(nameof(this.HslColorPicker), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption InventoryTabs
    {
        get => this.Get(nameof(this.InventoryTabs), StringToFeatureOption);
        set => this.Set(nameof(this.InventoryTabs), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption OpenHeldChest
    {
        get => this.Get(nameof(this.OpenHeldChest), StringToFeatureOption);
        set => this.Set(nameof(this.OpenHeldChest), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public ChestMenuOption ResizeChest
    {
        get => this.Get(nameof(this.ResizeChest), StringToChestMenuOption);
        set => this.Set(nameof(this.ResizeChest), value, ChestMenuOptionToString);
    }

    /// <inheritdoc />
    public int ResizeChestCapacity
    {
        get => this.Get(nameof(this.ResizeChestCapacity), StringToInt);
        set => this.Set(nameof(this.ResizeChestCapacity), value, IntToString);
    }

    /// <inheritdoc />
    public FeatureOption SearchItems
    {
        get => this.Get(nameof(this.SearchItems), StringToFeatureOption);
        set => this.Set(nameof(this.SearchItems), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption ShopFromChest
    {
        get => this.Get(nameof(this.ShopFromChest), StringToFeatureOption);
        set => this.Set(nameof(this.ShopFromChest), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption SortInventory
    {
        get => this.Get(nameof(this.SortInventory), StringToFeatureOption);
        set => this.Set(nameof(this.SortInventory), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public string SortInventoryBy
    {
        get => this.Get(nameof(this.SortInventoryBy));
        set => this.Set(nameof(this.SortInventoryBy), value);
    }

    /// <inheritdoc />
    public RangeOption StashToChest
    {
        get => this.Get(nameof(this.StashToChest), StringToRangeOption);
        set => this.Set(nameof(this.StashToChest), value, RangeOptionToString);
    }

    /// <inheritdoc />
    public int StashToChestDistance
    {
        get => this.Get(nameof(this.StashToChestDistance), StringToInt);
        set => this.Set(nameof(this.StashToChestDistance), value, IntToString);
    }

    /// <inheritdoc />
    public StashPriority StashToChestPriority
    {
        get => this.Get(nameof(this.StashToChestPriority), StringToStashPriority);
        set => this.Set(nameof(this.StashToChestPriority), value, StashPriorityToString);
    }

    /// <inheritdoc />
    public string StorageIcon
    {
        get => this.Get(nameof(this.StorageIcon));
        set => this.Set(nameof(this.StorageIcon), value);
    }

    /// <inheritdoc />
    public FeatureOption StorageInfo
    {
        get => this.Get(nameof(this.StorageInfo), StringToFeatureOption);
        set => this.Set(nameof(this.StorageInfo), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public FeatureOption StorageInfoHover
    {
        get => this.Get(nameof(this.StorageInfoHover), StringToFeatureOption);
        set => this.Set(nameof(this.StorageInfoHover), value, FeatureOptionToString);
    }

    /// <inheritdoc />
    public string StorageName
    {
        get => this.Get(nameof(this.StorageName));
        set => this.Set(nameof(this.StorageName), value);
    }

    /// <inheritdoc />
    protected override string Prefix => "furyx639.BetterChests/";

    private static string ChestMenuOptionToString(ChestMenuOption value) =>
        value is not ChestMenuOption.Default ? value.ToStringFast() : string.Empty;

    private static string FeatureOptionToString(FeatureOption value) =>
        value is not FeatureOption.Default ? value.ToStringFast() : string.Empty;

    private static string RangeOptionToString(RangeOption value) =>
        value is not RangeOption.Default ? value.ToStringFast() : string.Empty;

    private static string StashPriorityToString(StashPriority value) =>
        value is not StashPriority.Default ? value.ToStringFast() : string.Empty;

    private static ChestMenuOption StringToChestMenuOption(string value) =>
        ChestMenuOptionExtensions.TryParse(value, out var chestMenuOption) ? chestMenuOption : ChestMenuOption.Default;

    private static FeatureOption StringToFeatureOption(string value) =>
        FeatureOptionExtensions.TryParse(value, out var featureOption) ? featureOption : FeatureOption.Default;

    private static RangeOption StringToRangeOption(string value) =>
        RangeOptionExtensions.TryParse(value, out var rangeOption) ? rangeOption : RangeOption.Default;

    private static StashPriority StringToStashPriority(string value) =>
        StashPriorityExtensions.TryParse(value, out var stashPriority) ? stashPriority : StashPriority.Default;
}
