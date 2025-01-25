using LeFauxMods.Common.Interface;

namespace LeFauxMods.Common.Models;

/// <inheritdoc />
/// <param name="entity">The entity having mod data.</param>
internal sealed class ModDataModel(IHaveModData entity) : IDictionaryModel
{
    /// <inheritdoc />
    public bool ContainsKey(string key) => entity.modData.ContainsKey(key);

    /// <inheritdoc />
    public void SetValue(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            _ = entity.modData.Remove(key);
            return;
        }

        entity.modData[key] = value;
    }

    /// <inheritdoc />
    public bool TryGetValue(string key, [NotNullWhen(true)] out string? value) =>
        entity.modData.TryGetValue(key, out value);
}