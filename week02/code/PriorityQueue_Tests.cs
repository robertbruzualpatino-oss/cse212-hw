using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: 
    // Expected Result: 
    // Defect(s) Found: 
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("ItemA", 2);
        priorityQueue.Enqueue("ItemB", 5);
        priorityQueue.Enqueue("ItemC", 1);

        Assert.AreEqual("ItemB", priorityQueue.Dequeue());
        Assert.AreEqual("ItemA", priorityQueue.Dequeue());
        Assert.AreEqual("ItemC", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: 
    // Expected Result: 
    // Defect(s) Found: 
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("ItemA", 4);
        priorityQueue.Enqueue("ItemB", 4);
        priorityQueue.Enqueue("ItemC", 2);

        Assert.AreEqual("ItemA", priorityQueue.Dequeue());
        Assert.AreEqual("ItemB", priorityQueue.Dequeue());
        Assert.AreEqual("ItemC", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.
    [TestMethod]

    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("An InvalidOperationException should have been thrown for an empty queue.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
        }
    }

    [TestMethod]

    public void TestPriorityQueue_InterleavedOperations()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Mid", 3);

        Assert.AreEqual("Mid", priorityQueue.Dequeue());

        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Lowest", 0);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("Lowest", priorityQueue.Dequeue());
    }
}