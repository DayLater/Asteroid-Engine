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

        public override void Update()
        {
            base.Update();
            hunter.GoToPlayer(this);
        }
    }
}
