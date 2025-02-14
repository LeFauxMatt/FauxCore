using LeFauxMods.Common.Utilities;
using LeFauxMods.Core.Services;
using StardewModdingAPI.Events;
using StardewValley.Menus;

namespace LeFauxMods.Core;

/// <inheritdoc />
internal sealed class ModEntry : Mod
{
    /// <inheritdoc />
    public override void Entry(IModHelper helper)
    {
        // Init
        ModState.Init(helper);
        Log.Init(this.Monitor);

        // Events
        helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
    }

    /// <inheritdoc />
    public override object GetApi(IModInfo mod) => new ModApi(mod);

    private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
    {
        this.Helper.Events.GameLoop.UpdateTicked -= this.OnUpdateTicked;
        if (ModState.ConfigWithLastSave is not null)
        {
            ModState.ConfigWithLastSave.LastSave = Game1.player.slotName;
        }
    }

    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        if (Game1.activeClickableMenu is not TitleMenu ||
            TitleMenu.subMenu is not LoadGameMenu ||
            string.IsNullOrWhiteSpace(ModState.ConfigWithLastSave?.LastSave))
        {
            return;
        }

        this.Helper.Events.GameLoop.UpdateTicked -= this.OnUpdateTicked;
        SaveGame.Load(ModState.ConfigWithLastSave.LastSave);
        Game1.exitActiveMenu();
    }
}