using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LeFauxMods.Common.Integrations.GenericModConfigMenu;

/// <summary>An option using custom rendering logic.</summary>
/// <param name="helper">Dependency for events, input, and content.</param>
internal abstract class ComplexOption(IModHelper helper)
{
    private bool lastPressed;
    private int lastTick;

    /// <summary>Gets the name of the menu option.</summary>
    public virtual string Name { get; } = string.Empty;

    /// <summary>Gets the tooltip of the menu option.</summary>
    public virtual string Tooltip { get; } = string.Empty;

    /// <summary>Gets the height of the menu option.</summary>
    public virtual int Height { get; protected set; }

    protected IModHelper Helper { get; } = helper;

    /// <summary>Gets the available menu width.</summary>
    protected int AvailableWidth { get; private set; }

    /// <summary>Gets the mouse position adjusted for origin.</summary>
    protected Point MousePos { get; private set; }

    /// <summary>Gets the top-left drawing position.</summary>
    protected Point Origin { get; private set; }

    /// <summary>Gets a value indicating whether a pressed event occurs in the current draw loop.</summary>
    protected bool Pressed { get; private set; }

    /// <summary>Gets a value indicating whether a held event occurs in the current draw loop.</summary>
    protected bool Held { get; private set; }

    /// <summary>Executes a set of actions after the option is set.</summary>
    public virtual void AfterReset()
    {
    }

    /// <summary>Executes a set of actions after the option is saved.</summary>
    public virtual void AfterSave()
    {
    }

    /// <summary>Executes a set of actions before the menu is closed.</summary>
    public virtual void BeforeMenuClosed()
    {
    }

    /// <summary>Executes a set of actions before the menu is opened.</summary>
    public virtual void BeforeMenuOpened()
    {
    }

    /// <summary>Executes a set of actions before the option is reset.</summary>
    public virtual void BeforeReset()
    {
    }

    /// <summary>Executes a set of actions before the option is saved.</summary>
    public virtual void BeforeSave()
    {
    }

    /// <summary>Draws the menu option.</summary>
    /// <param name="spriteBatch">The sprite batch to draw to.</param>
    /// <param name="pos">The position to draw at.</param>
    public virtual void Draw(SpriteBatch spriteBatch, Vector2 pos)
    {
        this.AvailableWidth = Math.Min(1200, Game1.uiViewport.Width - 200);
        pos.X -= this.AvailableWidth / 2f;
        this.Origin = pos.ToPoint();
        this.MousePos = (this.Helper.Input.GetCursorPosition().GetScaledScreenPixels() - pos).ToPoint();

        var mouseLeft = this.Helper.Input.GetState(SButton.MouseLeft);
        var controllerA = this.Helper.Input.GetState(SButton.ControllerA);

        this.Pressed = !this.lastPressed &&
                       Game1.ticks == this.lastTick + 1 &&
                       (mouseLeft is SButtonState.Pressed || controllerA is SButtonState.Pressed);

        this.Held = mouseLeft is SButtonState.Held || controllerA is SButtonState.Held;

        this.lastPressed = this.Pressed;
        this.lastTick = Game1.ticks;

        this.DrawOption(spriteBatch, pos);
    }

    /// <summary>Draws the menu option.</summary>
    /// <param name="spriteBatch">The sprite batch to draw to.</param>
    /// <param name="pos">The position to draw at.</param>
    public abstract void DrawOption(SpriteBatch spriteBatch, Vector2 pos);
}