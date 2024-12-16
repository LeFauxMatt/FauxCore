namespace LeFauxMods.Common.Services;

using Integrations.ContentPatcher;
using Integrations.GenericModConfigMenu;
using Interface;
using Models;
using StardewModdingAPI.Events;
using Utilities;

/// <summary>Managed saving and loading config files.</summary>
/// <typeparam name="TConfig">The mod configuration type.</typeparam>
internal abstract class BaseConfigManager<TConfig>
    where TConfig : class, IBaseConfig, new()
{
    private static GenericModConfigMenuIntegration? genericModConfigMenuIntegration;
    private readonly IModHelper helper;

    private TConfig? config;
    private bool initialized;

    protected BaseConfigManager(IModHelper helper, IManifest manifest)
    {
        this.helper = helper;
        this.Manifest = manifest;
        genericModConfigMenuIntegration ??= new GenericModConfigMenuIntegration(manifest, helper.ModRegistry);
        var contentPatcherIntegration = new ContentPatcherIntegration(helper);
        if (contentPatcherIntegration.IsLoaded)
        {
            ModEvents.Subscribe<ConditionsApiReadyEventArgs>(this.OnConditionsApiReady);
            return;
        }

        helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
    }

    /// <summary>Gets the generic mod config menu integration.</summary>
    protected static GenericModConfigMenuIntegration GMCM => genericModConfigMenuIntegration!;

    /// <summary>Gets the config options.</summary>
    protected TConfig Config => this.config ??= this.Existing ?? this.Default;

    /// <summary>Gets the default config options.</summary>
    protected virtual TConfig Default => new();

    /// <summary>Gets the existing config options.</summary>
    protected virtual TConfig? Existing
    {
        get
        {
            // Try to load an existing config file
            try
            {
                return this.helper.ReadConfig<TConfig>();
            }
            catch
            {
                // Could not read config file from the folder
            }

            // Try to restore a backup config file
            try
            {
                return this.helper.Data.ReadGlobalData<TConfig>("config") ?? throw new InvalidOperationException();
            }
            catch
            {
                // Could not read config file from the global data
            }

            return null;
        }
    }

    /// <summary>Gets the mod's manifest.</summary>
    protected IManifest Manifest { get; }

    /// <summary>Resets the configuration by reassigning to <see cref="TConfig" />.</summary>
    protected void Reset()
    {
        this.config = this.Default;
        ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(this.Config));
    }

    /// <summary>Saves the provided config.</summary>
    /// <param name="newConfig">The config object to be saved.</param>
    protected void Save(TConfig newConfig)
    {
        this.helper.WriteConfig(newConfig);
        this.helper.Data.WriteGlobalData("config", newConfig);
        newConfig.CopyTo(this.Config);
        ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(this.Config));
    }

    protected virtual void SetupMenu()
    {
    }

    private void Init()
    {
        if (this.initialized)
        {
            return;
        }

        this.initialized = true;
        ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(this.Config));
        this.SetupMenu();
    }

    private void OnConditionsApiReady(ConditionsApiReadyEventArgs e) => this.Init();

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e) => this.Init();
}
