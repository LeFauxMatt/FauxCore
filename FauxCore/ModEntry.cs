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
        helper.Events.Input.ButtonPressed += OnButtonPressed;
    }

    /// <inheritdoc />
    public override object GetApi(IModInfo mod) => new ModApi(mod);

    private static void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
        if (Game1.activeClickableMenu is not TitleMenu ||
            TitleMenu.subMenu is not LoadGameMenu menu ||
            ModState.ConfigWithLastSave is null)
        {
            return;
        }

        var (mouseX, mouseY) = e.Cursor.GetScaledScreenPixels().ToPoint();
        var slotButton = menu.slotButtons.FirstOrDefault(slot => slot.containsPoint(mouseX, mouseY));
        if (slotButton is null)
        {
            return;
        }

        var index = menu.slotButtons.IndexOf(slotButton);
        if (index == -1)
        {
            return;
        }

        var menuSlot = menu.MenuSlots[menu.currentItemIndex + index];
        if (menuSlot is not LoadGameMenu.SaveFileSlot saveFileSlot)
        {
            return;
        }

        ModState.ConfigWithLastSave.LastSave = saveFileSlot.Farmer.slotName;
    }

    private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
    {
        this.Helper.Events.GameLoop.UpdateTicked -= this.OnUpdateTicked;
        this.Helper.Events.Input.ButtonPressed -= OnButtonPressed;
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