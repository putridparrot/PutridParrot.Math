using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using NUnit.Framework;
using PutridParrot.Math;

namespace Tests.PutridParrot.Math
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class FunctionTests
    {
        [Test]
        public void IsTriangular_IterateTriangularNumbers_ExpectTrue()
        {
            var current = 1.0;
            for (var i = 2; i < 100; i++)
            {
                Assert.IsTrue(Function.IsTriangular(current));
                current += i;
            }
        }

        [TestCase(0, 1)]
        [TestCase(3, 6)]
        [TestCase(-3, -6)]
        [TestCase(12, 479001600)]
        public void Factorial_BigInteger_Tests(int x, int expected)
        {
            Assert.IsTrue(BigInteger.Compare(new BigInteger(expected), Function.Factorial(new BigInteger(x))) == 0);
        }

        [TestCase(0, 1)]
        [TestCase(3, 6)]
        [TestCase(-3, -6)]
        [TestCase(12, 479001600)]
        public void Factorial_Tests(int x, int expected)
        {
            Assert.AreEqual(expected, Function.Factorial(x));
        }

        [TestCase(4, 2, 6)]
        public void BinomialCoefficient_Tests(int n, int k, int expected)
        {
            Assert.AreEqual(expected, Function.BinomialCoefficient(n, k));
        }
    }
}
