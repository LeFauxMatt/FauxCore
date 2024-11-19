namespace LeFauxMods.Core.Services;

using System.Globalization;

/// <summary>Handles logging information to the console.</summary>
internal sealed class SimpleLogging
{
    private readonly IMonitor monitor;

    private string lastMessage = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleLogging"/> class.
    /// </summary>
    /// <param name="monitor">Dependency used for monitoring and logging.</param>
    public SimpleLogging(IMonitor monitor) => this.monitor = monitor;

    /// <summary>Logs an alert message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Alert(string message, object?[]? args = null) => this.Raise(message, LogLevel.Alert, false, args);

    /// <summary>Logs a debug message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Debug(string message, object?[]? args = null) => this.Raise(message, LogLevel.Debug, false, args);

    /// <summary>Logs an error message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Error(string message, params object?[]? args) => this.Raise(message, LogLevel.Error, false, args);

    /// <summary>Logs an info message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Info(string message, params object?[]? args) => this.Raise(message, LogLevel.Info, false, args);

    /// <summary>Logs a trace message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Trace(string message, params object?[]? args) => this.Raise(message, LogLevel.Trace, false, args);

    /// <summary>Logs a trace message to the console only once.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void TraceOnce(string message, params object?[]? args) => this.Raise(message, LogLevel.Trace, true, args);

    /// <summary>Logs a warn message to the console.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void Warn(string message, params object?[]? args) => this.Raise(message, LogLevel.Warn, false, args);

    /// <summary>Logs a warn message to the console only once.</summary>
    /// <param name="message">The message to send.</param>
    /// <param name="args">The arguments to parse in a formatted string.</param>
    [StringFormatMethod("message")]
    public void WarnOnce(string message, params object?[]? args) => this.Raise(message, LogLevel.Warn, true, args);

    private void Raise(string message, LogLevel level, bool once, object?[]? args = null)
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
            this.monitor.LogOnce(message, level);
            return;
        }

        this.monitor.Log(message, level);
#else
        switch (level)
        {
            case LogLevel.Error:
            case LogLevel.Alert:
                if (once)
                {
                    this.monitor.LogOnce(message, level);
                    break;
                }

                this.monitor.Log(message, level);
                break;

            default:
                if (once)
                {
                    this.monitor.LogOnce(message);
                    break;
                }

                this.monitor.Log(message);
                break;
        }
#endif

        if (level == LogLevel.Alert)
        {
            Game1.showRedMessage(message);
        }
    }
}
