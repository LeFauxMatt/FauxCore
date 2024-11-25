namespace LeFauxMods.Core.Utilities;

using LeFauxMods.Core.Services;

internal static class EventBus
{
    private static readonly EventManager EventManager = new();

    /// <summary>Publishes an event with the given event arguments.</summary>
    /// <typeparam name="TEventArgs">The event argument implementation.</typeparam>
    /// <param name="eventArgs">The event arguments to publish.</param>
    /// <remarks>
    /// This method is used to raise an event with the provided event arguments. It can be used to notify subscribers
    /// of an event.
    /// </remarks>
    public static void Publish<TEventArgs>(TEventArgs eventArgs) =>
        EventManager.Publish(eventArgs);

    /// <summary>Publishes an event with the given event arguments.</summary>
    /// <typeparam name="TEventType">The type of the event arguments.</typeparam>
    /// <typeparam name="TEventArgs">The event argument implementation.</typeparam>
    /// <param name="eventArgs">The event arguments to publish.</param>
    /// <remarks>
    /// This method is used to raise an event with the provided event arguments. It can be used to notify subscribers
    /// of an event.
    /// </remarks>
    public static void Publish<TEventType, TEventArgs>(TEventArgs eventArgs)
        where TEventArgs : TEventType =>
        EventManager.Publish<TEventType, TEventArgs>(eventArgs);

    /// <summary>Subscribes to an event handler.</summary>
    /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
    /// <param name="handler">The event handler to subscribe.</param>
    public static void Subscribe<TEventArgs>(Action<TEventArgs> handler) =>
        EventManager.Subscribe(handler);

    /// <summary>Unsubscribes an event handler from an event.</summary>
    /// <param name="handler">The event handler to unsubscribe.</param>
    /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
    public static void Unsubscribe<TEventArgs>(Action<TEventArgs> handler) =>
        EventManager.Unsubscribe(handler);
}
