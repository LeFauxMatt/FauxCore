namespace LeFauxMods.Common.Services;

using Integrations.ContentPatcher;
using Models;
using StardewModdingAPI.Events;
using Utilities;

/// <summary>Managed saving and loading config files.</summary>
/// <typeparam name="TConfig">The mod configuration type.</typeparam>
internal abstract class ConfigManager<TConfig>
    where TConfig : class, new()
{
    private readonly IModHelper helper;

    private TConfig? config;
    private bool initialized;

    protected ConfigManager(IModHelper helper)
    {
        this.helper = helper;
        var contentPatcherIntegration = new ContentPatcherIntegration(helper);
        if (contentPatcherIntegration.IsLoaded)
        {
            ModEvents.Subscribe<ConditionsApiReadyEventArgs>(this.OnConditionsApiReady);
            return;
        }

        helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
    }

    /// <summary>Gets the config options.</summary>
    protected TConfig Config => this.config ??= this.LoadConfig();

    /// <summary>Perform initialization routine.</summary>
    public void Init()
    {
        if (this.initialized)
        {
            return;
        }

        this.initialized = true;
        ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(this.Config));
    }

    public virtual TConfig LoadConfig()
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

        return this.NewConfig();
    }

    /// <summary>Gets the default config options.</summary>
    /// <returns>Returns an instance of TConfig with default options applied.</returns>
    public virtual TConfig NewConfig() => new();

    /// <summary>Resets the configuration by reassigning to <see cref="TConfig" />.</summary>
    public void Reset()
    {
        this.config = this.NewConfig();
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

    private void OnConditionsApiReady(ConditionsApiReadyEventArgs e) => this.Init();

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e) => this.Init();
}
