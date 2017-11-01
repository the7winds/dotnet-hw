namespace Multithreading.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;
    using System.Diagnostics;
    using System.Collections.Generic;

    [TestClass]
    public class BlockingArrayQueueTests
    {
        private const int queueLimit = 10;
        private IDictionary<string, IBlockingQueue<int>> queues = new Dictionary<string, IBlockingQueue<int>>();

        [TestInitialize]
        public void PrepareQueue()
        {
            queues["lockbased"] =  new LockBased.BlockingArrayQueue<int>(queueLimit);
            queues["lockfree"] = new  LockFree.BlockingArrayQueue<int>(queueLimit);
        }

        [DataTestMethod]
        [DataRow("lockbased")]
        [DataRow("lockfree")]
        public void EnqueDequeCorrectnessTest(string queueKey)
        {
            var queue = queues[queueKey];

            queue.Enque(42);
            queue.Enque(43);
            queue.Enque(44);

            var firstOut = queue.Deque();
            var secondOut = queue.Deque();

            Assert.AreEqual(42, firstOut);
            Assert.AreEqual(43, secondOut);
        }

        [DataTestMethod]
        [DataRow("lockbased")]
        [DataRow("lockfree")]
        public void DequeBlockTest(string queueKey)
        {
            var queue = queues[queueKey];

            var enqueThread = new Thread(() =>
            {
                Thread.Sleep(2000);
                queue.Enque(42);
            });

            var sw = Stopwatch.StartNew();
            enqueThread.Start();
            var val = queue.Deque();
            var time = sw.ElapsedMilliseconds;

            Assert.IsTrue(time > 1000);
        }

        [DataTestMethod]
        [DataRow("lockbased")]
        [DataRow("lockfree")]
        public void EnqueBlockTest(string queueKey)
        {
            var queue = queues[queueKey];

            for (int i = 0; i < queueLimit; i++)
            {
                queue.Enque(i);
            }

            var dequeThread = new Thread(() =>
            {
                Thread.Sleep(2000);
                queue.Deque();
            });

            var sw = Stopwatch.StartNew();
            dequeThread.Start();
            queue.Enque(42);
            var time = sw.ElapsedMilliseconds;

            Assert.IsTrue(time > 1000);
        }

        [DataTestMethod]
        [DataRow("lockbased")]
        [DataRow("lockfree")]
        public void TryEnqueTest(string queueKey)
        {
            var queue = queues[queueKey];

            for (var i = 0; i < queueLimit; i++)
            {
                Assert.IsTrue(queue.TryEnque(i));
            }

            Assert.IsFalse(queue.TryEnque(42));
        }

        [DataTestMethod]
        [DataRow("lockbased")]
        [DataRow("lockfree")]
        public void TryDenqueTest(string queueKey)
        {
            var queue = queues[queueKey];

            for (var i = 0; i < queueLimit; i++)
            {
                Assert.IsTrue(queue.TryEnque(i));
            }

            for (var i = 0; i < queueLimit; i++)
            {
                int val;
                Assert.IsTrue(queue.TryDeque(out val));
                Assert.AreEqual(i, val);
            }
        }

    }
}