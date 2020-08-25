using System;
using System.Collections.Generic;
using System.Linq;
using AsteroidsEngine.Entities;
using AsteroidsEngine.Entities.Spawners;

namespace AsteroidsEngine
{
    public class GameModel
    {
        public EnemySpawner EnemySpawner { get; private set; }
        public Player Player { get; private set; }
        public List<Bullet> Bullets { get; private set; }

        public event Action GameOver;
        public event Action OnEnemyDeath; 

        private readonly int width;
        private readonly int height;
        public int Score { get; private set; }
        private int level;
        private int tickPassed;
        private int ticksForWave;

        public GameModel(int width, int height)
        {
            this.width = width;
            this.height = height;
            StartGame();
        }

        public void StartGame()
        {
            EnemySpawner = new EnemySpawner(width, height);
            Player = new Player(width, height);
            Bullets = new List<Bullet>();
            level = 5;
            Score = 0;
            OnEnemyDeath?.Invoke();
            EnemySpawner.CreateAsteroidWave(level);
        }

        private bool IsOutOfMap(GameObject gameObject)
        {
            return gameObject.Position.X > width || gameObject.Position.X < 0 
                                                 || gameObject.Position.Y > height 
                                                 || gameObject.Position.Y < 0;
        }

        private void ReturnToMap(GameObject gameObject)
        {
            if (gameObject.Position.X > width)
                gameObject.ChangePosition(new Vector(2, gameObject.Position.Y));
            else if (gameObject.Position.X < 0)
                gameObject.ChangePosition(new Vector(width - 2, gameObject.Position.Y));
            else if (gameObject.Position.Y > height)
                gameObject.ChangePosition(new Vector(gameObject.Position.X, 2));
            else if (gameObject.Position.Y < 0)
                gameObject.ChangePosition(new Vector(gameObject.Position.X, height - 2));
        }

        private IEnumerable<GameObject> UpdatedObjects()
        {
            yield return Player;
            foreach (var enemy in EnemySpawner)
                yield return enemy;
        }

        public void MakeShoot() => Bullets.Add(Player.Shoot());

        private void UpdateAllObjects()
        {
            UpdateBullets();
            foreach (var gameObject in UpdatedObjects())
            {
                gameObject.Update();
                if (IsOutOfMap(gameObject))
                    ReturnToMap(gameObject);
            }
        }

        public void Update()
        {
            UpdateAllObjects();
            if (IsGameOver())
                GameOver?.Invoke();

            if (EnemySpawner.AsteroidCount() == 0 || ticksForWave % 1200 == 0)
                StartNextWave();

            tickPassed++;
            ticksForWave++;
            if (tickPassed % 500 == 0)
                EnemySpawner.CreateUfo(Player);

            DestroyByLaser();
        }

        private void StartNextWave()
        {
            level++;
            ticksForWave = 0;
            EnemySpawner.CreateAsteroidWave(level);
        }

        private void UpdateBullets()
        {
            int i = 0;
            while (i < Bullets.Count)
            {
                var bullet = Bullets[i];
                bullet.Update();
                var isoOut = IsOutOfMap(bullet);
                foreach (var point in bullet.MainPoints)
                {
                    var isDestroyed = DestroyIfHit(point);
                    if (isoOut || isDestroyed)
                    {
                        Bullets.Remove(bullet);
                        break;
                    }
                } 
                i++;
            }
        }

        private void DestroyByLaser()
        {
            var laserPoints = Player.Laser.GetPoints();
            foreach (var laserPoint in laserPoints)
                DestroyIfHit(laserPoint);
        }

        private bool DestroyIfHit(Vector possibleHit)
        {
            int i = 0;
            while (i < EnemySpawner.Count)
            {
                var enemy = EnemySpawner[i];
                if (enemy.Contains(possibleHit))
                {
                    Score += enemy.Value;
                    OnEnemyDeath?.Invoke();
                    EnemySpawner.Remove(enemy);
                    return true;
                }
                i++;
            }
            return false;
        }

        private bool IsGameOver()
        {
            return EnemySpawner
                .Any(enemy => enemy.IsCollision(Player));
        }
    }
}
