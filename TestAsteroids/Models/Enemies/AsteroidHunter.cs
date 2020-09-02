using AsteroidsEngine;
using AsteroidsEngine.Entities;

namespace TestAsteroids.Models.Enemies
{
    public class AsteroidHunter: Asteroid
    {
        private IPlayerHunter hunter;

        public AsteroidHunter(Vector position, float r, IPlayerHunter hunter, bool isChild = false)
            : base(position, r, isChild)
        {
            this.hunter = hunter;
        }

        protected override void UpdateCoordinates()
        {
            base.UpdateCoordinates();
            hunter.GoToPlayer(this);
        }
    }
}
