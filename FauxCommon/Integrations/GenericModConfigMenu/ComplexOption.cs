namespace LeFauxMods.Common.Integrations.GenericModConfigMenu;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

internal abstract class ComplexOption
{
    /// <summary>Gets the height of the menu option.</summary>
    public virtual int Height { get; }

    /// <summary>Gets the name of the menu option.</summary>
    public virtual string Name { get; } = string.Empty;

    /// <summary>Gets the tooltip of the menu option.</summary>
    public virtual string Tooltip { get; } = string.Empty;

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
    public abstract void Draw(SpriteBatch spriteBatch, Vector2 pos);
}
