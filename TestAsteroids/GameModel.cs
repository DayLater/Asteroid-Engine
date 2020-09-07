﻿using System;
using System.Collections.Generic;
using System.Linq;
using AsteroidsEngine.Entities;
using AsteroidsEngine.Entities.Spawners;
using TestAsteroids.Models.Spawners;

namespace AsteroidsEngine
{
    public class GameModel
    {
        public EnemySpawner EnemySpawner { get; private set; }
        public BulletsFolder BulletsFolder { get; private set; }
        public Player Player { get; private set; }

        public event Action GameOver;
        public event Action OnEnemyDeath; 

        private readonly int width;
        private readonly int height;
        public int Score { get; private set; }
        private int level;
        private int tickPassed;
        private int ticksForWave;
        public static event Action OnTick; 


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
            BulletsFolder = new BulletsFolder();
            level = 5;
            Score = 0;
            OnEnemyDeath?.Invoke();
            EnemySpawner.CreateAsteroidWave(level, Player);
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

        public IEnumerable<GameObject> AllGameObjects()
        {
            yield return Player;
            foreach (var enemy in EnemySpawner)
                yield return enemy;
            foreach (var bullet in BulletsFolder)
                yield return bullet;
        }

        public void MakeShoot() => BulletsFolder.MakeShoot(Player.Angle, Player.Position);

        private void UpdateAllObjects()
        {
            foreach (var gameObject in AllGameObjects())
            {
                gameObject.Update();
                var isOut = IsOutOfMap(gameObject);
                if (gameObject is Bullet bullet && bullet.MainPoints.Select(DestroyIfHit).Any(isDestroyed => isOut || isDestroyed))
                    BulletsFolder.AddBulletToDelete(bullet);
                else if (isOut) 
                    ReturnToMap(gameObject);
            }

            BulletsFolder.DeleteNonActiveBullets();
        }

        public void Update()
        {
            UpdateAllObjects();            
            OnTick?.Invoke();
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
            EnemySpawner.CreateAsteroidWave(level, Player);
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
