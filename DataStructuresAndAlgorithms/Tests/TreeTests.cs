using NUnit.Framework;

namespace ConsoleApp1;

public class TreeTests
{
    [Test]
    public void Ancestors_ReturnsCorrectResult()
    {
        var result = CreateTree().Ancestors(26);
        CollectionAssert.AreEqual(new[] { 12, 5, 2, 0 }, result);
    }


    [Test]
    public void NonLeafIndices_ReturnsCorrectResult()
    {
        Tree<int> tree = new(TestData.SampleArray);
        var result = tree.InnerVertices().ToArray();
        CollectionAssert.AreEquivalent(Enumerable.Range(0, 13), result);
    }

    [Test]
    public void NonLeafIndices_LastItem_EqualsFloorNOver2()
    {
        Tree<int> tree = new(TestData.SampleArray);
        var innerVertices = tree.InnerVertices();
        var result = innerVertices.Last();
        var expected = (int)Math.Floor(TestData.SampleArray.Length / 2d) - 1;
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void Left_ReturnsCorrectResult()
    {
        Tree<int> tree = CreateTree();
        var result = tree.Left(5);
        Assert.AreEqual(11, result);
    }

    [Test]
    public void Right_ReturnsCorrectResult()
    {
        Tree<int> tree = CreateTree();
        var result = tree.Right(5);
        Assert.AreEqual(12, result);
    }

    [Test]
    public void Right_ReturnsCorrectResult_Negative()
    {
        Tree<int> tree = CreateTree();
        var result = tree.Right(26);
        Assert.AreEqual(-1, result);
    }

    [Test]
    public void Left_ReturnsCorrectResult_Negative()
    {
        Tree<int> tree = CreateTree();
        var result = tree.Left(26);
        Assert.AreEqual(-1, result);
    }

    [Test]
    public void Compare_ComparesCorrectly()
    {
        Tree<int> tree = CreateTree();
        Assert.IsTrue(tree.Compare(7, 5) < 0);
    }

    [Test]
    public void CompareWrapper_ComparesLessThanCorrectly()
    {
        Tree<int> tree = CreateTree();
        Assert.IsTrue(tree[7] < tree[5]);
    }

    [Test]
    public void CompareWrapper_ComparesEqualsCorrectly()
    {
        Tree<int> tree = CreateTree();
        Assert.IsTrue(tree[11] == tree[12]);
    }

    [Test]
    public void CompareWrapper_ComparesGreaterThanCorrectly()
    {
        Tree<int> tree = CreateTree();
        Assert.IsTrue(tree[14] > tree[15]);
    }

    private static Tree<int> CreateTree()
    {
        return new Tree<int>(TestData.SampleArray);
    }
}