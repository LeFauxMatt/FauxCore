namespace LeFauxMods.Core.Services;

using System.Globalization;

/// <summary>Handles logging information to the console.</summary>
internal sealed class Log
{
    private static Log instance = null!;

    private readonly IMonitor monitor;

    private string lastMessage = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="Log"/> class.
    /// </summary>
    /// <param name="monitor"></param>
    public Log(IMonitor monitor)
    {
        instance = this;
        this.monitor = monitor;
    }

    private static IMonitor Monitor => instance.monitor;

    /// <summary>Logs an alert message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Alert(string message, object?[]? args = null) => Raise(message, LogLevel.Alert, false, args);

    /// <summary>Logs a debug message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Debug(string message, object?[]? args = null) => Raise(message, LogLevel.Debug, false, args);

    /// <summary>Logs an error message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Error(string message, params object?[]? args) => Raise(message, LogLevel.Error, false, args);

    /// <summary>Logs an info message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Info(string message, params object?[]? args) => Raise(message, LogLevel.Info, false, args);

    /// <summary>Logs a trace message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Trace(string message, params object?[]? args) => Raise(message, LogLevel.Trace, false, args);

    /// <summary>Logs a trace message to the console only once.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void TraceOnce(string message, params object?[]? args) => Raise(message, LogLevel.Trace, true, args);

    /// <summary>Logs a warn message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Warn(string message, params object?[]? args) => Raise(message, LogLevel.Warn, false, args);

    /// <summary>Logs a warn message to the console only once.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void WarnOnce(string message, params object?[]? args) => Raise(message, LogLevel.Warn, true, args);

    private static void Raise(string message, LogLevel level, bool once, object?[]? args = null)
    {
        if (args != null)
        {
            message = string.Format(CultureInfo.InvariantCulture, message, args);
        }

        // Prevent consecutive duplicate messages
        if (message == instance.lastMessage)
        {
            return;
        }

        instance.lastMessage = message;
#if DEBUG
        if (once)
        {
            Monitor.LogOnce(message, level);
            return;
        }

        Monitor.Log(message, level);
#else
        switch (level)
        {
            case LogLevel.Error:
            case LogLevel.Alert:
                if (once)
                {
                    Monitor.LogOnce(message, level);
                    break;
                }

                Monitor.Log(message, level);
                break;

            default:
                if (once)
                {
                    Monitor.LogOnce(message);
                    break;
                }

                Monitor.Log(message);
                break;
        }
#endif

        if (level == LogLevel.Alert)
        {
            Game1.showRedMessage(message);
        }
    }
}
