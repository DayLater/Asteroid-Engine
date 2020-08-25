using System;
using System.Collections.Generic;

namespace AsteroidsEngine.Entities
{
    public class Laser
    {
        private Vector[] laserPoints = new Vector[0];
        private readonly List<Vector> pointsToBuildLaser = new List<Vector>();
        private readonly Player player;

        private int laserActiveTicks;
        private readonly int maxLaserTicks = 40;

        private bool isLaserActive;
        private bool isReload = true;

        private int currentReloadTicks;
        private readonly int needToReloadTicks = 500;

        public static event Action OnReady;
        public static event Action<double> OnReload;

        public Laser(Player player, int width, int height)
        {
            this.player = player;

            var maxDistance = (int)System.Math.Sqrt(width * width + height *height);

            for (int i = 0; i > -maxDistance; i -= 5)
                pointsToBuildLaser.Add(new Vector(0, i));
        }

        public bool Activate()
        {
            if (!isReload)
            {
                isReload = true;
                laserPoints = new Vector[pointsToBuildLaser.Count];
                isLaserActive = true;
                laserActiveTicks = 0;
                UpdatePosition();
                return true;
            }

            return false;
        }

        private void UpdatePosition()
        {
            for (int i = 0; i < laserPoints.Length; i++)
                laserPoints[i] = (player.MainPoints[0] + pointsToBuildLaser[i])
                    .Rotate(player.Position, player.Angle);
        }

        private void LaserOn()
        {
            UpdatePosition();
            laserActiveTicks++;
            if (laserActiveTicks >= maxLaserTicks)
            {
                laserPoints = new Vector[0];
                isLaserActive = false;
            }
        }

        private void Waiting()
        {
            if (currentReloadTicks >= needToReloadTicks)
            {
                currentReloadTicks = 0;
                isReload = false;
                OnReady?.Invoke();
            }
            else if (isReload)
            {
                currentReloadTicks++;
                OnReload?.Invoke(((double)currentReloadTicks / maxLaserTicks) * 8);
            }
        }

        public void Update()
        {
            if (isLaserActive)
                LaserOn();
            else Waiting();
        }

        public Vector[] GetPoints()
        {
            return new List<Vector>(laserPoints)
                .ToArray();
        }
    }
}
