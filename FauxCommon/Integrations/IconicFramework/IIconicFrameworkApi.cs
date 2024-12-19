using Microsoft.Xna.Framework;

namespace LeFauxMods.Common.Integrations.IconicFramework;

#pragma warning disable

/// <summary>Public api to add icons above or below the toolbar.</summary>
public interface IIconicFrameworkApi : IToolbarIconsArchived
{
    /// <summary>Adds an icon.</summary>
    /// <param name="id">A unique identifier for the icon.</param>
    /// <param name="texturePath">The path to the texture icon.</param>
    /// <param name="sourceRect">The source rectangle of the icon.</param>
    /// <param name="getTitle">Text to appear as the title in the Radial Menu.</param>
    /// <param name="getDescription">Text to appear when hovering over the icon.</param>
    public void AddToolbarIcon(
        string id,
        string texturePath,
        Rectangle? sourceRect,
        Func<string>? getTitle,
        Func<string>? getDescription);

    /// <summary>Removes an icon.</summary>
    /// <param name="id">A unique identifier for the icon.</param>
    public void RemoveToolbarIcon(string id);

    /// <summary>Subscribes to an event handler.</summary>
    /// <param name="handler">The event handler to subscribe.</param>
    public void Subscribe(Action<IIconPressedEventArgs> handler);

    /// <summary>Unsubscribes an event handler from an event.</summary>
    /// <param name="handler">The event handler to unsubscribe.</param>
    public void Unsubscribe(Action<IIconPressedEventArgs> handler);
}

/// <summary>Represents the event arguments for a toolbar icon being pressed.</summary>
public interface IIconPressedEventArgs
{
    /// <summary>Gets the button that was pressed.</summary>
    public SButton Button { get; }

    /// <summary>Gets the id of the icon that was pressed.</summary>
    public string Id { get; }
}

public interface IToolbarIconsArchived
{
    /// <summary>Event triggered when any toolbar icon is pressed.</summary>
    [Obsolete("Use Subscribe(Action<IIconPressedEventArgs>) and Unsubscribe(Action<IIconPressedEventArgs>) instead.")]
    public event EventHandler<string> ToolbarIconPressed;

    /// <summary>Adds an icon.</summary>
    /// <param name="id">A unique identifier for the icon.</param>
    /// <param name="texturePath">The path to the texture icon.</param>
    /// <param name="sourceRect">The source rectangle of the icon.</param>
    /// <param name="hoverText">Text to appear when hovering over the icon.</param>
    [Obsolete(
        "Use AddToolbarIcon(string id, string texturePath, Rectangle? sourceRect, Func<string>? getTitle, Func<string>? getDescription) instead.")]
    public void AddToolbarIcon(string id, string texturePath, Rectangle? sourceRect, string? hoverText);
}
