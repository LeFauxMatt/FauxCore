namespace LeFauxMods.Common.Integrations.FauxCore;

/// <summary>Api for shared functionality between mods.</summary>
public interface IFauxCoreApi
{
    public void LoadLastSave(IConfigWithLastSave config);
}

public interface IConfigWithLastSave
{
    /// <summary>Gets or sets the last save that was loaded.</summary>
    public string LastSave { get; set; }
}