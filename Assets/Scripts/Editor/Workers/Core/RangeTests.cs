using Assets.Scripts.Workers.Core;
using NUnit.Framework;
using System;

namespace Assets.Scripts.Editor.Workers.Core
{
    [Category("Core")]
    class RangeTests
    {
        [Test]
        public void DefaultConstructor()
        {
            Assert.IsNotNull(new Range());
        }

        [Test]
        public void Numeric_Constrcutor()
        {
            var sut = new Range(1, 2);

            Assert.AreEqual(1, sut.Min);
            Assert.AreEqual(2, sut.Max);
        }

        [Test]
        public void Numeric_Constrcutor_InverseOrder()
        {
            var sut = new Range(2, 1);

            Assert.AreEqual(2, sut.Min);
            Assert.AreEqual(1, sut.Max);
        }

        [Test]
        public void String_Constrcutor()
        {
            var sut = new Range("1-2");

            Assert.AreEqual(1, sut.Min);
            Assert.AreEqual(2, sut.Max);
        }

        [Test]
        public void String_Constrcutor_InvalidSeperator()
        {
            Assert.Throws<ArgumentException>(() => new Range("1:2"));
        }

        [Test]
        public void String_Constrcutor_InvalidMinValue()
        {
            Assert.Throws<ArgumentException>(() => new Range("a-2"));
        }

        [Test]
        public void String_Constrcutor_InvalidMaxValue()
        {
            Assert.Throws<ArgumentException>(() => new Range("1-a"));
        }

        [Test]
        public void WithinRange_InRange()
        {
            var sut = new Range(1, 5);

            Assert.IsTrue(sut.WithinRange(3));
        }

        [Test]
        public void WithinRange_OutOfRange()
        {
            var sut = new Range(1, 5);

            Assert.IsFalse(sut.WithinRange(6));
        }
    }
}
