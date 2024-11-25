namespace LeFauxMods.Core.Models;

/// <summary>Initializes a new instance of the <see cref="Signal{T}" /> class.</summary>
/// <typeparam name="T">The signal type.</typeparam>
/// <param name="value">The signal value.</param>
internal sealed class Signal<T>(T value) : EventArgs
    where T : Enum
{
    /// <summary>
    /// Gets the value of the signal.
    /// </summary>
    public T Value { get; } = value;
}
