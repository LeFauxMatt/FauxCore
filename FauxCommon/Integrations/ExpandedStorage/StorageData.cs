using LeFauxMods.Common.Interface;
using LeFauxMods.Common.Models;
using Microsoft.Xna.Framework;

namespace LeFauxMods.Common.Integrations.ExpandedStorage;

/// <summary>Initializes a new instance of the <see cref="StorageData" /> class.</summary>
/// <param name="dictionaryModel">The backing dictionary.</param>
internal sealed class StorageData(IDictionaryModel? dictionaryModel = null)
    : DictionaryDataModel(dictionaryModel ?? new DictionaryModel())
{
    /// <summary>Gets or sets the lid opening animation style.</summary>
    public Animation Animation
    {
        get => this.Get(nameof(this.Animation), StringToAnimation);
        set => this.Set(nameof(this.Animation), value, AnimationToString);
    }

    /// <summary>Gets or sets the sound to play when the lid closing animation plays.</summary>
    public string CloseNearbySound
    {
        get => this.Get(nameof(this.CloseNearbySound), "doorCreakReverse");
        set => this.Set(nameof(this.CloseNearbySound), value);
    }

    /// <summary>Gets or sets the number of frames in the lid animation.</summary>
    public int Frames
    {
        get => this.Get(nameof(this.Frames), StringToInt, 1);
        set => this.Set(nameof(this.Frames), value, IntToString);
    }

    /// <summary>Gets or sets the global inventory id.</summary>
    public string? GlobalInventoryId
    {
        get => this.Get(nameof(this.GlobalInventoryId));
        set => this.Set(nameof(this.GlobalInventoryId), value ?? string.Empty);
    }

    /// <summary>Gets or sets a value indicating whether the storage is a fridge.</summary>
    public bool IsFridge
    {
        get => this.Get(nameof(this.IsFridge), StringToBool);
        set => this.Set(nameof(this.IsFridge), value, BoolToString);
    }

    /// <summary>Gets or sets any mod data that should be added to the chest on creation.</summary>
    public Dictionary<string, string>? ModData
    {
        get => this.Get(nameof(this.ModData), StringToDict);
        set => this.Set(nameof(this.ModData), value, DictToString);
    }

    /// <summary>
    ///     Gets or sets a value indicating whether the storage will play its lid opening animation when the player is
    ///     nearby.
    /// </summary>
    public bool OpenNearby
    {
        get => this.Get(nameof(this.OpenNearby), StringToBool);
        set => this.Set(nameof(this.OpenNearby), value, BoolToString);
    }

    /// <summary>Gets or sets the sound to play when the lid opening animation plays.</summary>
    public string OpenNearbySound
    {
        get => this.Get(nameof(this.OpenNearbySound), "doorCreak");
        set => this.Set(nameof(this.OpenNearbySound), value);
    }

    /// <summary>Gets or sets the sound to play when the storage is opened.</summary>
    public string OpenSound
    {
        get => this.Get(nameof(this.OpenSound), "openChest");
        set => this.Set(nameof(this.OpenSound), value);
    }

    /// <summary>Gets or sets the sound to play when storage is placed.</summary>
    public string PlaceSound
    {
        get => this.Get(nameof(this.PlaceSound), "axe");
        set => this.Set(nameof(this.PlaceSound), value);
    }

    /// <summary>Gets or sets a value indicating whether player color is enabled.</summary>
    public bool PlayerColor
    {
        get => this.Get(nameof(this.PlayerColor), StringToBool);
        set => this.Set(nameof(this.PlayerColor), value, BoolToString);
    }

    /// <summary>Gets or sets a color to apply to the tinted layer.</summary>
    public Color[] TintOverride
    {
        get => this.Get(nameof(this.TintOverride), StringToArray(StringToColor), []);
        set => this.Set(nameof(this.TintOverride), value, ArrayToString<Color>(ColorToString));
    }

    /// <inheritdoc />
    protected override string Prefix => "furyx639.ExpandedStorage/";

    private static string AnimationToString(Animation value) =>
        value is not Animation.None ? value.ToStringFast() : string.Empty;

    private static Animation StringToAnimation(string value) =>
        !AnimationExtensions.TryParse(value, out var animation) ? default : animation;
}
