using TestAsteroids.Models.Enemies;

namespace AsteroidsEngine.Entities
{
    public class Asteroid: Enemy
    {
        private readonly float r;
        public bool IsChild { get; }
        public override int Value => IsChild ? 150 : 100;

        public Asteroid(Vector position, float r, IBehavior behavior, bool isChild = false) : base(behavior)
        {
            IsChild = isChild;
            Speed = isChild? GetRandomNotZeroVector(-3, 3) : GetRandomNotZeroVector(-2,2);
            Position = isChild? position + GetRandomNotZeroVector(-1, 1): position;
            this.r = r;
        }

        public override bool Contains(Vector vector)
        {
            var difference = vector - Position;
            return difference.Length < r;
        }
    }
}
