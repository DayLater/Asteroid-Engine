using TestAsteroids.Models.Enemies;

namespace AsteroidsEngine.Entities
{
    public class Ufo: Enemy
    {
        private readonly IPlayerHunter hunter;
        public Ellipse Body { get;  }
        public Ellipse Head { get;  }
        public override int Value { get; } = 100;

        public Ufo(Vector position, Player player)
        {
            hunter = new LineHunter(this, player);
            MainPoints = new Vector[0];
            Position = position;
            Body = new Ellipse(30, 15, Position);
            Head = new Ellipse(15, 15, Position + new Vector(0, -Body.Ry * 0.7f));
            Speed = Vector.GetRandomVector(-2, 2);
        }

        protected override void UpdateCoordinates()
        {
            Position += Speed;
            Body.Center = Position;
            Head.Center = Position + new Vector(0, -Body.Ry*0.7f);
            hunter.GoToPlayer();
        }

        public override bool Contains(Vector vector)
        {
            return Body.Contains(vector) || Head.Contains(vector);
        }
    }
}
