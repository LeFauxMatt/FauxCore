using LeFauxMods.Common.Utilities;
using StardewModdingAPI.Events;

namespace LeFauxMods.Common.Integrations.ContentPatcher;

internal sealed class ContentPatcherIntegration : ModIntegration<IContentPatcherApi>
{
    private readonly IModHelper helper;
    private int countDown = 10;

    /// <summary>Initializes a new instance of the <see cref="ContentPatcherIntegration" /> class.</summary>
    public ContentPatcherIntegration(IModHelper helper) : base(helper.ModRegistry)
    {
        this.helper = helper;
        helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
    }

    public override string UniqueId => "Pathoschild.ContentPatcher";

    /// <inheritdoc />
    public override ISemanticVersion Version { get; } = new SemanticVersion(2, 0, 0);

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e) =>
        this.helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;

    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        if (--this.countDown == 0)
        {
            this.helper.Events.GameLoop.UpdateTicked -= this.OnUpdateTicked;
        }

        if (!this.IsLoaded || !this.Api.IsConditionsApiReady)
        {
            return;
        }

        this.helper.Events.GameLoop.UpdateTicked -= this.OnUpdateTicked;
        ModEvents.Publish(new ConditionsApiReadyEventArgs());
    }
}
