namespace LeFauxMods.Common.Models;

using Interface;

/// <inheritdoc />
internal sealed class DictionaryModel : IDictionaryModel
{
    private readonly Func<Dictionary<string, string>?> getData;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DictionaryModel" /> class.
    /// </summary>
    /// <param name="getter">Get the source data.</param>
    public DictionaryModel(Func<Dictionary<string, string>?>? getter = null)
    {
        if (getter is null)
        {
            var newData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            getter = () => newData;
        }

        this.getData = getter;
    }

    /// <summary>Initializes a new instance of the <see cref="DictionaryModel" /> class.</summary>
    private Dictionary<string, string>? Data => this.getData();

    /// <inheritdoc />
    public bool ContainsKey(string key) => this.Data?.ContainsKey(key) == true;

    /// <inheritdoc />
    public void SetValue(string key, string value)
    {
        if (this.Data is null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            _ = this.Data.Remove(key);
            return;
        }

        this.Data[key] = value;
    }

    /// <inheritdoc />
    public bool TryGetValue(string key, [NotNullWhen(true)] out string? value)
    {
        if (this.Data is not null)
        {
            return this.Data.TryGetValue(key, out value);
        }

        value = null;
        return false;
    }
}
