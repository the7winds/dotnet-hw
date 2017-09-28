namespace MyNUnit.Test
{
    using System;
    using System.IO;

#pragma warning disable SA1649 // File name must match first type name
#pragma warning disable SA1402 // File may only contain a single class
    public class Example01
    {
        public static readonly string OutputName = $"{typeof(Example01).Name}.output";

        [Test]
        public void Test()
        {
            using (var output = new FileStream(OutputName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var writer = new StreamWriter(output))
                {
                    writer.WriteLine("TEST");
                }
            }
        }
    }

    public class Example02
    {
        public static readonly string OutputName = $"{typeof(Example02).Name}.output";

        private FileStream output;
        private StreamWriter writer;

        [BeforeClass]
        public void BeforeClass()
        {
            this.output = new FileStream(OutputName, FileMode.OpenOrCreate, FileAccess.Write);
            this.writer = new StreamWriter(this.output);
            this.writer.WriteLine("BEFORE CLASS");
        }

        [Before]
        public void Before() => this.writer.WriteLine("BEFORE");

        [Test]
        public void Test01() => this.writer.WriteLine("TEST-01");

        [Test]
        public void Test02() => this.writer.WriteLine("TEST-02");

        [After]
        public void After() => this.writer.WriteLine("AFTER");

        [AfterClass]
        public void AfterClass()
        {
            this.writer.WriteLine("AFTER CLASS");
            this.writer.Close();
            this.output.Close();
        }
    }
}

#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name