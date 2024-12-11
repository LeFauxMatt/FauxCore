namespace LeFauxMods.Common.Models;

using System;

/// <summary>Represents an attribute used to specify the priority of a subscriber method.</summary>
/// <remarks>Initializes a new instance of the <see cref="PriorityAttribute" /> class.</remarks>
/// <param name="priority">The priority level for the subscriber.</param>
[AttributeUsage(AttributeTargets.Method)]
internal sealed class PriorityAttribute(int priority) : Attribute
{
    public int Priority { get; } = priority;
}
