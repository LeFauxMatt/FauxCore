namespace LeFauxMods.Common.Services;

using System.Reflection;
using Models;
using Utilities;

/// <summary>Managed event subscriptions and publishing.</summary>
internal sealed class EventManager
{
    private static readonly ReverseComparer<int> ReverseComparer = new();

    private readonly Dictionary<Type, SortedList<int, List<Delegate>>> subscribers = [];

    /// <summary>Publishes an event with the given event arguments.</summary>
    /// <typeparam name="TEventArgs">The event argument implementation.</typeparam>
    /// <param name="eventArgs">The event arguments to publish.</param>
    /// <remarks>
    ///     This method is used to raise an event with the provided event arguments. It can be used to notify subscribers
    ///     of an event.
    /// </remarks>
    public void Publish<TEventArgs>(TEventArgs eventArgs)
    {
        var eventType = typeof(TEventArgs);
        SortedList<int, List<Delegate>> handlersToInvoke;
        lock (this.subscribers)
        {
            if (!this.subscribers.TryGetValue(eventType, out var priorityHandlers))
            {
                return;
            }

            handlersToInvoke = new SortedList<int, List<Delegate>>(priorityHandlers, ReverseComparer);
        }

        foreach (var priorityGroup in handlersToInvoke.Values)
        {
            foreach (var @delegate in priorityGroup.ToList())
            {
                if (@delegate is not Action<TEventArgs> handler)
                {
                    continue;
                }

                try
                {
                    handler(eventArgs);
                }
                catch (Exception ex)
                {
                    Log.Trace("Exception occurred: {0}\n{1}", ex.Message, ex.StackTrace);
                }
            }
        }
    }

    /// <summary>Publishes an event with the given event arguments.</summary>
    /// <typeparam name="TEventType">The type of the event arguments.</typeparam>
    /// <typeparam name="TEventArgs">The event argument implementation.</typeparam>
    /// <param name="eventArgs">The event arguments to publish.</param>
    /// <remarks>
    ///     This method is used to raise an event with the provided event arguments. It can be used to notify subscribers
    ///     of an event.
    /// </remarks>
    public void Publish<TEventType, TEventArgs>(TEventArgs eventArgs)
        where TEventArgs : TEventType
    {
        var eventType = typeof(TEventType);
        SortedList<int, List<Delegate>> handlersToInvoke;
        lock (this.subscribers)
        {
            if (!this.subscribers.TryGetValue(eventType, out var priorityHandlers))
            {
                return;
            }

            handlersToInvoke = new SortedList<int, List<Delegate>>(priorityHandlers, ReverseComparer);
        }

        foreach (var priorityGroup in handlersToInvoke.Values)
        {
            foreach (var @delegate in priorityGroup.ToList())
            {
                if (@delegate is not Action<TEventArgs> handler)
                {
                    continue;
                }

                try
                {
                    handler(eventArgs);
                }
                catch (Exception ex)
                {
                    Log.Trace("Exception occurred: {0}\n{1}", ex.Message, ex.StackTrace);
                }
            }
        }
    }

    /// <summary>Subscribes to an event handler.</summary>
    /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
    /// <param name="handler">The event handler to subscribe.</param>
    public void Subscribe<TEventArgs>(Action<TEventArgs> handler)
    {
        var eventType = typeof(TEventArgs);
        lock (this.subscribers)
        {
            if (!this.subscribers.TryGetValue(eventType, out var priorityHandlers))
            {
                priorityHandlers = [];
                this.subscribers.Add(eventType, priorityHandlers);
            }

            var priorityAttribute = handler.Method.GetCustomAttribute<PriorityAttribute>();
            var priority = priorityAttribute?.Priority ?? 0;
            if (!priorityHandlers.TryGetValue(priority, out var handlers))
            {
                handlers = [];
                priorityHandlers.Add(priority, handlers);
            }

            handlers.Add(handler);
        }
    }

    /// <summary>Unsubscribes an event handler from an event.</summary>
    /// <param name="handler">The event handler to unsubscribe.</param>
    /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
    public void Unsubscribe<TEventArgs>(Action<TEventArgs> handler)
    {
        var eventType = typeof(TEventArgs);
        lock (this.subscribers)
        {
            if (!this.subscribers.TryGetValue(eventType, out var priorityHandlers))
            {
                return;
            }

            var methodInfo = handler.Method;
            var priorityAttribute = methodInfo.GetCustomAttribute<PriorityAttribute>();
            var priority = priorityAttribute?.Priority ?? 0;
            if (!priorityHandlers.TryGetValue(priority, out var handlers))
            {
                return;
            }

            _ = handlers.Remove(handler);
            if (priorityHandlers.Count != 0)
            {
                return;
            }

            _ = this.subscribers.Remove(eventType);
        }
    }
}
