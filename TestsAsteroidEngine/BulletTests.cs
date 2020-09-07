using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsteroidsEngine;
using AsteroidsEngine.Entities;
using NUnit.Framework;

namespace TestsAsteroidEngine
{
    [TestFixture]
    public class BulletTests
    {
        [Test]
        public void BulletReturnsSameHashCode()
        {
            var bullet = new Bullet(Vector.Zero, 0);
            var hash1 = bullet.GetHashCode();
            var hash2 = bullet.GetHashCode();
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void HashSetRemoveBullet()
        {
            var bullet = new Bullet(new Vector(1, 1), 90);
            var hashset = new HashSet<Bullet>(100);
            var hashset2 = new HashSet<Bullet>(10);
            hashset.Add(bullet);
            hashset2.Add(bullet);
            foreach (var b in hashset2)
            {
                hashset.Remove(b);
            }
            Assert.IsEmpty(hashset);
        }
    }
}
