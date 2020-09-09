using System;
using System.Collections.Generic;
using TestAsteroids.Models.Enemies;

namespace AsteroidsEngine.Entities
{
    public class UfoSpawner
    {
        private readonly Random random = new Random();
        private readonly List<Enemy> enemyFolder;

        public UfoSpawner(List<Enemy> enemyFolder)
        {
            this.enemyFolder = enemyFolder;
        }

        public void CreateUfo(Player player, int width, int height)
        {
            var lineHunter = new LineHunterBehavior(player);
            var y = random.Next(0, height);
            var x = random.Next(0, 2) == 0 ? 1 : width;
            var position = new Vector(x, y);
            var ufo = new Ufo(position, lineHunter);
            enemyFolder.Add(ufo);
        }
    }
}
