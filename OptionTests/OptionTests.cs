using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Option.Tests
{
    [TestClass()]
    public class OptionTests
    {
        [TestMethod()]
        public void SomeTest()
        {
            Assert.IsTrue(Option<int>.Some(42).IsSome());
        }

        [TestMethod()]
        public void NoneTest()
        {
            Assert.IsTrue(Option<int>.None().IsNone());
        }

        [TestMethod()]
        public void ValueTest()
        {
            Assert.AreEqual(42, Option<int>.Some(42).Value());
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void ValueTestShouldFail()
        {
            Option<int>.None().Value();
        }

        [TestMethod()]
        public void MapNoneTest()
        {
            Assert.IsTrue(Option<int>.None().Map<string>(_ => _.ToString()).IsNone());
        }

        [TestMethod()]
        public void MapSomeTest()
        {
            Assert.AreEqual("42", Option<int>.Some(42).Map<string>(_ => _.ToString()).Value());
        }

        [TestMethod()]
        public void FlattenNoneTest()
        {
            Assert.IsTrue(Option<int>.Flatten(Option<Option<int>>.None()).IsNone());
        }

        [TestMethod()]
        public void FlattenSomeTest()
        {
            var o1 = Option<int>.Some(42);
            var o2 = Option<Option<int>>.Some(o1);
            Assert.AreEqual(42, Option<int>.Flatten(o2).Value());
        }
    }
}