namespace LeFauxMods.Common.Services;

using Integrations.ContentPatcher;
using Integrations.GenericModConfigMenu;
using Models;
using StardewModdingAPI.Events;
using Utilities;

/// <summary>Managed saving and loading config files.</summary>
/// <typeparam name="TConfig">The mod configuration type.</typeparam>
internal abstract class ConfigManager<TConfig>
    where TConfig : class, new()
{
    private static GenericModConfigMenuIntegration? genericModConfigMenuIntegration;
    private readonly IModHelper helper;

    private TConfig? config;
    private bool initialized;

    protected ConfigManager(IModHelper helper, IManifest manifest)
    {
        this.helper = helper;
        genericModConfigMenuIntegration ??= new GenericModConfigMenuIntegration(manifest, helper.ModRegistry);
        var contentPatcherIntegration = new ContentPatcherIntegration(helper);
        if (contentPatcherIntegration.IsLoaded)
        {
            ModEvents.Subscribe<ConditionsApiReadyEventArgs>(this.OnConditionsApiReady);
            return;
        }

        helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
    }

    /// <summary>Gets the default config options.</summary>
    public virtual TConfig Default => new();

    /// <summary>Gets the existing config options.</summary>
    public virtual TConfig? Existing
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

    /// <summary>Gets the generic mod config menu integration.</summary>
    protected static GenericModConfigMenuIntegration GMCM => genericModConfigMenuIntegration!;

    /// <summary>Gets the config options.</summary>
    protected TConfig Config => this.config ??= this.Existing ?? this.Default;

    /// <summary>Perform initialization routine.</summary>
    public void Init()
    {
        if (this.initialized)
        {
            return;
        }

        this.initialized = true;
        ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(this.Config));
        this.SetupMenu();
    }

    /// <summary>Resets the configuration by reassigning to <see cref="TConfig" />.</summary>
    public void Reset()
    {
        this.config = this.Default;
        ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(this.Config));
    }

    /// <summary>Saves the provided config.</summary>
    /// <param name="newConfig">The config object to be saved.</param>
    public void Save(TConfig newConfig)
    {
        this.helper.WriteConfig(newConfig);
        this.helper.Data.WriteGlobalData("config", newConfig);
        this.config = newConfig;
        ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(this.Config));
    }

    protected virtual void SetupMenu()
    {
    }

    private void OnConditionsApiReady(ConditionsApiReadyEventArgs e) => this.Init();

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e) => this.Init();
}
