namespace LeFauxMods.Core.Integrations.IconicFramework;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Menus;

#pragma warning disable

/// <summary>Public api to add icons above or below the toolbar.</summary>
public interface IIconicFrameworkApi : IToolbarIconsArchived
{
    /// <summary>Adds an icon.</summary>
    /// <param name="id">A unique identifier for the icon.</param>
    /// <param name="texturePath">The path to the texture icon.</param>
    /// <param name="sourceRect">The source rectangle of the icon.</param>
    /// <param name="hoverText">Text to appear when hovering over the icon.</param>
    public void AddToolbarIcon(string id, string texturePath, Rectangle? sourceRect, string? hoverText);

    /// <summary>Removes an icon.</summary>
    /// <param name="id">A unique identifier for the icon.</param>
    public void RemoveToolbarIcon(string id);

    /// <summary>Subscribes to an event handler.</summary>
    /// <param name="handler">The event handler to subscribe.</param>
    void Subscribe(Action<IIconPressedEventArgs> handler);

    /// <summary>Unsubscribes an event handler from an event.</summary>
    /// <param name="handler">The event handler to unsubscribe.</param>
    void Unsubscribe(Action<IIconPressedEventArgs> handler);
}

/// <summary>Represents the event arguments for a toolbar icon being pressed.</summary>
public interface IIconPressedEventArgs
{
    /// <summary>Gets the button that was pressed.</summary>
    SButton Button { get; }

    /// <summary>Gets the id of the icon that was pressed.</summary>
    string Id { get; }
}

#region Archived

public interface IToolbarIconsArchived
{
    /// <summary>Event triggered when any toolbar icon is pressed.</summary>
    [Obsolete("Use Subscribe(Action<IIconPressedEventArgs>) and Unsubscribe(Action<IIconPressedEventArgs>) instead.")]
    public event EventHandler<string> ToolbarIconPressed;

    /// <summary>Adds an icon next to the <see cref="Toolbar" />.</summary>
    /// <param name="icon">The icon to add.</param>
    /// <param name="hoverText">Text to appear when hovering over the icon.</param>
    [Obsolete("Use AddToolbarIcon(string id, string texturePath, Rectangle? sourceRect, string? hoverText) instead.")]
    public void AddToolbarIcon(IIcon icon, string? hoverText);

    /// <summary>Removes an icon.</summary>
    /// <param name="icon">The icon to remove.</param>
    [Obsolete("Use RemoveToolbarIcon(string id) instead.")]
    public void RemoveToolbarIcon(IIcon icon);
}

/// <summary>Represents an icon on a sprite sheet.</summary>
public interface IIcon
{
    /// <summary>Gets the icon source area.</summary>
    public Rectangle Area { get; }

    /// <summary>Gets the icon id.</summary>
    public string Id { get; }

    /// <summary>Gets the icon texture path.</summary>
    public string Path { get; }

    /// <summary>Gets the id of the mod this icon is loaded from.</summary>
    public string Source { get; }

    /// <summary>Gets the unique identifier for this icon.</summary>
    public string UniqueId { get; }

    /// <summary>Gets a component with the icon.</summary>
    /// <param name="style">The component style.</param>
    /// <param name="x">The component x-coordinate.</param>
    /// <param name="y">The component y-coordinate.</param>
    /// <param name="scale">The target component scale.</param>
    /// <param name="name">The name.</param>
    /// <param name="hoverText">The hover text.</param>
    /// <returns>Returns a new button.</returns>
    public ClickableTextureComponent Component(
        IconStyle style,
        int x = 0,
        int y = 0,
        float scale = Game1.pixelZoom,
        string? name = null,
        string? hoverText = null);

    /// <summary>Gets the icon texture.</summary>
    /// <param name="style">The style of the icon.</param>
    /// <returns>Returns the texture.</returns>
    public Texture2D Texture(IconStyle style);
}

/// <summary>The styles of icon.</summary>
public enum IconStyle
{
    /// <summary>An icon with a transparent background.</summary>
    Transparent,

    /// <summary>An icon with a button background.</summary>
    Button,
}

#endregion Archived
