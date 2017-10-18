namespace MyNUnit
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using static MyNUnit.TestReport;
    using static MyNUnit.TestReport.RunReport;

    /// <summary>
    /// Represents tests for an assembly
    /// </summary>
    public class TestSystem
    {
        private readonly Assembly assembly;
        private readonly List<TestClassContext> testableTypes;

        public TestSystem(Assembly assembly)
        {
            this.assembly = assembly;
            this.testableTypes = new List<TestClassContext>();
        }

        public void Test()
        {
            this.CollectTestableTypes();
            this.RunTests();
        }

        private void RunTests()
        {
            this.testableTypes.ForEach(type => type.Run().Print());
        }

        private bool ContainsTestMethod(Type type)
        {
            return type.GetMethods()
                .SelectMany(method => method.CustomAttributes)
                .Any(attr => attr.AttributeType == typeof(TestAttribute));
        }

        private void CollectTestableTypes()
        {
            this.assembly.ExportedTypes
                .ToList()
                .FindAll(type => this.ContainsTestMethod(type))
                .ForEach(type => this.testableTypes.Add(new TestClassContext(type)));
        }

        /// <summary>
        /// Represents a class with tests.
        /// </summary>
        private class TestClassContext
        {
            private static readonly Type TestType = typeof(TestAttribute);
            private static readonly Type BeforeType = typeof(BeforeAttribute);
            private static readonly Type AfterType = typeof(AfterAttribute);
            private static readonly Type BeforeClassType = typeof(BeforeClassAttribute);
            private static readonly Type AfterClassType = typeof(AfterClassAttribute);

            private static readonly List<Type> TestAttributesTypes = new List<Type>
            {
                TestType,
                BeforeType,
                AfterType,
                BeforeClassType,
                AfterClassType
            };

            private readonly Type testObjType;
            private readonly MethodInfo beforeClass;
            private readonly MethodInfo afterClass;
            private readonly MethodInfo before;
            private readonly MethodInfo after;
            private readonly List<TestContext> tests;

            public TestClassContext(Type type)
            {
                this.testObjType = type;
                this.tests = new List<TestContext>();

                foreach (var method in type.GetMethods())
                {
                    var attr = method
                        .GetCustomAttributes()
                        .FirstOrDefault(a => TestAttributesTypes.Contains(a.GetType()));

                    if (attr == null)
                    {
                        continue;
                    }

                    var attrType = attr.GetType();

                    if (attrType == TestType)
                    {
                        this.tests.Add(new TestContext(attr, method));
                    }
                    else if (attrType == BeforeType)
                    {
                        this.before = method;
                    }
                    else if (attrType == AfterType)
                    {
                        this.after = method;
                    }
                    else if (attrType == BeforeClassType)
                    {
                        this.beforeClass = method;
                    }
                    else if (attrType == AfterClassType)
                    {
                        this.afterClass = method;
                    }
                }
            }

            public TestReport Run()
            {
                var testReport = new TestReport(this.testObjType);
                var testObj = Activator.CreateInstance(this.testObjType);

                this.beforeClass?.Invoke(testObj, null);
                this.tests.ForEach(test => testReport.AddRunResult(test.Run(testObj, this.before, this.after)));
                this.afterClass?.Invoke(testObj, null);

                return testReport;
            }

            private class TestContext
            {
                private readonly TestAttribute attrubute;
                private readonly MethodInfo method;

                public TestContext(Attribute attribute, MethodInfo method)
                {
                    this.method = method;
                    this.attrubute = attribute as TestAttribute;
                }

                public RunReport Run(object testObj, MethodInfo before, MethodInfo after)
                {
                    if (this.attrubute.Ignore != null)
                    {
                        return new RunReport(this.method, RunStatus.IGNORED, this.attrubute.Ignore);
                    }

                    before?.Invoke(testObj, null);

                    Exception catched = null;
                    var timer = Stopwatch.StartNew();
                    try
                    {
                        this.method.Invoke(testObj, null);
                    }
                    catch (Exception e)
                    {
                        catched = e.GetBaseException();
                    }

                    timer.Stop();

                    after?.Invoke(testObj, null);

                    var status = RunStatus.FAILED;

                    if (this.attrubute.Expected == catched?.GetType())
                    {
                        status = RunStatus.SUCCESS;
                    }

                    return new RunReport(this.method, status, timer.ElapsedMilliseconds);
                }
            }
        }
    }
}
