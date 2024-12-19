using System.Globalization;
using LeFauxMods.Common.Interface;

namespace LeFauxMods.Common.Utilities;

internal sealed class Log
{
    private static Log? instance;
    private readonly IConfigWithLogAmount? config;
    private readonly IMonitor monitor;
    private string lastMessage = string.Empty;

    private Log(IMonitor m, IConfigWithLogAmount? c)
    {
        this.monitor = m;
        this.config = c;
    }

    /// <summary>Logs an alert message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="hudType">The hud type to show.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Alert(string message, int hudType = HUDMessage.error_type, params object?[]? args) =>
        instance?.Raise(
            message,
            LogLevel.Alert,
            false,
            hudType,
            args);

    /// <summary>Logs a debug message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Debug(string message, params object?[]? args) =>
        instance?.Raise(
            message,
            LogLevel.Debug,
            false,
            0,
            args);

    /// <summary>Logs an error message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Error(string message, params object?[]? args) =>
        instance?.Raise(
            message,
            LogLevel.Error,
            false,
            0,
            args);

    /// <summary>Logs an info message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Info(string message, params object?[]? args) =>
        instance?.Raise(
            message,
            LogLevel.Info,
            false,
            0,
            args);

    public static void Init(IMonitor monitor, IConfigWithLogAmount? config = null) =>
        instance ??= new Log(monitor, config);

    /// <summary>Logs a trace message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Trace(string message, params object?[]? args) =>
        instance?.Raise(
            message,
            LogLevel.Trace,
            false,
            0,
            args);

    /// <summary>Logs a trace message to the console only once.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void TraceOnce(string message, params object?[]? args) =>
        instance?.Raise(
            message,
            LogLevel.Trace,
            true,
            0,
            args);

    /// <summary>Logs a warn message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void Warn(string message, params object?[]? args) =>
        instance?.Raise(
            message,
            LogLevel.Warn,
            false,
            0,
            args);

    /// <summary>Logs a warn message to the console only once.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public static void WarnOnce(string message, params object?[]? args) =>
        instance?.Raise(
            message,
            LogLevel.Warn,
            true,
            0,
            args);

    private void Raise(
        string message,
        LogLevel level,
        bool once,
        int hudType = 0,
        object?[]? args = null)
    {
        if (args != null)
        {
            message = string.Format(CultureInfo.InvariantCulture, message, args);
        }

        // Prevent consecutive duplicate messages
        if (message == this.lastMessage)
        {
            return;
        }

        this.lastMessage = message;

#if RELEASE
        // Reduced logging in release mode
        if (level is not (LogLevel.Error or LogLevel.Alert) && this.config?.LogAmount is not Models.LogAmount.More)
        {
            return;
        }
#endif

        if (once)
        {
            this.monitor.LogOnce(message, level);
            return;
        }

        this.monitor.Log(message, level);

        if (level == LogLevel.Alert || hudType != 0)
        {
            Game1.addHUDMessage(new HUDMessage(message, hudType));
        }
    }
}
