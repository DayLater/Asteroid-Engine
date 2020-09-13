using AsteroidsEngine.Entities;

namespace TestAsteroids.Models.Enemies
{
    public interface IBehavior
    {
        void Moving(Enemy enemy);
    }
}
