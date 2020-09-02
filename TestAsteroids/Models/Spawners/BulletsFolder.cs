using System;
using System.Collections;
using System.Collections.Generic;
using AsteroidsEngine;
using AsteroidsEngine.Entities;

namespace TestAsteroids.Models.Spawners
{
    public class BulletsFolder: IEnumerable<Bullet>
    {
        private readonly Queue<Bullet> activeBullets;
        private readonly Queue<Bullet> nonActiveBullets;
        private readonly Vector folderPosition;

        public BulletsFolder(int width, int height)
        {
            folderPosition = new Vector(width* width, height * height);
            activeBullets = new Queue<Bullet>();
            nonActiveBullets = new Queue<Bullet>();
            FullNonActiveBullets();
        }

        private void FullNonActiveBullets()
        {
            for (int i = 0; i < 100; i++)
            {
                var bullet = new Bullet(folderPosition, 0);
                nonActiveBullets.Enqueue(bullet);
            }
        }

        public void MakeShoot(float angle, Vector position)
        {
            if (nonActiveBullets.Count > 0)
            {
                var bullet = nonActiveBullets.Dequeue();
                bullet.ChangePosition(position);
                bullet.Speed = new Vector(0, -15).Rotate(Vector.Zero, angle);
                bullet.Turn(angle);
                activeBullets.Enqueue(bullet);
            }
        }

        public void ReturnActiveBullet(Bullet bullet)
        {
            activeBullets.Enqueue(bullet);
        }

        public void DeactivateBullet(Bullet bullet)
        {
            nonActiveBullets.Enqueue(bullet);
            bullet.ChangePosition(folderPosition);
            bullet.Speed = Vector.Zero;
            bullet.Turn(-bullet.Angle);
        }

        public Bullet GetNextActiveBullet()
        {
            if (activeBullets.Count <= 0)
                throw new IndexOutOfRangeException("There are no active bullets");
            return activeBullets.Dequeue();
        }

        public int Count => activeBullets.Count;

        public IEnumerator<Bullet> GetEnumerator()
        {
            foreach (var bullet in activeBullets)
                yield return bullet;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
