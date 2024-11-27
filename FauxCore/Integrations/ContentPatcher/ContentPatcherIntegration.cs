namespace LeFauxMods.Core.Integrations.ContentPatcher;

using StardewModdingAPI;
using StardewModdingAPI.Events;

internal sealed class ContentPatcherIntegration : ModIntegration<IContentPatcherApi>
{
    private readonly IModHelper helper;
    private EventHandler<bool>? conditionsApiReady;
    private int countDown = 10;

    /// <summary>Initializes a new instance of the <see cref="ContentPatcherIntegration" /> class.</summary>
    /// <param name="helper"></param>
    public ContentPatcherIntegration(IModHelper helper) : base(helper.ModRegistry)
    {
        this.helper = helper;
        helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
    }

    /// <summary>Event raised when the conditions api is ready.</summary>
    public event EventHandler<bool> ConditionsApiReady
    {
        add => this.conditionsApiReady += value;
        remove => this.conditionsApiReady -= value;
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
        if (this.conditionsApiReady is null)
        {
            return;
        }

        foreach (var handler in this.conditionsApiReady.GetInvocationList())
        {
            try
            {
                _ = handler.DynamicInvoke(this, true);
            }
            catch (Exception ex)
            {
                Log.Error(
                    "{0} failed: {1}",
                    nameof(this.conditionsApiReady),
                    ex.Message);
            }
        }
    }
}
