namespace MyNUnit
{
    using System;

#pragma warning disable SA1649 // File name must match first type name
#pragma warning disable SA1402 // File may only contain a single class
    [AttributeUsage(AttributeTargets.All)]
    public class TestAttribute : Attribute
    {
        public string Ignore;
        public Type Expected;
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BeforeAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    public class AfterAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    public class BeforeClassAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    public class AfterClassAttribute : Attribute
    {
    }
#pragma warning restore SA1402 // File may only contain a single class
#pragma warning restore SA1649 // File name must match first type name
}
