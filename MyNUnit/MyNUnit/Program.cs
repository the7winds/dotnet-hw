namespace MyNUnit
{
    using System;
    using System.Reflection;

    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: ./mynunit [ASSEMBLY PATH]");
                return;
            }

            var assembly = Assembly.LoadFrom(args[0]);
            new TestSystem(assembly).Test();
        }
    }
}
