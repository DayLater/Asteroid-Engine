using System.Collections.Generic;
using TestAsteroids.Models.Enemies;

namespace AsteroidsEngine.Entities
{
    public class Ufo: Enemy
    {
        public Ellipse Body => new Ellipse(30, 15, Position);
        public Ellipse Head => new Ellipse(15, 15, Position + new Vector(0, -Body.Ry * 0.7f));

        public override int Value { get; } = 100;

        public Ufo(Vector position, IBehavior behavior) : base(behavior)
        {
            Position = position;
            Speed = GetRandomNotZeroVector(-2, 2);
        }

        public override bool Contains(Vector vector)
        {
            return Body.Contains(vector) || Head.Contains(vector);
        }
    }
}
