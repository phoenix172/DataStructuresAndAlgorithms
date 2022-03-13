namespace DataStructuresAndAlgorithms.DataStructures;

public readonly struct TreeItem<T> : IComparable
    where T : IComparable
{
    public static readonly TreeItem<T> Empty = new() { IsEmpty = true };

    public TreeItem(T item)
    {
        Value = item;
        IsEmpty = false;
    }

    public bool IsEmpty { get; private init; }
    public T Value { get; }

    public int CompareTo(object? obj)
    {
        if (obj is TreeItem<T> treeItem)
        {
            return treeItem.IsEmpty || IsEmpty ? 0 : Value.CompareTo(treeItem.Value);
        }

        return 0;
    }

    public override string? ToString() => Value?.ToString();
    public bool Equals(TreeItem<T> other) => EqualityComparer<T>.Default.Equals(Value, other.Value);
    public override bool Equals(object? obj) => obj is TreeItem<T> other && Equals(other);
    public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);

    public static bool operator <(TreeItem<T> left, TreeItem<T> right) => left.CompareTo(right) < 0;
    public static bool operator >(TreeItem<T> left, TreeItem<T> right) => left.CompareTo(right) > 0;
    public static bool operator ==(TreeItem<T> left, TreeItem<T> right) => left.CompareTo(right) == 0;
    public static bool operator !=(TreeItem<T> left, TreeItem<T> right) => left.CompareTo(right) != 0;
}