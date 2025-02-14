namespace LeFauxMods.Common.Integrations.StarControl;

/// <inheritdoc />
internal sealed class StarControlIntegration(IModRegistry modRegistry, bool required = false)
    : ModIntegration<IStarControlApi>(modRegistry, required)
{
    /// <inheritdoc />
    public override string UniqueId => "focustense.StarControl";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
}