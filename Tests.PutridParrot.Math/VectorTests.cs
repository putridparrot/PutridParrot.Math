using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using PutridParrot.Math;

namespace Tests.PutridParrot.Math
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void Constructor_Empty_ExpectEmptyVector()
        {
            var v = new Vector();
            Assert.IsTrue(v.IsEmpty);
        }
        
    }
}
