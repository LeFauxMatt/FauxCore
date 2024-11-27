namespace LeFauxMods.Core.Services;

using System.Globalization;

/// <summary>Handles logging information to the console.</summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SimpleLogging"/> class.
/// </remarks>
/// <param name="monitor">Dependency used for monitoring and logging.</param>
internal sealed class SimpleLogging(IMonitor monitor)
{
    private string lastMessage = string.Empty;

    /// <summary>Logs an alert message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="hudType">The hud type to show.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Alert(string message, int hudType = HUDMessage.error_type, object?[]? args = null) => this.Raise(message, LogLevel.Alert, false, hudType, args);

    /// <summary>Logs a debug message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Debug(string message, object?[]? args = null) => this.Raise(message, LogLevel.Debug, false, 0, args);

    /// <summary>Logs an error message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Error(string message, params object?[]? args) => this.Raise(message, LogLevel.Error, false, 0, args);

    /// <summary>Logs an info message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Info(string message, params object?[]? args) => this.Raise(message, LogLevel.Info, false, 0, args);

    /// <summary>Logs a trace message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Trace(string message, params object?[]? args) => this.Raise(message, LogLevel.Trace, false, 0, args);

    /// <summary>Logs a trace message to the console only once.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void TraceOnce(string message, params object?[]? args) => this.Raise(message, LogLevel.Trace, true, 0, args);

    /// <summary>Logs a warn message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Warn(string message, params object?[]? args) => this.Raise(message, LogLevel.Warn, false, 0, args);

    /// <summary>Logs a warn message to the console only once.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void WarnOnce(string message, params object?[]? args) => this.Raise(message, LogLevel.Warn, true, 0, args);

    private void Raise(string message, LogLevel level, bool once, int hudType = 0, object?[]? args = null)
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
#if DEBUG
        if (once)
        {
            monitor.LogOnce(message, level);
            return;
        }

        monitor.Log(message, level);
#else
        switch (level)
        {
            case LogLevel.Error:
            case LogLevel.Alert:
                if (once)
                {
                    monitor.LogOnce(message, level);
                    break;
                }

                monitor.Log(message, level);
                break;

            default:
                if (once)
                {
                    monitor.LogOnce(message);
                    break;
                }

                monitor.Log(message);
                break;
        }
#endif

        if (level == LogLevel.Alert || hudType != 0)
        {
            Game1.addHUDMessage(new HUDMessage(message, hudType));
        }
    }
}
