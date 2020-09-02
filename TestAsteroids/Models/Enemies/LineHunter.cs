using System;
using AsteroidsEngine;
using AsteroidsEngine.Entities;

namespace TestAsteroids.Models.Enemies
{
    public class LineHunter: IPlayerHunter
    {
        private readonly Random random = new Random();
        private readonly Player player;
        private int tickCount;
        private readonly int tickCountToGoToPlayer;

        public LineHunter(Player player, int tickCountToGoToPlayer = 40)
        { 
            this.player = player;
            this.tickCountToGoToPlayer = tickCountToGoToPlayer;
        }

        public void GoToPlayer(Enemy enemy)
        {
            tickCount++;
            if (tickCount >= tickCountToGoToPlayer)
            {
                tickCount = 0;
                ChangeSpeed(enemy);
            }
        }

        private void ChangeSpeed(Enemy enemy)
        {
            var way = random.Next(0, 2);
            var pos = player.Position;
            var newSpeed = Vector.Zero;
            if (way == 0)
            {
                if (pos.X > enemy.Position.X)
                    newSpeed = new Vector(2, 0);
                else if (pos.X < enemy.Position.X)
                    newSpeed = new Vector(-2, 0);
            }
            else
            {
                if (pos.Y > enemy.Position.Y)
                    newSpeed = new Vector(0, 2);
                else if (pos.Y < enemy.Position.Y)
                    newSpeed = new Vector(0, -2);
            }

            enemy.Speed = newSpeed;
        }
    }
}
