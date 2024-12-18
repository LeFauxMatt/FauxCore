namespace LeFauxMods.Common.Models;

using System.Globalization;
using Interface;
using Microsoft.Xna.Framework;

/// <summary>
///     Base class for storing and retrieving typed values backed by a string dictionary.
/// </summary>
/// <param name="dictionaryModel">The underlying dictionary storage.</param>
internal abstract class DictionaryDataModel(IDictionaryModel dictionaryModel)
{
    private readonly Dictionary<string, ICachedValue> cachedValues = [];

    /// <summary>Initializes a new instance of the <see cref="DictionaryDataModel" /> class.</summary>
    /// <summary>Gets the prefix added to all dictionary keys.</summary>
    protected abstract string Prefix { get; }

    /// <summary>Checks if a value exists for the specified id.</summary>
    /// <param name="id">The id of the item.</param>
    /// <returns><c>true</c> if the dictionary contains a value; otherwise, <c>false</c>.</returns>
    public bool HasValue(string id) => dictionaryModel.ContainsKey(this.Prefix + id);

    /// <summary>
    ///     Converts an array to a comma-separated string.
    /// </summary>
    /// <param name="value">The array to convert.</param>
    /// <returns>Comma-separated values or empty string.</returns>
    protected static string ArrayToString(string[]? value) =>
        value?.Length > 0 ? string.Join(',', value) : string.Empty;

    /// <summary>
    ///     Converts an array of typed values to a comma-separated string.
    /// </summary>
    /// <typeparam name="TValue">The type of values to serialize.</typeparam>
    /// <param name="parser">Function to convert TValue to string.</param>
    /// <returns>Function that converts an array to a string.</returns>
    protected static Func<TValue[], string> ArrayToString<TValue>(Func<TValue, string> parser) =>
        value => value?.Length > 0 ? string.Join(',', value.Select(parser)) : string.Empty;

    /// <summary>Converts a boolean to its string representation.</summary>
    /// <param name="value">The boolean value.</param>
    /// <returns>The string "true" or empty string.</returns>
    protected static string BoolToString(bool value) =>
        value ? value.ToString(CultureInfo.InvariantCulture) : string.Empty;

    /// <summary>Converts a Color to its packed value string.</summary>
    /// <param name="value">The color to convert.</param>
    /// <returns>The packed color value or empty string for black.</returns>
    protected static string ColorToString(Color value) =>
        value.Equals(Color.Black) ? string.Empty : value.PackedValue.ToString(CultureInfo.InvariantCulture);

    /// <summary>Converts a dictionary to a key-value string format.</summary>
    /// <param name="value">The dictionary to convert.</param>
    /// <returns>Comma-separated key=value pairs or empty string.</returns>
    protected static string DictToString(Dictionary<string, string>? value) =>
        value?.Count > 0 ? string.Join(',', value.Select(pair => $"{pair.Key}={pair.Value}")) : string.Empty;

    /// <summary>
    ///     Converts a generic dictionary to a key-value string format.
    /// </summary>
    /// <typeparam name="TValue">The type of values to serialize.</typeparam>
    /// <param name="parser">Function to convert TValue to string.</param>
    /// <returns>Function that converts dictionary to string.</returns>
    protected static Func<Dictionary<string, TValue>, string> DictToString<TValue>(Func<TValue, string> parser) =>
        value => value?.Count > 0
            ? string.Join(',', value.Select(pair => $"{pair.Key}={parser(pair.Value)}"))
            : string.Empty;

    /// <summary>
    ///     Converts an integer to its string representation.
    /// </summary>
    /// <param name="value">The integer to convert.</param>
    /// <returns>String representation or empty string for zero.</returns>
    protected static string IntToString(int value) =>
        value == 0 ? string.Empty : value.ToString(CultureInfo.InvariantCulture);

    /// <summary>
    ///     Parses a string into an array of strings.
    /// </summary>
    /// <param name="value">The comma-separated string.</param>
    /// <returns>Array of parsed values or empty array.</returns>
    protected static string[] StringToArray(string value) =>
        !string.IsNullOrWhiteSpace(value)
            ? [.. value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)]
            : [];

    /// <summary>
    ///     Parses a string into an array of typed values.
    /// </summary>
    /// <typeparam name="TValue">Type to parse values into.</typeparam>
    /// <param name="parser">Function to parse strings into TValue.</param>
    /// <returns>Function that parses strings into a typed array.</returns>
    protected static Func<string, TValue[]> StringToArray<TValue>(Func<string, TValue> parser) =>
        value => !string.IsNullOrWhiteSpace(value)
            ?
            [
                .. value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(parser)
            ]
            : [];

    /// <summary>
    ///     Parses a string into a boolean value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>True if value is "True", false otherwise.</returns>
    protected static bool StringToBool(string value) =>
        !string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out var boolValue) && boolValue;

    /// <summary>
    ///     Parses a string into a Color value.
    /// </summary>
    /// <param name="value">The packed color value string.</param>
    /// <returns>Parsed Color or Color.Black for invalid input.</returns>
    protected static Color StringToColor(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !uint.TryParse(value, out var intValue))
        {
            return Utility.StringToColor(value) ?? Color.Black;
        }

        var color = Color.Black;
        color.PackedValue = intValue;
        return color;
    }

    /// <summary>
    ///     Parses a comma-separated string into string dictionary.
    /// </summary>
    /// <param name="value">The key-value string to parse.</param>
    /// <returns>Dictionary of parsed values or empty dictionary.</returns>
    protected static Dictionary<string, string> StringToDict(string value) =>
        !string.IsNullOrWhiteSpace(value)
            ? value.Split(',').Select(part => part.Split('=')).ToDictionary(part => part[0], part => part[1])
            : [];

    /// <summary>
    ///     Parses a string into a dictionary of typed values.
    /// </summary>
    /// <typeparam name="TValue">The type of values to parse into.</typeparam>
    /// <param name="parser">Function to convert strings to TValue.</param>
    /// <returns>Function that parses strings into dictionaries.</returns>
    protected static Func<string, Dictionary<string, TValue>> StringToDict<TValue>(Func<string, TValue> parser) =>
        value => !string.IsNullOrWhiteSpace(value)
            ? value.Split(',').Select(part => part.Split('=')).ToDictionary(part => part[0], part => parser(part[1]))
            : [];

    /// <summary>Parses a string to an int.</summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>The integer value, or the default value if the value is not a valid integer.</returns>
    protected static int StringToInt(string value) =>
        !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out var intValue) ? intValue : 0;

    /// <summary>Retrieves a value from the dictionary based on the provided id.</summary>
    /// <param name="id">The id of the item.</param>
    /// <param name="defaultValue">The value to return if the key is not found.</param>
    /// <returns>The value from the dictionary, or empty if the value is not found.</returns>
    protected string Get(string id, string? defaultValue = null) =>
        !dictionaryModel.TryGetValue(this.Prefix + id, out var value) ? defaultValue ?? string.Empty : value;

    /// <summary>
    ///     Retrieves and caches a typed value from the dictionary.
    /// </summary>
    /// <typeparam name="TValue">The type to convert the value to.</typeparam>
    /// <param name="id">The ID to look up.</param>
    /// <param name="parser">Function to convert string to TValue.</param>
    /// <param name="defaultValue">Value to return if key not found.</param>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the cached value exists but is of the wrong type for the
    ///     requested TValue.
    /// </exception>
    /// <returns>The parsed value, cached value, or default.</returns>
    [return: NotNullIfNotNull(nameof(defaultValue))]
    protected TValue? Get<TValue>(string id, Func<string, TValue> parser, TValue? defaultValue = default)
    {
        var key = this.Prefix + id;
        if (!dictionaryModel.TryGetValue(key, out var value))
        {
            return defaultValue;
        }

        if (this.cachedValues.TryGetValue(id, out var cachedValue))
        {
            if (cachedValue is not CachedValue<TValue> cached)
            {
                throw new InvalidOperationException($"Cached value for key '{key}' is of the wrong type.");
            }

            if (cached.OriginalValue == value)
            {
                return cached.Value;
            }
        }

        var newValue = parser(value);
        this.cachedValues[id] = new CachedValue<TValue>(value, newValue);
        return newValue;
    }

    /// <summary>
    ///     Sets a value in the dictionary with prefix.
    /// </summary>
    /// <param name="id">The ID to store under.</param>
    /// <param name="value">The value to store.</param>
    protected void Set(string id, string value) => dictionaryModel.SetValue(this.Prefix + id, value);

    /// <summary>
    ///     Sets and caches a typed value in the dictionary.
    /// </summary>
    /// <typeparam name="TValue">The type of value to store.</typeparam>
    /// <param name="id">The ID to store under.</param>
    /// <param name="value">The value to store.</param>
    /// <param name="parser">Function to convert value to string.</param>
    protected void Set<TValue>(string id, TValue value, Func<TValue, string> parser)
    {
        var key = this.Prefix + id;
        var stringValue = parser(value);
        this.cachedValues[id] = new CachedValue<TValue>(stringValue, value);
        dictionaryModel.SetValue(key, stringValue);
    }

    /// <summary>Initializes a new instance of the <see cref="CachedValue{T}" /> struct.</summary>
    /// <typeparam name="T">The type of the cached value.</typeparam>
    /// <param name="originalValue">The original value.</param>
    /// <param name="cachedValue">The cached value.</param>
    private readonly struct CachedValue<T>(string originalValue, T cachedValue) : ICachedValue
    {
        /// <inheritdoc />
        public string OriginalValue { get; } = originalValue;

        /// <summary>Gets the cached value.</summary>
        public T Value { get; } = cachedValue;
    }

    /// <summary>Represents a cached value.</summary>
    private interface ICachedValue
    {
        /// <summary>Gets the original value.</summary>
        public string OriginalValue { get; }
    }
}
