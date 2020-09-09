using AsteroidsEngine.Entities;

namespace TestAsteroids.Models.Enemies
{
    public class DefaultMovingBehavior: IBehavior
    {
        public void Moving(Enemy enemy) => enemy.ChangePosition(enemy.Position + enemy.Speed);
    }
}
