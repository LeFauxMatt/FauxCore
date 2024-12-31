using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LeFauxMods.Common.Integrations.FindAnything;

#pragma warning disable

/// <summary>Mod API for Find Anything.</summary>
public interface IFindAnythingApi
{
    /// <summary>Subscribe to an event handler.</summary>
    /// <param name="handler">The event handler to subscribe to.</param>
    public void Subscribe(Action<ISearchUpdated> handler);

    /// <summary>Subscribe to an event handler.</summary>
    /// <param name="handler">The event handler to subscribe to.</param>
    public void Subscribe(Action<ISearchSubmitted> handler);

    /// <summary>Subscribe to an event handler.</summary>
    /// <param name="handler">The event handler to subscribe to.</param>
    public void Unsubscribe(Action<ISearchUpdated> handler);

    /// <summary>Subscribe to an event handler.</summary>
    /// <param name="handler">The event handler to subscribe to.</param>
    public void Unsubscribe(Action<ISearchSubmitted> handler);
}

/// <summary>Event arguments for when the search text changes.</summary>
public interface ISearchUpdated
{
    /// <summary>Gets the current search text.</summary>
    public string Text { get; }

    /// <summary>Add any keywords which partially match the search text.</summary>
    /// <param name="keywords">The keywords to add.</param>
    public void AddKeywords(IEnumerable<string> keywords);
}

/// <summary>Event arguments for when the search text is submitted.</summary>
public interface ISearchSubmitted
{
    /// <summary>Gets the location that the search pertains to.</summary>
    public GameLocation Location { get; }

    /// <summary>Gets the current search text.</summary>
    public string Text { get; }

    /// <summary>Add a search result.</summary>
    /// <param name="result"></param>
    public void AddResult(IFoundEntity result);
}

/// <summary>
///     Represents the results for a searched term.
/// </summary>
/// <param name="term">The search term.</param>
/// <param name="results">The matched results.</param>
/// <param name="keywords">Keywords that the search term is a partial match for.</param>
/// <returns>Returns true if the search term was handled.</returns>
public delegate bool SearchHandler(
    string term,
    out IEnumerable<IFoundEntity> results,
    out IEnumerable<string> keywords);

/// <summary>Represents the real-time location of a found entity.</summary>
public interface IFoundEntity
{
    /// <summary>Gets a context which uniquely references the entity being pointed to.</summary>
    public object? Context { get; }

    /// <summary>Gets the location of the entity being pointed to.</summary>
    public GameLocation? Location { get; }

    /// <summary>Gets the pixel offset to draw a pointer above the found object.</summary>
    public Vector2 Offset { get; }

    /// <summary>Gets an optional source rectangle for the icon.</summary>
    public Rectangle? SourceRectangle { get; }

    /// <summary>Gets an optional icon for the entity being pointed to.</summary>
    public Texture2D? Texture { get; }

    /// <summary>Gets the tile location of the entity being pointed to.</summary>
    public Vector2 Tile { get; }
}