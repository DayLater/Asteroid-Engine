using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidsEngine;
using NUnit.Framework;

namespace TestsAsteroidEngine
{
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void TestGetHashCode()
        {
            var vector = new Vector(4, 5);
            var hash1 = vector.GetHashCode();
            var hash2 = vector.GetHashCode();
            Assert.AreEqual(hash1, hash2);

            var other = new Vector(5, 4);
            Assert.IsFalse(hash1 == other.GetHashCode());
        }
    }
}
