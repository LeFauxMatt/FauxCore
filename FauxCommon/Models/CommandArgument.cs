namespace LeFauxMods.Common.Models;

internal readonly record struct CommandArgument(string Name, Func<string> GetDocumentation);