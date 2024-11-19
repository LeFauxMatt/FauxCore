namespace LeFauxMods.Core.Integrations;

using System;

/// <summary>Provides an integration point for using external mods' APIs.</summary>
/// <typeparam name="T">Interface for the external mod's API.</typeparam>
internal abstract class ModIntegration<T>
    where T : class
{
    private readonly Lazy<T?> modApi;

    /// <summary>Initializes a new instance of the <see cref="ModIntegration{T}" /> class.</summary>
    /// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
    internal ModIntegration(IModRegistry modRegistry)
    {
        this.ModRegistry = modRegistry;
        this.modApi = new Lazy<T?>(() => this.ModRegistry.GetApi<T>(this.UniqueId));
    }

    /// <summary>Gets a value indicating whether the mod is loaded.</summary>
    [MemberNotNullWhen(true, nameof(Api), nameof(ModInfo))]
    public bool IsLoaded =>
        this.ModRegistry.IsLoaded(this.UniqueId)
        && (this.Version is null || this.ModInfo?.Manifest.Version.IsOlderThan(this.Version) != true);

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
