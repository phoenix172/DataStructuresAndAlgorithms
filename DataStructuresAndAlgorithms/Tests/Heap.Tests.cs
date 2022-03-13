using DataStructuresAndAlgorithms.DataStructures;
using NUnit.Framework;

namespace DataStructuresAndAlgorithms.Tests;

public class HeapTests
{
    [Test]
    public void Insert_NewMax_InsertsAsRoot()
    {
        var heap = Heap<int>.Create(TestData.SampleArray);
        heap.Insert(100);

        Assert.AreEqual(100, heap.Maximum().Value);
    }

    [Test]
    public void ExtractMax_ReturnsCorrectItemsInOrder()
    {
        var heap = Heap<int>.Create(TestData.SampleArray);
        
        Assert.AreEqual(44,heap.ExtractMax().Value);
        Assert.AreEqual(41,heap.ExtractMax().Value);
        Assert.AreEqual(35,heap.ExtractMax().Value);
        Assert.AreEqual(35, heap.ExtractMax().Value);
        Assert.AreEqual(32, heap.ExtractMax().Value);
        Assert.AreEqual(32, heap.ExtractMax().Value);
        Assert.AreEqual(30, heap.ExtractMax().Value);
    }

    [Test]
    public void EmptyItem_EqualToAny()
    {
        var tree = new Tree<int>(TestData.SampleArray);
        Assert.IsTrue(TreeItem<int>.Empty == tree[0]);
    }

    [Test]
    public void Inversions_ReturnsCorrectResult()
    {
        var resultArray = Heap<int>.Inversions(TestData.SampleArray);
        CollectionAssert.AreEquivalent(new[] {(2, 26), (5, 26), (12, 26)}, resultArray);
    }

    [Test]
    public void Inversions_ReturnsCorrectCount()
    {
        var resultArray = Heap<int>.Inversions(TestData.SampleArray);
        CollectionAssert.AreEquivalent(new[] {(2, 26), (5, 26), (12, 26)}, resultArray);
    }

    [Test]
    public void Sort_SortsCorrectly()
    {
        var result = Heap<int>.Create(TestData.SampleArray).Sort();
        result.ToList().ForEach(Console.WriteLine);
    }
    [Test]
    public void Heap_ContainsNoInversions()
    {
        var heap = Heap<int>.Create(TestData.SampleArray);

        Assert.AreEqual(3, Heap<int>.Inversions(TestData.SampleArray).Count());
        Assert.AreEqual(0,heap.Inversions().Count());
    }
}