using LeFauxMods.Common.Models;

namespace LeFauxMods.Common.Interface;

/// <summary>Represents a configuration with a log amount.</summary>
internal interface IConfigWithLogAmount
{
    /// <summary>Gets or sets the amount of log messages to show.</summary>
    LogAmount LogAmount { get; set; }
}
