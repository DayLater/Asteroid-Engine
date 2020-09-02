using System;
using System.Collections.Generic;
using AsteroidsEngine.Entities;

namespace AsteroidsEngine
{
    public class AsteroidsSpawner
    {
        private readonly Random rnd = new Random();
        private readonly List<Enemy> folder;

        public AsteroidsSpawner(List<Enemy> enemyFolder)
        {
            folder = enemyFolder;
        }

        public void CreateBigAsteroids(int level, int width, int height)
        {
            for (int i = 0; i < level; i++)
            {
                var y = rnd.Next(0, height);
                var x = rnd.Next(0, 2) == 0 ? 1 : width - 1;
                var position = new Vector(x, y);
                var asteroid = new Asteroid(position, 30);
                folder.Add(asteroid);
            }
        }

        public void CreateChildAsteroids(Asteroid asteroid)
        {
            for (int i = 0; i < 3; i++)
            {
                var child = new Asteroid(asteroid.Position, 15, true);
                folder.Add(child);
            }
        }
    }
}
