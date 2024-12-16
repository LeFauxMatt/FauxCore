namespace LeFauxMods.Common.Utilities;

using StardewValley.Mods;

/// <summary>Common extension methods.</summary>
internal static class CommonExtensions
{
    /// <summary>Tries to parse the specified string value as a boolean and returns the result.</summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="defaultValue">The default value to return if the value cannot be parsed as a boolean.</param>
    /// <returns>The boolean value, or the default value if the value is not a valid boolean.</returns>
    public static bool GetBool(this string value, bool defaultValue = false) =>
        !string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out var boolValue) ? boolValue : defaultValue;

    /// <summary>Retrieves a boolean value from the specified dictionary based on the given key and optional default value.</summary>
    /// <param name="dictionary">The dictionary to retrieve the boolean value from.</param>
    /// <param name="key">The key used to look up the value.</param>
    /// <param name="defaultValue">The default value to return if the key is not found or the value is not a valid boolean. </param>
    /// <returns>The boolean value associated with the key, or the default value.</returns>
    public static bool GetBool(this IDictionary<string, string> dictionary, string key, bool defaultValue = false) =>
        dictionary.TryGetValue(key, out var value) ? value.GetBool(defaultValue) : defaultValue;

    /// <summary>Retrieves a boolean value from the specified dictionary based on the given key and optional default value.</summary>
    /// <param name="modData">The mod data dictionary to retrieve the boolean value from.</param>
    /// <param name="key">The key used to look up the value.</param>
    /// <param name="defaultValue">The default value to return if the key is not found or the value is not a valid boolean. </param>
    /// <returns>The boolean value associated with the key, or the default value.</returns>
    public static bool GetBool(this ModDataDictionary modData, string key, bool defaultValue = false) =>
        modData.TryGetValue(key, out var value) ? value.GetBool(defaultValue) : defaultValue;

    /// <summary>Tries to parse the specified string value as an integer and returns the result.</summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="defaultValue">The default value to return if the value cannot be parsed as an integer.</param>
    /// <returns>The integer value, or the default value if the value is not a valid integer.</returns>
    public static int GetInt(this string value, int defaultValue = 0) =>
        !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out var intValue) ? intValue : defaultValue;

    /// <summary>Retrieves an integer value from the specified dictionary based on the given key and optional default value.</summary>
    /// <param name="modData">The mod data dictionary to retrieve the integer value from.</param>
    /// <param name="key">The key used to look up the value.</param>
    /// <param name="defaultValue">The default value to return if the key is not found or the value is not a valid integer.</param>
    /// <returns>The integer value associated with the key, or the default value.</returns>
    public static int GetInt(this ModDataDictionary modData, string key, int defaultValue = 0) =>
        modData.TryGetValue(key, out var value) ? value.GetInt(defaultValue) : defaultValue;

    /// <summary>Retrieves an integer value from the specified dictionary based on the given key and optional default value.</summary>
    /// <param name="dictionary">The dictionary to retrieve the integer value from.</param>
    /// <param name="key">The key used to look up the value.</param>
    /// <param name="defaultValue">The default value to return if the key is not found or the value is not a valid integer.</param>
    /// <returns>The integer value associated with the key, or the default value.</returns>
    public static int GetInt(this IDictionary<string, string> dictionary, string key, int defaultValue = 0) =>
        dictionary.TryGetValue(key, out var value) ? value.GetInt(defaultValue) : defaultValue;

    /// <summary>Invokes all event handlers for an event.</summary>
    /// <param name="eventHandler">The event.</param>
    /// <param name="source">The source.</param>
    public static void InvokeAll(this EventHandler? eventHandler, object? source)
    {
        if (eventHandler is null)
        {
            return;
        }

        foreach (var handler in eventHandler.GetInvocationList())
        {
            try
            {
                _ = handler.DynamicInvoke(source, null);
            }
            catch
            {
                // ignored
            }
        }
    }

    /// <summary>Invokes all event handlers for an event.</summary>
    /// <param name="eventHandler">The event.</param>
    /// <param name="source">The source.</param>
    /// <param name="param">The event parameters.</param>
    /// <typeparam name="T">The event handler type.</typeparam>
    public static void InvokeAll<T>(this EventHandler<T>? eventHandler, object? source, T param)
    {
        if (eventHandler is null)
        {
            return;
        }

        foreach (var @delegate in eventHandler.GetInvocationList())
        {
            var handler = (EventHandler<T>)@delegate;
            try
            {
                handler(source, param);
            }
            catch
            {
                // ignored
            }
        }
    }

    /// <summary>Shuffles a list randomly.</summary>
    /// <param name="source">The list to shuffle.</param>
    /// <typeparam name="T">The list type.</typeparam>
    /// <returns>Returns a shuffled list.</returns>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) => source.Shuffle(new Random());

    private static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (rng is null)
        {
            throw new ArgumentNullException(nameof(rng));
        }

        return source.ShuffleIterator(rng);
    }

    private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
    {
        var buffer = source.ToList();
        for (var i = 0; i < buffer.Count; ++i)
        {
            var j = rng.Next(i, buffer.Count);
            yield return buffer[j];

            buffer[j] = buffer[i];
        }
    }
}
