using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsteroidsEngine;
using AsteroidsEngine.Entities;

namespace TestAsteroids.Models.Enemies
{
    public class LineHunter: IPlayerHunter
    {
        private readonly Random random = new Random();
        private readonly Player player;
        private readonly Enemy enemy;
        private int tickCount;
        private readonly int tickCountToFindPlayer;

        public LineHunter(Enemy enemy, Player player, int tickCountToFindPlayer = 40)
        {
            this.enemy = enemy;
            this.player = player;
            this.tickCountToFindPlayer = tickCountToFindPlayer;
        }

        public void GoToPlayer()
        {
            tickCount++;
            if (tickCount >= tickCountToFindPlayer)
            {
                tickCount = 0;
                ChangeSpeed();
            }
        }

        private void ChangeSpeed()
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
