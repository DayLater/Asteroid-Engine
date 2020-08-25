namespace AsteroidsEngine.Entities
{
    public class Ufo: Enemy
    {
        private readonly Player player;
        private int tickCount;
        private readonly int tickCountToFindPlayer = 40;
        public Ellipse Body { get;  }
        public Ellipse Head { get;  }
        public override int Value { get; } = 100;

        public Ufo(Vector position, Player player)
        {
            MainPoints = new Vector[0];
            this.player = player;
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
            tickCount++;
            if (tickCount >= tickCountToFindPlayer)
            {
                tickCount = 0;
                GoToPlayer();
            }
        }

        private void GoToPlayer()
        {
            var way = random.Next(0, 2);
            var pos = player.Position;
            var speed = Vector.Zero;
            if (way == 0)
            {
                if (pos.X > Position.X)
                    speed = new Vector(2, 0);
                else if (pos.X < Position.X)
                    speed = new Vector(-2, 0);
            }
            else
            {
                if (pos.Y > Position.Y)
                    speed = new Vector(0, 2);
                else if (pos.Y < Position.Y)
                    speed = new Vector(0, -2);
            }

            Speed = speed;
        }

        public override bool Contains(Vector vector)
        {
            return Body.Contains(vector) || Head.Contains(vector);
        }
    }
}
