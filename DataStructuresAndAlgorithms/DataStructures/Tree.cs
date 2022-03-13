using System.Collections;

namespace DataStructuresAndAlgorithms.DataStructures;

public class Tree<T> : IReadOnlyList<TreeItem<T>> where T : IComparable
{
    private readonly List<TreeItem<T>> _vertices;

    public Tree(params T[] items)
    {
        _vertices = new(items.Select(x => new TreeItem<T>(x)));
    }

    public IEnumerable<int> InnerVertices() => Enumerable.Range(0, (int)Math.Floor(_vertices.Count / 2d));

    public bool Exists(int index) => index > -1 && index < Count;

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

    public int Parent(int index) => index + 1 != 1 ? (int)Math.Floor((index + 1) / 2d) - 1 : index;

    public int Parent(int index, int parentDegree) =>
        parentDegree > 1 ? Parent(Parent(index), parentDegree - 1) : Parent(index);

    public Tree<T> Append(T item)
    {
        _vertices.Add(new TreeItem<T>(item));
        return this;
    }

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

    public Tree<T> Trim(int count = 1)
    {
        _vertices.RemoveAt(_vertices.Count - 1);
        return this;
    }

    public TreeItem<T> this[int index] => Exists(index) ? _vertices[index] : TreeItem<T>.Empty;

    public IEnumerator<TreeItem<T>> GetEnumerator() => _vertices.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _vertices.GetEnumerator();
    public int Count => _vertices.Count;
}