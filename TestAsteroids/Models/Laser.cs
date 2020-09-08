using System;
using System.Collections.Generic;
using System.Linq;

namespace AsteroidsEngine.Entities
{
    public class Laser
    {
        private Vector[] laserPoints = new Vector[0];
        private readonly List<Vector> pointsToBuildLaser = new List<Vector>();
        private readonly Player player;

        private int laserActiveTicks;
        private readonly int maxLaserTicks = 40;

        private bool isReload = true;

        private int currentReloadTicks;
        private readonly int needToReloadTicks = 500;

        public static event Action OnReady;
        public static event Action<double> OnReload;

        public Laser(Player player, int width, int height)
        {
            this.player = player;
            GameModel.OnTick += Reload;
            var maxDistance = (int)Math.Sqrt(width * width + height *height);
            for (int i = 0; i > -maxDistance; i -= 5)
                pointsToBuildLaser.Add(new Vector(0, i));
        }

        public bool TryActivate()
        {
            if (isReload) return false;
            GameModel.OnTick += LaserOn;
            isReload = true;
            laserPoints = new Vector[pointsToBuildLaser.Count];
            laserActiveTicks = 0;
            return true;
        }

        private void UpdatePosition()
        {
            var startLaserPosition = player.MainVectors.FirstOrDefault()?.Rotate(player.Position, -player.Angle);
            for (var i = 0; i < laserPoints.Length; i++)
                laserPoints[i] = (startLaserPosition + pointsToBuildLaser[i])
                    .Rotate(player.Position, player.Angle);
        }

        private void LaserOn()
        {
            UpdatePosition();
            laserActiveTicks++;
            if (laserActiveTicks < maxLaserTicks) return;

            laserPoints = new Vector[0];
            GameModel.OnTick -= LaserOn;
            GameModel.OnTick += Reload;
        }

        private void Reload()
        {
            if (currentReloadTicks >= needToReloadTicks)
            {
                currentReloadTicks = 0;
                isReload = false;
                OnReady?.Invoke();
                GameModel.OnTick -= Reload;
            }
            else if (isReload)
            {
                currentReloadTicks++;
                OnReload?.Invoke(((double)currentReloadTicks / needToReloadTicks) * 100);
            }
        }

        public Vector[] GetPoints()
        {
            return new List<Vector>(laserPoints)
                .ToArray();
        }
    }
}
