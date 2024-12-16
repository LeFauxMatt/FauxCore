namespace LeFauxMods.Common.Models;

using Interface;

/// <inheritdoc />
/// <param name="getData">Get the custom field data.</param>
internal sealed class DictionaryModel(Func<Dictionary<string, string>?> getData) : IDictionaryModel
{
    /// <summary>Initializes a new instance of the <see cref="DictionaryModel" /> class.</summary>
    private Dictionary<string, string>? Data => getData();

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
