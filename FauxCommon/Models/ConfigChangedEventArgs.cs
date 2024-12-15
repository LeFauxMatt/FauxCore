namespace LeFauxMods.Common.Models;

/// <summary>Initializes a new instance of the <see cref="ConfigChangedEventArgs{TConfig}" /> class.</summary>
/// <typeparam name="TConfig">The config type.</typeparam>
/// <param name="config">The config.</param>
internal sealed class ConfigChangedEventArgs<TConfig>(TConfig config) : EventArgs
{
    /// <summary>Gets the current config options.</summary>
    public TConfig Config { get; } = config;
}
