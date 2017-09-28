namespace MyNUnit
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class TestReport
    {
        private Type testObjType;
        private List<RunReport> runReports = new List<RunReport>();

        public TestReport(Type test)
        {
            this.testObjType = test;
        }

        public void AddRunResult(MethodInfo method, bool success, long time)
        {
            this.runReports.Add(new RunReport(method, success, time));
        }

        public void Print()
        {
            Console.WriteLine($"Test class: {this.testObjType.Name}");

            foreach (var report in this.runReports)
            {
                report.Print();
            }

            Console.WriteLine();
        }

        private class RunReport
        {
            private MethodInfo method;
            private bool success;
            private long time;

            public RunReport(MethodInfo method, bool success, long time)
            {
                this.method = method;
                this.success = success;
                this.time = time;
            }

            public void Print()
            {
                Console.WriteLine($"[{(this.success ? "pass" : "fail")}] {(this.success ? $"({this.time} ms)" : string.Empty)} {this.method.Name}");
            }
        }
    }
}
