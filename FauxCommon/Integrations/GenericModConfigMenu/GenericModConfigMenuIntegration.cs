namespace LeFauxMods.Common.Integrations.GenericModConfigMenu;

/// <summary>Initializes a new instance of the <see cref="GenericModConfigMenuIntegration" /> class.</summary>
/// <param name="manifest">The mod's manifest.</param>
/// <param name="modRegistry">Dependency used for fetching metadata about loaded mods.</param>
internal sealed class GenericModConfigMenuIntegration(IManifest manifest, IModRegistry modRegistry)
    : ModIntegration<IGenericModConfigMenuApi>(modRegistry)
{
    private bool isRegistered;

    public override string UniqueId => "spacechase0.GenericModConfigMenu";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(1, 14, 1);

    /// <summary>Add an option at the current position in the form using custom rendering logic.</summary>
    /// <param name="complexOption">The option to add.</param>
    public void AddComplexOption(ComplexOption complexOption) =>
        this.Api?.AddComplexOption(
            manifest,
            () => complexOption.Name,
            complexOption.Draw,
            () => complexOption.Tooltip,
            complexOption.BeforeMenuOpened,
            complexOption.BeforeSave,
            complexOption.AfterSave,
            complexOption.BeforeReset,
            complexOption.AfterReset,
            complexOption.BeforeMenuClosed,
            () => complexOption.Height);

    /// <summary>Register a config menu with GMCM.</summary>
    /// <param name="reset">Reset the mod's config to its default values.</param>
    /// <param name="save">Save the mod's current config to the <c>config.json</c> file.</param>
    /// <param name="titleScreenOnly">Whether the options can only be edited from the title screen.</param>
    public void Register(Action reset, Action save, bool titleScreenOnly = false)
    {
        if (this.isRegistered)
        {
            this.Api?.Unregister(manifest);
        }

        this.Api?.Register(manifest, reset, save, titleScreenOnly);
        this.isRegistered = true;
    }
}
