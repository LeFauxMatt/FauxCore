namespace LeFauxMods.Common.Models;

/// <inheritdoc />
internal sealed class CommandReceivedEventArgs(string command, Dictionary<string, string> args) : EventArgs
{
    /// <summary>Gets the command arguments.</summary>
    public Dictionary<string, string> Arguments { get; } = args;

    /// <summary>Gets the command.</summary>
    public string Command { get; } = command;
}