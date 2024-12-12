namespace LeFauxMods.Common.Utilities;

/// <inheritdoc />
internal sealed class ReverseComparer<T> : Comparer<T>
    where T : IComparable<T>
{
    /// <inheritdoc />
    public override int Compare(T? x, T? y)
    {
        var xNull = EqualityComparer<T?>.Default.Equals(x, default);
        var yNull = EqualityComparer<T?>.Default.Equals(y, default);
        if (xNull && yNull)
        {
            return 0;
        }

        if (xNull)
        {
            return 1;
        }

        if (yNull)
        {
            return -1;
        }

        return y?.CompareTo(x) ?? 0;
    }
}
