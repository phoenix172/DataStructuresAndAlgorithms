namespace DataStructuresAndAlgorithms.DataStructures;

public class Heap<T>
    where T : IComparable
{
    private readonly Tree<T> _tree;

    private Heap(params T[] items)
    {
        _tree = BuildHeap(items);
    }

    public TreeItem<T> Maximum() => _tree[0];
    public TreeItem<T> ExtractMax() => ExtractMax(null);
    public Heap<T> Insert(T item)
    {
        var ancestors =_tree.Append(item).Ancestors(_tree.Count - 1);
        RemoveInversions(ancestors);
        return this;
    }

    public Heap<T> RemoveInversions(IEnumerable<int> indices)
    {
        indices.Aggregate(_tree, (current, i) => current.Heapify(i));
        return this;
    }

    public static Heap<T> Create(params T[] items)
    {
        return new Heap<T>(items);
    }
    
    private static Tree<T> BuildHeap(params T[] array)
    {
        Tree<T> tree = new(array);
        var nonLeafIndices = tree.InnerVertices().Reverse().ToList();
        var resultTree = nonLeafIndices.Aggregate(tree, (current, i) => current.Heapify(i));
        return resultTree;
    }


    public IEnumerable<(int, int)> Inversions() => Inversions(_tree);
    public IEnumerable<(int, int)> Inversions(int index) => Inversions(index, _tree);


    public static IEnumerable<(int, int)> Inversions(T[] array) => Inversions(new Tree<T>(array));
    public static IEnumerable<(int, int)> Inversions(Tree<T> tree) => tree.SelectMany((_, i) => Inversions(i, tree));
    public static IEnumerable<(int, int)> Inversions(int index, Tree<T> tree)
    {
        var result = tree
            .Ancestors(index)
            .Where(x => tree[x].CompareTo(tree[index]) < 0)
            .Select(x => (x, index));
        return result;
    }

    public IEnumerable<T> Sort()
    {
        return Enumerable.Range(0,_tree.Count-1).Reverse().Select(i =>
        {
            var result = ExtractMax(i).Value;
            return result;
        });
    }

    private TreeItem<T> ExtractMax(int? i)
    {
        i ??= _tree.Count - 1;
        TreeItem<T> result = _tree[0];
        _tree.Swap(0, i.Value).Trim().Heapify(0);
        return result;
    }
}