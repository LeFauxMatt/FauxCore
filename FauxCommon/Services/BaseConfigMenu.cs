using LeFauxMods.Common.Integrations.GenericModConfigMenu;
using LeFauxMods.Common.Interface;

namespace LeFauxMods.Common.Services;

/// <summary>Responsible for handling the mod configuration menu.</summary>
/// <typeparam name="TConfig">The mod configuration type.</typeparam>
internal abstract class BaseConfigMenu<TConfig>
    where TConfig : class, IModConfig<TConfig>, new()
{
    /// <summary>Initializes a new instance of the <see cref="BaseConfigMenu{TConfig}" /> class.</summary>
    /// <param name="helper">Dependency for events, input, and content.</param>
    /// <param name="manifest">Dependency for accessing mod manifest.</param>
    protected BaseConfigMenu(IModHelper helper, IManifest manifest)
    {
        this.Helper = helper;
        this.Manifest = manifest;
        this.GMCM = new GenericModConfigMenuIntegration(manifest, helper.ModRegistry);

        if (!this.GMCM.IsLoaded)
        {
            return;
        }

        this.SetupMenu();
    }

    protected IGenericModConfigMenuApi Api => this.GMCM.Api!;

    protected abstract TConfig Config { get; }

    protected abstract ConfigHelper<TConfig> ConfigHelper { get; }

    protected GenericModConfigMenuIntegration GMCM { get; }

    protected IModHelper Helper { get; }

    protected IManifest Manifest { get; }

    public virtual void SetupMenu()
    {
        this.GMCM.Register(this.Reset, this.Save);
        this.SetupOptions();
    }

    protected internal abstract void SetupOptions();

    protected internal virtual void Reset()
    {
        this.ConfigHelper.Reset();
#if DEBUG
        this.SetupMenu();
#endif
    }

    protected internal virtual void Save() => this.ConfigHelper.Save();
}