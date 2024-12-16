namespace LeFauxMods.Common.Interface;

using Models;

/// <summary>Represents a configuration with a log amount.</summary>
internal interface IConfigWithLogAmount
{
    /// <summary>Gets the amount of log messages to show.</summary>
    LogAmount LogAmount { get; }
}
