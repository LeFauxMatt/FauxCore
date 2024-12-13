namespace LeFauxMods.Common.Models;

using System.Globalization;
using Microsoft.Xna.Framework;

/// <summary>
///     Base class for storing and retrieving typed values backed by a string dictionary.
/// </summary>
internal abstract class DictionaryDataModel
{
    private readonly Dictionary<string, ICachedValue> cachedValues = new();
    private readonly IDictionaryModel dictionaryModel;

    /// <summary>Initializes a new instance of the <see cref="DictionaryDataModel" /> class.</summary>
    /// <param name="dictionaryModel">The underlying dictionary storage.</param>
    protected DictionaryDataModel(IDictionaryModel dictionaryModel) => this.dictionaryModel = dictionaryModel;

    /// <summary>Gets the prefix added to all dictionary keys.</summary>
    protected abstract string Prefix { get; }

    /// <summary>Checks if a value exists for the specified id.</summary>
    /// <param name="id">The id of the item.</param>
    /// <returns><c>true</c> if the dictionary contains a value; otherwise, <c>false</c>.</returns>
    public bool HasValue(string id) => this.dictionaryModel.ContainsKey(this.Prefix + id);

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
        value?.Any() == true ? string.Join(',', value.Select(pair => $"{pair.Key}={pair.Value}")) : string.Empty;

    /// <summary>
    ///     Converts a generic dictionary to a key-value string format.
    /// </summary>
    /// <typeparam name="TValue">The type of values to serialize.</typeparam>
    /// <param name="serializer">Function to convert TValue to string.</param>
    /// <returns>Function that converts dictionary to string.</returns>
    protected static Func<Dictionary<string, TValue>, string> DictToString<TValue>(Func<TValue, string> serializer) =>
        value =>
            value?.Any() == true
                ? string.Join(',', value.Select(pair => $"{pair.Key}={serializer(pair.Value)}"))
                : string.Empty;

    /// <summary>
    ///     Converts an integer to its string representation.
    /// </summary>
    /// <param name="value">The integer to convert.</param>
    /// <returns>String representation or empty string for zero.</returns>
    protected static string IntToString(int value) =>
        value == 0 ? string.Empty : value.ToString(CultureInfo.InvariantCulture);

    /// <summary>
    ///     Converts a list to a comma-separated string.
    /// </summary>
    /// <param name="value">The list to convert.</param>
    /// <returns>Comma-separated values or empty string.</returns>
    protected static string ListToString(List<string>? value) =>
        value?.Any() == true ? string.Join(',', value) : string.Empty;

    /// <summary>
    ///     Converts a list of typed values to a comma-separated string.
    /// </summary>
    /// <typeparam name="TValue">The type of values to serialize.</typeparam>
    /// <param name="serializer">Function to convert TValue to string.</param>
    /// <returns>Function that converts lists to strings.</returns>
    protected static Func<List<TValue>, string> ListToString<TValue>(Func<TValue, string> serializer) =>
        value =>
            value?.Any() == true
                ? string.Join(',', value.Select(serializer))
                : string.Empty;

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
            return Color.Black;
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
            : new Dictionary<string, string>();

    /// <summary>
    ///     Parses a string into a dictionary of typed values.
    /// </summary>
    /// <typeparam name="TValue">The type of values to parse into.</typeparam>
    /// <param name="deserializer">Function to convert strings to TValue.</param>
    /// <returns>Function that parses strings into dictionaries.</returns>
    protected static Func<string, Dictionary<string, TValue>> StringToDict<TValue>(Func<string, TValue> deserializer) =>
        value =>
            !string.IsNullOrWhiteSpace(value)
                ? value.Split(',').Select(part => part.Split('='))
                    .ToDictionary(part => part[0], part => deserializer(part[1]))
                : new Dictionary<string, TValue>();

    /// <summary>Deserialize a string to an int.</summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>The integer value, or the default value if the value is not a valid integer.</returns>
    protected static int StringToInt(string value) =>
        !string.IsNullOrWhiteSpace(value) && int.TryParse(value, out var intValue) ? intValue : 0;

    /// <summary>
    ///     Parses a string into a list of strings.
    /// </summary>
    /// <param name="value">The comma-separated string.</param>
    /// <returns>List of parsed values or empty list.</returns>
    protected static List<string> StringToList(string value) =>
        !string.IsNullOrWhiteSpace(value)
            ? value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()
            : [];

    /// <summary>
    ///     Parses a string into a list of typed values.
    /// </summary>
    /// <typeparam name="TValue">Type to parse values into.</typeparam>
    /// <param name="deserializer">Function to parse strings into TValue.</param>
    /// <returns>Function that parses strings into typed lists.</returns>
    protected static Func<string, List<TValue>> StringToList<TValue>(Func<string, TValue> deserializer) =>
        value =>
            !string.IsNullOrWhiteSpace(value)
                ? value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(deserializer).ToList()
                : [];

    /// <summary>Retrieves a value from the dictionary based on the provided id.</summary>
    /// <param name="id">The id of the item.</param>
    /// <param name="defaultValue">The value to return if the key is not found.</param>
    /// <returns>The value from the dictionary, or empty if the value is not found.</returns>
    protected string Get(string id, string? defaultValue = null) =>
        !this.dictionaryModel.TryGetValue(this.Prefix + id, out var value) ? defaultValue ?? string.Empty : value;

    /// <summary>
    ///     Retrieves and caches a typed value from the dictionary.
    /// </summary>
    /// <typeparam name="TValue">The type to convert the value to.</typeparam>
    /// <param name="id">The ID to look up.</param>
    /// <param name="deserializer">Function to convert string to TValue.</param>
    /// <param name="defaultValue">Value to return if key not found.</param>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the cached value exists but is of the wrong type for the
    ///     requested TValue.
    /// </exception>
    /// <returns>The parsed value, cached value, or default.</returns>
    protected TValue? Get<TValue>(string id, Func<string, TValue> deserializer, TValue? defaultValue = default)
    {
        var key = this.Prefix + id;
        if (!this.dictionaryModel.TryGetValue(key, out var value))
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

        var newValue = deserializer(value);
        this.cachedValues[id] = new CachedValue<TValue>(value, newValue);
        return newValue;
    }

    /// <summary>
    ///     Sets a value in the dictionary with prefix.
    /// </summary>
    /// <param name="id">The ID to store under.</param>
    /// <param name="value">The value to store.</param>
    protected void Set(string id, string value)
    {
        var key = this.Prefix + id;
        this.dictionaryModel.SetValue(key, value);
    }

    /// <summary>
    ///     Sets and caches a typed value in the dictionary.
    /// </summary>
    /// <typeparam name="TValue">The type of value to store.</typeparam>
    /// <param name="id">The ID to store under.</param>
    /// <param name="value">The value to store.</param>
    /// <param name="serializer">Function to convert value to string.</param>
    protected void Set<TValue>(string id, TValue value, Func<TValue, string> serializer)
    {
        var key = this.Prefix + id;
        var stringValue = serializer(value);
        this.cachedValues[id] = new CachedValue<TValue>(stringValue, value);
        this.dictionaryModel.SetValue(key, stringValue);
    }

    /// <inheritdoc />
    private readonly struct CachedValue<T> : ICachedValue
    {
        /// <summary>Initializes a new instance of the <see cref="CachedValue{T}" /> struct.</summary>
        /// <param name="originalValue">The original value.</param>
        /// <param name="cachedValue">The cached value.</param>
        public CachedValue(string originalValue, T cachedValue)
        {
            this.OriginalValue = originalValue;
            this.Value = cachedValue;
        }

        /// <inheritdoc />
        public string OriginalValue { get; }

        /// <summary>Gets the cached value.</summary>
        public T Value { get; }
    }

    /// <summary>Represents a cached value.</summary>
    private interface ICachedValue
    {
        /// <summary>Gets the original value.</summary>
        public string OriginalValue { get; }
    }
}