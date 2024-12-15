namespace LeFauxMods.Common.Integrations.ExpandedStorage;

using NetEscapades.EnumGenerators;

#pragma warning disable

/// <summary>Mod API for Expanded Storage.</summary>
public interface IExpandedStorageApi
{
}

/// <summary>Lid opening animation styles.</summary>
[EnumExtensions]
internal enum Animation
{
    /// <summary>The default animation style.</summary>
    None = 0,

    /// <summary>Constantly loop.</summary>
    Loop = 1
}
