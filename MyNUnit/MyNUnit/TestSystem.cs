namespace MyNUnit
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Represents tests for an assebly
    /// </summary>
    public class TestSystem
    {
        private Assembly assembly;
        private List<TestTypeContext> testableTypes = new List<TestTypeContext>();

        public TestSystem(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public void Test()
        {
            this.CollectTestableTypes();
            this.RunTests();
        }

        private void RunTests()
        {
            foreach (var type in this.testableTypes)
            {
                type.Run().Print();
            }
        }

        private bool ContainsTestMethod(Type type)
        {
            var name = type.Name;
            foreach (var method in type.GetMethods())
            {
                foreach (var attr in method.CustomAttributes)
                {
                    if (attr.AttributeType == typeof(TestAttribute))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void CollectTestableTypes()
        {
            foreach (var type in this.assembly.ExportedTypes)
            {
                if (this.ContainsTestMethod(type))
                {
                    this.testableTypes.Add(new TestTypeContext(type));
                }
            }
        }

        /// <summary>
        /// Represents a class with tests.
        /// </summary>
        private class TestTypeContext
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

            private Type testObjType;
            private MethodInfo beforeClass;
            private MethodInfo afterClass;
            private MethodInfo before;
            private MethodInfo after;
            private List<MethodInfo> tests = new List<MethodInfo>();

            public TestTypeContext(Type type)
            {
                this.testObjType = type;

                foreach (var method in type.GetMethods())
                {
                    Type attrType = method.GetCustomAttributes()
                            .Select(attr => attr.GetType())
                            .Where(t => TestAttributesTypes.Contains(t))
                            .FirstOrDefault();

                    if (attrType == TestType)
                    {
                        this.tests.Add(method);
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
                TestReport testReport = new TestReport(this.testObjType);
                object testObj = System.Activator.CreateInstance(this.testObjType);

                var passedTests = 0;

                if (this.beforeClass != null)
                {
                    this.beforeClass.Invoke(testObj, null);
                }

                foreach (var test in this.tests)
                {
                    if (this.before != null)
                    {
                        this.before.Invoke(testObj, null);
                    }

                    try
                    {
                        var timer = Stopwatch.StartNew();
                        test.Invoke(testObj, null);
                        timer.Stop();
                        passedTests += 1;
                        testReport.AddRunResult(test, true, timer.ElapsedMilliseconds);
                    } catch
                    {
                        testReport.AddRunResult(test, false, 0);
                    }

                    if (this.after != null)
                    {
                        this.after.Invoke(testObj, null);
                    }
                }

                if (this.afterClass != null)
                {
                    this.afterClass.Invoke(testObj, null);
                }

                return testReport;
            }
        }
    }
}
