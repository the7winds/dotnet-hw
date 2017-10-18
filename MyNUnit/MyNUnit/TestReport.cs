namespace MyNUnit
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class TestReport
    {
        private readonly Type testObjType;
        private readonly List<RunReport> runReports;
        private int success;
        private int total;

        public TestReport(Type test)
        {
            this.testObjType = test;
            this.runReports = new List<RunReport>();
        }

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
            private readonly RunStatus status;
            private readonly MethodInfo method;
            private readonly long time;
            private readonly string ignored;

            public RunReport(MethodInfo method, RunStatus status, string ignored)
            {
                this.method = method;
                this.ignored = ignored;
                this.status = status;
            }

            public RunReport(MethodInfo method, RunStatus status, long time)
            {
                this.method = method;
                this.status = status;
                this.time = time;
            }

            public enum RunStatus
            {
                SUCCESS,
                IGNORED,
                FAILED
            }

            public RunStatus Status => this.status;

            public void Print() => Console.WriteLine(this.GetRepr());

            private string GetRepr()
            {
                string state = string.Empty;

                switch (this.Status)
                {
                    case RunStatus.IGNORED:
                        state = $"{this.ignored}";
                        break;
                    case RunStatus.SUCCESS:
                        state = $"({this.time} ms)";
                        break;
                    case RunStatus.FAILED:
                        break;
                }

                return $"[{this.Status}] {state} {this.method.Name}]";
            }
        }
    }
}