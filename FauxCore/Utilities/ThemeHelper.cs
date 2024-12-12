namespace LeFauxMods.Core.Utilities;

using Common.Integrations.ContentPatcher;
using Common.Services;
using Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI.Events;

internal sealed class ThemeHelper
{
    private static readonly Dictionary<Point[], Color> VanillaPalette = new()
    {
        // Outside edge of frame
        { [new Point(17, 369), new Point(104, 469), new Point(118, 483)], new Color(91, 43, 42) },

        // Inner frame color
        { [new Point(18, 370), new Point(105, 471), new Point(116, 483)], new Color(220, 123, 5) },

        // Dark shade of inner frame
        { [new Point(19, 371), new Point(106, 475), new Point(115, 475)], new Color(177, 78, 5) },

        // Dark shade of menu background
        { [new Point(20, 372), new Point(28, 378), new Point(22, 383)], new Color(228, 174, 110) },

        // Menu background
        { [new Point(21, 373), new Point(26, 377), new Point(21, 381)], new Color(255, 210, 132) },

        // Highlight of menu button
        { [new Point(104, 471), new Point(111, 470), new Point(117, 480)], new Color(247, 186, 0) }
    };

    private static ThemeHelper instance = null!;
    private readonly Dictionary<IAssetName, Asset> cachedAssets = [];
    private readonly IModHelper helper;
    private readonly Dictionary<Color, Color> paletteSwap = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="ThemeHelper" /> class.
    /// </summary>
    public ThemeHelper(IModHelper helper)
    {
        // Init
        instance = this;
        this.helper = helper;

        // Events
        helper.Events.Content.AssetRequested += this.OnAssetRequested;
        helper.Events.Content.AssetsInvalidated += this.OnAssetsInvalidated;
        EventManager.Subscribe<ConditionsApiReadyEventArgs>(this.OnConditionsApiReady);
    }

    /// <summary>Adds a new asset to theme helper using the provided texture data and asset name.</summary>
    /// <param name="path">The game content path for the asset.</param>
    /// <param name="data">The raw texture data for the asset.</param>
    public static void AddAsset(string path, IRawTextureData data)
    {
        var assetName = instance.helper.GameContent.ParseAssetName(path);
        if (!instance.cachedAssets.TryAdd(assetName, new Asset(data)))
        {
            Log.Trace("Error, conflicting key {0} found in ThemeHelper. Asset not added.", assetName.Name);
            return;
        }

        Log.Trace("Adding asset to theme helper: {0}", path);
    }

    private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
    {
        if (!this.cachedAssets.TryGetValue(e.NameWithoutLocale, out var asset))
        {
            return;
        }

        if (asset.Texture is not null && !asset.Dirty)
        {
            e.LoadFrom(() => asset.Texture, AssetLoadPriority.Exclusive);
            return;
        }

        asset.Texture ??= new Texture2D(Game1.spriteBatch.GraphicsDevice, asset.Raw.Width, asset.Raw.Height);
        asset.Texture.SetData(
            asset.Raw.Data.Select(color => this.paletteSwap.GetValueOrDefault(color, color)).ToArray());
        asset.Dirty = false;
        e.LoadFrom(() => asset.Texture, AssetLoadPriority.Exclusive);
    }

    private void OnAssetsInvalidated(object? sender, AssetsInvalidatedEventArgs e)
    {
        if (e.NamesWithoutLocale.Any(assetName => assetName.IsEquivalentTo("LooseSprites/Cursors")))
        {
            this.RefreshPalette();
        }

        foreach (var assetName in e.NamesWithoutLocale)
        {
            if (assetName.IsEquivalentTo("LooseSprites/Cursors"))
            {
                this.RefreshPalette();
                continue;
            }

            if (this.cachedAssets.TryGetValue(assetName, out var asset))
            {
                asset.Dirty = true;
            }
        }
    }

    private void OnConditionsApiReady(ConditionsApiReadyEventArgs e) => this.RefreshPalette();

    private void RefreshPalette()
    {
        var data = new Color[Game1.mouseCursors.Width * Game1.mouseCursors.Height];
        Game1.mouseCursors.GetData(data);

        var newPalette = new Dictionary<Color, Color>();
        foreach (var (points, color) in VanillaPalette)
        {
            newPalette[color] = points
                .Select(point => data[point.X + (point.Y * Game1.mouseCursors.Width)])
                .GroupBy(sample => sample)
                .OrderByDescending(group => group.Count())
                .First()
                .Key;
        }

        if (newPalette.Count == this.paletteSwap.Count && !newPalette.Except(this.paletteSwap).Any())
        {
            return;
        }

        this.paletteSwap.Clear();
        foreach (var (key, value) in newPalette)
        {
            this.paletteSwap[key] = value;
        }

        foreach (var assetName in this.cachedAssets.Keys)
        {
            _ = this.helper.GameContent.InvalidateCache(assetName);
        }
    }

    private record struct Asset(IRawTextureData Raw, Texture2D? Texture = null, bool Dirty = true);
}
