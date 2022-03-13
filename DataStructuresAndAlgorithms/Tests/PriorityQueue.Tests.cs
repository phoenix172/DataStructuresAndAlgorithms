using System.Diagnostics.Contracts;
using DataStructuresAndAlgorithms.DataStructures;
using NUnit.Framework;

namespace DataStructuresAndAlgorithms.Tests
{
    public class PriorityQueueTests
    {
        [Test]
        public void Enqueue_DequeuesItemsInCorrectOrder()
        {
            PriorityQueue<int> queue = new();

            queue.Enqueue(5);
            queue.Enqueue(5);
            queue.Enqueue(6);
            queue.Enqueue(9);
            queue.Enqueue(10);

            int? prev = null;
            while (!queue.IsEmpty)
            {
                int item = queue.Dequeue();
                if(prev != null)
                    Assert.GreaterOrEqual(prev, item);
            }
        }
    }
}