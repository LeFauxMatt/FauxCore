using LeFauxMods.Common.Interface;
using LeFauxMods.Common.Models;
using LeFauxMods.Common.Utilities;

namespace LeFauxMods.Common.Services;

/// <summary>Manages saving and loading config files.</summary>
/// <typeparam name="TConfig">The mod configuration type.</typeparam>
/// <param name="helper">Dependency for events, input, and content.</param>
internal sealed class ConfigHelper<TConfig>(IModHelper helper)
    where TConfig : class, IModConfig<TConfig>, new()
{
    private TConfig? config;
    private TConfig? temp;

    public TConfig Config
    {
        get
        {
            if (this.config is not null)
            {
                return this.config;
            }

            // Load an existing config file
            try
            {
                this.config ??= helper.Data.ReadJsonFile<TConfig>("config.json");
            }
            catch (Exception ex)
            {
                Log.Debug("There was an issue loading config.json\nError: {0}", ex.Message);
            }

            // Try to restore a backup config file
            try
            {
                this.config ??= helper.Data.ReadGlobalData<TConfig>("config") ?? throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Log.Debug("There was an issue loading config.json\nError: {0}", ex.Message);
            }

            // Generate a new config file
            this.config ??= new TConfig();
            ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(this.config));
            Log.Info("Config Initialized\n{0}", this.config.GetSummary());
            return this.config;
        }
    }

    public TConfig Default { get; } = new();

    public TConfig Temp
    {
        get
        {
            if (this.temp is not null)
            {
                return this.temp;
            }

            this.temp = new TConfig();
            this.Config.CopyTo(this.temp);
            return this.temp;
        }
    }

    /// <summary>Resets the temp config.</summary>
    public void Reset() => this.Default.CopyTo(this.Temp);

    /// <summary>Saves the temp config.</summary>
    public void Save()
    {
        this.Temp.CopyTo(this.Config);
        helper.WriteConfig(this.Config);
        helper.Data.WriteGlobalData("config", this.Config);
        ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(this.Config));
        Log.Info("Config Saved\n{0}", this.Config.GetSummary());
    }
}