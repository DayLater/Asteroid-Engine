using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsEngine.Entities.Spawners
{
    public class EnemySpawner: IEnumerable<Enemy>
    {
        private readonly AsteroidsSpawner asteroidsSpawner;
        private readonly UfoSpawner ufoSpawner;
        private readonly List<Enemy> enemyFolder;
        private readonly int width;
        private readonly int height;

        public EnemySpawner(int width, int height)
        {
            this.width = width;
            this.height = height;
            enemyFolder = new List<Enemy>();
            asteroidsSpawner = new AsteroidsSpawner(enemyFolder);
            ufoSpawner = new UfoSpawner(enemyFolder);
        }

        public void CreateAsteroidWave(int level, Player player)
        {
            asteroidsSpawner.CreateBigAsteroids(level, width, height);
            asteroidsSpawner.CreateHunterAsteroid(width, height, player);
        }


        public void CreateChildAsteroids(Asteroid asteroid)
        {
            asteroidsSpawner.CreateChildAsteroids(asteroid);
        }

        public void CreateUfo(Player player)
        {
            ufoSpawner.CreateUfo(player, width, height);
        }

        public int Count => enemyFolder.Count;

        public Enemy this[int index]
        {
            get
            {
                if (index > enemyFolder.Count || index < 0)
                    throw new IndexOutOfRangeException();
                return enemyFolder[index];
            }
        }

        public void Remove(Enemy enemy)
        {
            if (enemy is Asteroid asteroid && !asteroid.IsChild)
                CreateChildAsteroids(asteroid);
            enemyFolder.Remove(enemy);
        }

        public int AsteroidCount()
        {
            var count = 0;
            foreach (var enemy in enemyFolder)
                if (enemy is Asteroid asteroid)
                    count++;
            return count;
        }

        #region IEmumerator
        public IEnumerator<Enemy> GetEnumerator()
        {
            foreach (var enemy in enemyFolder)
                yield return enemy;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
