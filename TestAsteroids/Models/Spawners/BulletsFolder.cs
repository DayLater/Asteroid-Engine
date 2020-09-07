using System;
using System.Collections;
using System.Collections.Generic;
using AsteroidsEngine;
using AsteroidsEngine.Entities;

namespace TestAsteroids.Models.Spawners
{
    public class BulletsFolder: IEnumerable<Bullet>
    {
        private readonly HashSet<Bullet> bullets;
        private HashSet<Bullet> bulletsToDelete;

        public BulletsFolder()
        {
            bullets = new HashSet<Bullet>(64);
            bulletsToDelete = new HashSet<Bullet>(8);
        }

        public void MakeShoot(float angle, Vector position)
        {
            var bullet = new Bullet(position, angle);
            bullets.Add(bullet);
        }

        public void AddBulletToDelete(Bullet bullet)
        {
            bulletsToDelete.Add(bullet);
        }

        public void DeleteNonActiveBullets()
        {
            if (bulletsToDelete.Count == 0) return;
            foreach (var bulletToDelete in bulletsToDelete)
                bullets.Remove(bulletToDelete);
            bulletsToDelete = new HashSet<Bullet>(8);
        }

        public int Count => bullets.Count;

        public IEnumerator<Bullet> GetEnumerator()
        {
            foreach (var bullet in bullets)
                yield return bullet;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
