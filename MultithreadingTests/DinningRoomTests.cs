namespace Multithreading.Philosophers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class DinningRoomTests
    {
        [TestMethod]
        public void DinningRoomTest()
        {
            var dinningRoom = new DinningRoom(5);

            dinningRoom.Run(5000);

            var allHadDinner = dinningRoom.GetDinnerCounters()
                .Select(c => c > 0)
                .Aggregate(true, (acc, v) => acc && v);

            Assert.IsTrue(allHadDinner);
        }
    }
}