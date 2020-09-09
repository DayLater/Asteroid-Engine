using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsteroidsEngine.Entities
{
    public class Laser
    {
        private readonly List<Vector> vectorsToBuildLaser = new List<Vector>();
        private readonly Player player;

        private bool isActive;
        private bool canActivate;
        private bool breakReload;

        public static event Action<double> OnReload;
        private const int ReloadTime = 10;

        public Laser(Player player, int width, int height)
        {
            GameModel.GameOver += () => breakReload = true;
            GameModel.GameStart += () => ReloadAsync(ReloadTime);

            this.player = player;
            CreateVectorsToBuildLaser(width, height);
        }

        private void CreateVectorsToBuildLaser(int width, int height)
        {
            var maxDistance = (int)Math.Sqrt(width * width + height * height);
            for (var i = 0; i > -maxDistance; i -= 5)
                vectorsToBuildLaser.Add(new Vector(0, i));
        }

        public bool TryActivate()
        {
            if (!canActivate) return false;
            ActivateAsync(1);
            return true;
        }

        private async void ActivateAsync(int seconds)
        {
            canActivate = false;
            isActive = true;
            await Task.Delay(seconds * 1000);
            isActive = false;
            ReloadAsync(ReloadTime);
        }

        private async void ReloadAsync(int seconds)
        {
            for (var i = 0; i < 100; i++)
            {
                if (breakReload) return;
                await Task.Delay(seconds * 10);
                OnReload?.Invoke(i);
            }
            canActivate = true;
        }

        public IEnumerable<Vector> Vectors
        {
            get
            {
                if (!isActive) return new Vector[0];
                var startLaserPosition = player.MainVectors
                    .FirstOrDefault()?
                    .Rotate(player.Position, -player.Angle);

                return vectorsToBuildLaser
                    .Select(v => (startLaserPosition + v).Rotate(player.Position, player.Angle));
            }
        }
    }
}
