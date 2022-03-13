namespace ConsoleApp1;

public class Heap<T>
    where T : IComparable
{
    private readonly Tree<T> _tree;

    private Heap(params T[] items)
    {
        _tree = BuildHeap(items);
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
            T result = _tree[0].Value;
            _tree.Swap(0, i).Trim().Heapify(0);
            return result;
        });
    }
}