using LeFauxMods.Common.Utilities;

namespace LeFauxMods.Common.Integrations;

/// <summary>Provides an integration point for using external mods' APIs.</summary>
/// <typeparam name="T">Interface for the external mod's API.</typeparam>
internal abstract class ModIntegration<T>
    where T : class
{
    private readonly Lazy<T?> modApi;

    /// <summary>Initializes a new instance of the <see cref="ModIntegration{T}" /> class.</summary>
    /// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
    /// <param name="required">Determines if the mod is required.</param>
    internal ModIntegration(IModRegistry modRegistry, bool required)
    {
        this.ModRegistry = modRegistry;
        this.modApi = new Lazy<T?>(() => this.ModRegistry.GetApi<T>(this.UniqueId));

        if (this.Version is not null && this.ModInfo?.Manifest.Version.IsOlderThan(this.Version) == false)
        {
            Log.Warn("Please update {0} to version {1} to enable compatibility.",
                this.ModInfo.Manifest.Name,
                this.Version.ToString());
        }

        if (!required)
        {
            return;
        }

        if (!modRegistry.IsLoaded(this.UniqueId))
        {
            Log.Warn("Missing required dependency: {0}", this.UniqueId);
        }
    }

    /// <summary>Gets a value indicating whether the mod is loaded.</summary>
    [MemberNotNullWhen(true, nameof(Api), nameof(ModInfo))]
    public bool IsLoaded =>
        this.ModRegistry.IsLoaded(this.UniqueId) &&
        (this.Version is null || this.ModInfo?.Manifest.Version.IsOlderThan(this.Version) != true);

    /// <summary>Gets metadata for this mod.</summary>
    public IModInfo? ModInfo => this.ModRegistry.Get(this.UniqueId);

    /// <summary>Gets the Unique Id for this mod.</summary>
    public abstract string UniqueId { get; }

    /// <summary>Gets the minimum supported version for this mod.</summary>
    public virtual ISemanticVersion? Version => null;

    /// <summary>Gets the Mod's API through SMAPI's standard interface.</summary>
    protected internal T? Api => this.IsLoaded ? this.modApi.Value : default;

    private IModRegistry ModRegistry { get; }
}