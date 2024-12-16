namespace LeFauxMods.Common.Services;

using Models;
using Utilities;

/// <summary>Managed saving and loading config files.</summary>
/// <typeparam name="TConfig">The mod configuration type.</typeparam>
/// <param name="helper">Dependency for events, input, and content.</param>
internal sealed class ConfigManager<TConfig>(IModHelper helper)
    where TConfig : class, new()
{
    /// <summary>Saves the provided config.</summary>
    /// <param name="newConfig">The config object to be saved.</param>
    public void Save(TConfig newConfig)
    {
        helper.WriteConfig(newConfig);
        helper.Data.WriteGlobalData("config", newConfig);
        ModEvents.Publish(new ConfigChangedEventArgs<TConfig>(newConfig));
    }

    /// <summary>Try to load existing config options.</summary>
    /// <param name="config">The config options.</param>
    /// <returns>true if the config options could be loaded.</returns>
    public bool TryLoad([NotNullWhen(true)] out TConfig? config)
    {
        // Try to load an existing config file
        try
        {
            config = helper.ReadConfig<TConfig>();
            return true;
        }
        catch
        {
            // Could not read config file from the folder
        }

        // Try to restore a backup config file
        try
        {
            config = helper.Data.ReadGlobalData<TConfig>("config") ?? throw new InvalidOperationException();
            return true;
        }
        catch
        {
            // Could not read config file from the global data
        }

        config = null;
        return false;
    }
}
