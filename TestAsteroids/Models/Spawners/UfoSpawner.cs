using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var y = random.Next(0, height);
            var x = random.Next(0, 2) == 0 ? 1 : width;
            var position = new Vector(x, y);
            var ufo = new Ufo(position, player);
            enemyFolder.Add(ufo);
        }
    }
}
