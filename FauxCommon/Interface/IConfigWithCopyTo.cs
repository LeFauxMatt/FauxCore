namespace LeFauxMods.Common.Interface;

/// <summary>Represents a configuration with a copy to method.</summary>
/// <typeparam name="TConfig">The mod configuration type.</typeparam>
internal interface IConfigWithCopyTo<in TConfig> where TConfig : class
{
    /// <summary>
    ///     Copies the values from this instance to another instance.
    /// </summary>
    /// <param name="other">The other config instance.</param>
    public void CopyTo(TConfig other);
}