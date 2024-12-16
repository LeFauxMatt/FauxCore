namespace LeFauxMods.Common.Models;

using NetEscapades.EnumGenerators;

/// <summary>The amount of debugging information that will be logged to the console.</summary>
[EnumExtensions]
public enum LogAmount
{
    /// <summary>Less debugging information will be logged to the console.</summary>
    Less = 0,

    /// <summary>More debugging information will be logged to the console.</summary>
    More = 2
}
