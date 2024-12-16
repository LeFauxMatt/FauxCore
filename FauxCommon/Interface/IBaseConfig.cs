namespace LeFauxMods.Common.Interface;

/// <summary>Represents the base configuration.</summary>
internal interface IBaseConfig
{
    /// <summary>Copy config value from this to another.</summary>
    /// <param name="other">The config instance to copy to.</param>
    public void CopyTo(IBaseConfig other);
}
