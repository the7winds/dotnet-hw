namespace MyNUnit
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class TestReport
    {
        private readonly Type testObjType;
        private readonly List<RunReport> runReports = new List<RunReport>();
        private int success = 0;
        private int total = 0;

        public TestReport(Type test) => this.testObjType = test;

        public void AddRunResult(RunReport runReport)
        {
            this.runReports.Add(runReport);

            if (runReport.Status == RunReport.RunStatus.SUCCESS)
            {
                this.success += 1;
            }

            if (runReport.Status != RunReport.RunStatus.IGNORED)
            {
                this.total += 1;
            }
        }

        public void Print()
        {
            Console.WriteLine($"Test class: {this.testObjType.Name}");

            foreach (var report in this.runReports)
            {
                report.Print();
            }

            Console.WriteLine($"Passed: {this.success} / {this.total}");
            Console.WriteLine();
        }

        public class RunReport
        {
            public readonly RunStatus Status;
            private readonly MethodInfo method;
            private readonly long time;
            private readonly string ignored;

            public RunReport(MethodInfo method, string ignored)
            {
                this.method = method;
                this.ignored = ignored;
                this.Status = RunStatus.IGNORED;
            }

            public RunReport(MethodInfo method, RunStatus success, long time)
            {
                this.method = method;
                this.Status = success;
                this.time = time;
            }

            public void Print() => Console.WriteLine(this.GetRepr());

            private string GetRepr()
            {
                switch (this.Status)
                {
                    case RunStatus.IGNORED:
                        return $"[{this.Status.ToString()}] {this.ignored} {this.method.Name}";
                    default:
                        return $"[{this.Status.ToString()}] {(this.Status == RunStatus.SUCCESS ? $"({this.time} ms)" : string.Empty)} {this.method.Name}";
                }
            }

            public enum RunStatus
            {
                SUCCESS,
                IGNORED,
                FAILED
            }
        }
    }
}