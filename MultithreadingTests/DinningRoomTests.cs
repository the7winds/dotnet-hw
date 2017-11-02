namespace Multithreading.Philosophers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;
    using System.Collections.Generic;

    [TestClass]
    public class DinningRoomTests
    {
        [TestMethod]
        public void DinningRoomTest()
        {
            var dinningRoom = new DinningRoom(5);

            var threads = new LinkedList<Thread>();

            foreach (var philosopher in dinningRoom.Philosophers)
            {
                var thread = new Thread(() => philosopher.Live());
                threads.AddLast(thread);
                thread.Start();
            }

            Thread.Sleep(5000);

            foreach (var thread in threads)
            {
                thread.Interrupt();
                thread.Join();
            }
        }
    }
}