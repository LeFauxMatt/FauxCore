namespace LeFauxMods.Common.Interface;

/// <summary>Represents the mod's configuration.</summary>
/// <typeparam name="TConfig">The mod configuration type.</typeparam>
internal interface IModConfig<in TConfig> where TConfig : class
{
    /// <summary>
    ///     Copies the values from this instance to another instance.
    /// </summary>
    /// <param name="other">The other config instance.</param>
    public void CopyTo(TConfig other);

    /// <summary>Get a summary of the mod's configuration options.</summary>
    /// <returns>Returns the summary.</returns>
    public string GetSummary();
}