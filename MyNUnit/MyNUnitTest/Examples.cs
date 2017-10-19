namespace MyNUnitTest
{
    using System;
    using System.IO;
    using MyNUnit;

#pragma warning disable SA1649 // File name must match first type name
#pragma warning disable SA1402 // File may only contain a single class
    public class Example01
    {
        public static readonly string OutputName = $"{typeof(Example01).Name}.output";

        [Test]
        public void Test()
        {
            File.AppendAllText(OutputName, "TEST\n");
        }
    }

    public class Example02
    {
        public static readonly string OutputName = $"{typeof(Example02).Name}.output";

        [BeforeClass]
        public void BeforeClass()
        {
            File.AppendAllText(OutputName, "BEFORE CLASS\n");
        }

        [Before]
        public void Before()
        {
            File.AppendAllText(OutputName, "BEFORE\n");
        }

        [Test]
        public void Test01()
        {
            File.AppendAllText(OutputName, "TEST-01\n");
        }

        [Test]
        public void Test02()
        {
            File.AppendAllText(OutputName, "TEST-02\n");
        }

        [Test(Ignore = "test ignore")]
        public void Test03()
        {
            File.AppendAllText(OutputName, "TEST-03\n");
        }

        [Test(Expected = typeof(Exception))]
        public void Test04() => throw new Exception();

        [After]
        public void After()
        {
            File.AppendAllText(OutputName, "AFTER\n");
        }

        [AfterClass]
        public void AfterClass()
        {
            File.AppendAllText(OutputName, "AFTER CLASS\n");
        }
    }
}

#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name