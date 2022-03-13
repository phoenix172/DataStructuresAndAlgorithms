using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace ConsoleApp1;

public class Tree<T> : IReadOnlyList<TreeItem<T>> where T : IComparable
{
    private readonly List<TreeItem<T>> _vertices;

    public Tree(params T[] items)
    {
        _vertices = new(items.Select(x => new TreeItem<T>(x)));
    }

    public int VertexCount => _vertices.Count;

    public IEnumerable<int> InnerVertices() => Enumerable.Range(0, (int) Math.Floor(_vertices.Count / 2d));

    public bool Exists(int index) => index > -1 && index < VertexCount;

    public int Right(int index)
    {
        int rightIndex = 2 * (index + 1);
        return Exists(rightIndex) ? rightIndex : -1;
    }

    public int Left(int index)
    {
        int leftIndex = 2 * (index + 1) - 1;
        return Exists(leftIndex) ? leftIndex : -1;
    }

    public int Parent(int index) => index + 1 != 1 ? (int) Math.Floor((index + 1) / 2d) - 1 : index;

    public int Parent(int index, int parentDegree) =>
        parentDegree > 1 ? Parent(Parent(index), parentDegree - 1) : Parent(index);

    public IEnumerable<int> Ancestors(int index)
    {
        int lastAncestor;
        int currentAncestor = index;
        do
        {
            currentAncestor = Parent(lastAncestor = currentAncestor);
            if (lastAncestor != currentAncestor)
                yield return currentAncestor;
        } while (currentAncestor != lastAncestor);
    }

    public Tree<T> Heapify(int swapRootIndex)
    {
        int lastNonLeafIndex = LastNonLeafIndex();

        while (swapRootIndex <= lastNonLeafIndex)
        {
            int leftIndex = Left(swapRootIndex);
            int rightIndex = Right(swapRootIndex);

            bool shouldSwap = this[leftIndex] > this[swapRootIndex] ||
                              this[rightIndex] > this[swapRootIndex];
            if (!shouldSwap) return this;


            int largestIndex = this[leftIndex] > this[rightIndex]
                ? leftIndex
                : Exists(rightIndex) ? rightIndex : leftIndex;

            Swap(largestIndex, swapRootIndex);

            swapRootIndex = largestIndex;
        }

        return this;
    }

    public int LastNonLeafIndex() => InnerVertices().LastOrDefault(-1);


    public Tree<T> Swap(int a, int b)
    {
        (_vertices[a], _vertices[b]) = (_vertices[b], _vertices[a]);
        return this;
    }

    public int Compare(int a, int b) => this[a].CompareTo(this[b]);

    public Tree<T> Trim(int count= 1)
    {
        _vertices.RemoveAt(_vertices.Count-1);
        return this;
    }

    public TreeItem<T> this[int index] => Exists(index) ? _vertices[index] : TreeItem<T>.Empty;

    public IEnumerator<TreeItem<T>> GetEnumerator() => _vertices.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _vertices.GetEnumerator();
    public int Count => _vertices.Count;
}

public struct TreeItem<T> : IComparable
    where T : IComparable
{
    public static readonly TreeItem<T> Empty = new(){ IsEmpty = true };

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

    public override string? ToString()
    {
        return Value?.ToString();
    }

    public bool Equals(TreeItem<T> other)
    {
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is TreeItem<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Value);
    }

    public static bool operator <(TreeItem<T> left, TreeItem<T> right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(TreeItem<T> left, TreeItem<T> right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator ==(TreeItem<T> left, TreeItem<T> right)
    {
        return left.CompareTo(right) == 0;
    }

    public static bool operator !=(TreeItem<T> left, TreeItem<T> right)
    {
        return left.CompareTo(right) != 0;
    }
}