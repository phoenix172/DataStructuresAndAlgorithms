using System.Security.Cryptography;
using NUnit.Framework;

namespace ConsoleApp1;

public class HeapTests
{
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