namespace LeFauxMods.Common.Services;

using Models;
using Utilities;

/// <summary>Manages saving and loading config files.</summary>
/// <typeparam name="TConfig">The mod configuration type.</typeparam>
/// <param name="helper">Dependency for events, input, and content.</param>
internal sealed class ConfigHelper<TConfig>(IModHelper helper)
    where TConfig : class, new()
{
    /// <summary>Load the config options.</summary>
    /// <returns>Existing, backup, or new config options.</returns>
    public TConfig Load()
    {
        TConfig? config = null;

        // Load an existing config file
        try
        {
            config ??= helper.Data.ReadJsonFile<TConfig>("config.json");
        }
        catch
        {
            // ignored
        }

        // Try to restore a backup config file
        try
        {
            config ??= helper.Data.ReadGlobalData<TConfig>("config") ?? throw new InvalidOperationException();
        }
        catch
        {
            // ignored
        }

        // Generate a new config file
        config ??= new TConfig();
        return config;
    }

    /// <summary>Saves the provided config.</summary>
    /// <param name="config">The config object to be saved.</param>
    public void Save(TConfig config)
    {
        helper.WriteConfig(config);
        helper.Data.WriteGlobalData("config", config);
        ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(config));
    }
}
