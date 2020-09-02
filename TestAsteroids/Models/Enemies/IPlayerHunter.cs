using AsteroidsEngine.Entities;

namespace TestAsteroids.Models.Enemies
{
    public interface IPlayerHunter
    {
        void GoToPlayer(Enemy enemy);
    }
}
