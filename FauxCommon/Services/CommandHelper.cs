using System.Globalization;
using System.Text;
using LeFauxMods.Common.Models;
using LeFauxMods.Common.Utilities;

namespace LeFauxMods.Common.Services;

/// <summary>Manages console commands.</summary>
internal sealed class CommandHelper
{
    private readonly Dictionary<string, Command> commands = new(StringComparer.OrdinalIgnoreCase);
    private readonly Func<string> getUnknown;
    private readonly string mainCommand;

    public CommandHelper(
        IModHelper helper,
        string mainCommand,
        Func<string> getDocumentation,
        Func<string> getUnknown)
    {
        this.mainCommand = mainCommand;
        this.getUnknown = getUnknown;
        helper.ConsoleCommands.Add(mainCommand, getDocumentation(), this.OnCommand);
    }

    public string HelpText
    {
        get
        {
            var sb = new StringBuilder();
            if (this.commands.TryGetValue("help", out var helpCommand))
            {
                sb.AppendLine(helpCommand.Documentation).AppendLine();
            }

            foreach (var (name, command) in this.commands)
            {
                if (name.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (!command.Arguments.Any())
                {
                    sb.AppendLine(CultureInfo.InvariantCulture, $"{this.mainCommand} {name}")
                        .AppendLine(CultureInfo.InvariantCulture, $"{command.Documentation}");
                    continue;
                }

                sb.AppendLine().Append(CultureInfo.InvariantCulture, $"{this.mainCommand} {name}");
                foreach (var (argName, _) in command.Arguments)
                {
                    sb.Append(CultureInfo.InvariantCulture, $" [{argName}]");
                }

                sb.AppendLine()
                    .AppendLine(CultureInfo.InvariantCulture, $"{command.Documentation}")
                    .AppendLine();

                foreach (var (argName, getDescription) in command.Arguments)
                {
                    sb.AppendLine(CultureInfo.InvariantCulture, $"{argName:25} {getDescription()}");
                }
            }

            return sb.ToString();
        }
    }

    /// <summary>Add a new console command.</summary>
    /// <param name="command">The command to add.</param>
    /// <param name="getDocumentation">The documentation.</param>
    /// <param name="arguments">The command arguments.</param>
    public CommandHelper AddCommand(
        string command,
        Func<string> getDocumentation,
        params CommandArgument[]? arguments)
    {
        this.commands.Add(
            command,
            new Command(command, getDocumentation, arguments?.ToList() ?? []));
        return this;
    }

    private void OnCommand(string arg1, string[] arg2)
    {
        switch (arg2.ElementAtOrDefault(0)?.ToLower(CultureInfo.InvariantCulture).Trim())
        {
            case { } name when this.commands.TryGetValue(name, out var command):
                var arguments = Enumerable.Range(0, Math.Min(arg2.Length - 1, command.Arguments.Count))
                    .ToDictionary(
                        i => command.Arguments[i].Name,
                        i => arg2[i + 1]);

                ModEvents.Publish(new CommandReceivedEventArgs(command.Name, arguments));
                return;

            default:
                Log.Info(this.getUnknown());
                return;
        }
    }

    private readonly record struct Command(
        string Name,
        Func<string> GetDocumentation,
        List<CommandArgument> Arguments)
    {
        public string Documentation => this.GetDocumentation();
    }
}