namespace Option.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class OptionTests
    {
        [TestMethod]
        public void SomeTest()
        {
            Assert.IsTrue(Option<int>.Some(42).IsSome);
        }

        [TestMethod]
        public void NoneTest()
        {
            Assert.IsTrue(Option<int>.None.IsNone);
        }

        [TestMethod]
        public void ValueTest()
        {
            Assert.AreEqual(42, Option<int>.Some(42).Value);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValueTestShouldFail()
        {
            var val = Option<int>.None.Value;
        }

        [TestMethod]
        public void MapNoneTest()
        {
            Assert.IsTrue(Option<int>.None.Map(_ => _.ToString()).IsNone);
        }

        [TestMethod]
        public void MapSomeTest()
        {
            Assert.AreEqual("42", Option<int>.Some(42).Map(_ => _.ToString()).Value);
        }

        [TestMethod]
        public void FlattenNoneTest()
        {
            Assert.IsTrue(Option<int>.Flatten(Option<Option<int>>.None).IsNone);
        }

        [TestMethod]
        public void FlattenSomeTest()
        {
            var o1 = Option<int>.Some(42);
            var o2 = Option<Option<int>>.Some(o1);
            Assert.AreEqual(42, Option<int>.Flatten(o2).Value);
        }

        [TestMethod]
        public void EqualsSomeTest()
        {
            var o1 = Option<int>.Some(42);
            var o2 = Option<int>.Some(42);
            var o3 = Option<int>.Some(43);

            Assert.AreEqual(o1, o2);
            Assert.AreNotEqual(o1, o3);
        }

        [TestMethod]
        public void EqualsNullTest()
        {
            var o = Option<int>.Some(42);
            Assert.IsFalse(o.Equals(null));
        }

        [TestMethod]
        public void EqualsNoneTest()
        {
            var o1 = Option<int>.None;
            var o2 = Option<int>.None;

            Assert.AreEqual(o1, o2);
        }

        [TestMethod]
        public void EqualsSomeNoneTest()
        {
            var o1 = Option<int>.Some(42);
            var o2 = Option<int>.None;

            Assert.AreNotEqual(o1, o2);
            Assert.AreNotEqual(o2, o1);
        }
    }
}