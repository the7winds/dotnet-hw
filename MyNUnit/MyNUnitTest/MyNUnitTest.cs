namespace MyNUnit.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MyNUnitTest
    {
        [TestInitialize]
        public void RunTestSystem()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var system = new TestSystem(assembly);
                system.Test();
            }
        }

        [TestMethod]
        public void SimpleRunTest()
        {
            using (FileStream input = new FileStream(Example01.OutputName, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(input);
                Assert.AreEqual("TEST", reader.ReadLine());
            }
        }

        [TestMethod]
        public void RunTest()
        {
            using (FileStream input = new FileStream(Example02.OutputName, FileMode.Open, FileAccess.Read))
            {
                var testsText = new List<string>() { "TEST-01", "TEST-02" };
                var reader = new StreamReader(input);
                Assert.AreEqual("BEFORE CLASS", reader.ReadLine());
                Assert.AreEqual("BEFORE", reader.ReadLine());
                Assert.IsTrue(testsText.Remove(reader.ReadLine()));
                Assert.AreEqual("AFTER", reader.ReadLine());
                Assert.AreEqual("BEFORE", reader.ReadLine());
                Assert.IsTrue(testsText.Remove(reader.ReadLine()));
                Assert.AreEqual("AFTER", reader.ReadLine());
                Assert.AreEqual("BEFORE", reader.ReadLine());
                Assert.AreEqual("AFTER", reader.ReadLine());
                Assert.AreEqual("AFTER CLASS", reader.ReadLine());
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(Example01.OutputName);
            File.Delete(Example02.OutputName);
        }
    }
}