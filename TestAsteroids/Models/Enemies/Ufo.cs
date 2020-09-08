using TestAsteroids.Models.Enemies;

namespace AsteroidsEngine.Entities
{
    public class Ufo: Enemy
    {
        private readonly IPlayerHunter hunter;
        public Ellipse Body { get;  }
        public Ellipse Head { get;  }
        public override int Value { get; } = 100;

        public Ufo(Vector position, IPlayerHunter hunter)
        {
            this.hunter = hunter;
            Position = position;
            Body = new Ellipse(30, 15, Position);
            Head = new Ellipse(15, 15, Position + new Vector(0, -Body.Ry * 0.7f));
            Speed = GetRandomNotZeroVector(-2, 2);
        }

        public override void Update()
        {
            Position += Speed;
            Body.Center = Position;
            Head.Center = Position + new Vector(0, -Body.Ry*0.7f);
            hunter.GoToPlayer(this);
        }

        public override bool Contains(Vector vector)
        {
            return Body.Contains(vector) || Head.Contains(vector);
        }
    }
}
