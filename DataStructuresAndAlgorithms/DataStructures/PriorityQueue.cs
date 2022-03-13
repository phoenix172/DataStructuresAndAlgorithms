namespace ConsoleApp1;

public class PriorityQueue<T>
    where T: IComparable
{
    private readonly Heap<T> _heap = Heap<T>.Create();

    public bool IsEmpty => _heap.Maximum().IsEmpty;

    public void Enqueue(T item) => _heap.Insert(item);

    public T Dequeue() => _heap.ExtractMax().Value;

    public T Peek() => _heap.Maximum().Value;
}