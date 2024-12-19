using LeFauxMods.Common.Interface;
using StardewValley.Mods;

namespace LeFauxMods.Common.Models;

/// <inheritdoc />
/// <param name="modData">The mod data dictionary.</param>
internal sealed class ModDataModel(ModDataDictionary modData) : IDictionaryModel
{
    /// <inheritdoc />
    public bool ContainsKey(string key) => modData.ContainsKey(key);

    /// <inheritdoc />
    public void SetValue(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            _ = modData.Remove(key);
            return;
        }

        modData[key] = value;
    }

    /// <inheritdoc />
    public bool TryGetValue(string key, [NotNullWhen(true)] out string? value) => modData.TryGetValue(key, out value);
}
