using System.Collections.Generic;
using System.Drawing;
using AsteroidsEngine.Entities;

namespace AsteroidsEngine
{
    public class Player : GameObject
    {
        public Laser Laser { get;  }
        public Player(int width, int height)
        {
            Position = new Vector(width / 2, height / 2);
            Speed = Vector.Zero;
            Laser = new Laser(this, width, height);
        }

        public override void Update()
        {
            Speed *= .98f;
            Position += Speed;
        }

        public override IEnumerable<Vector> MainVectors 
        {
            get
            {
                var vector1 = new Vector(Position.X, Position.Y - 25);
                var vector2 = new Vector(Position.X - 15, Position.Y + 25);
                var vector3 = new Vector(Position.X + 15, Position.Y + 25);
                var vector4 = (vector1 + vector2) * 0.5f;
                var vector5 = (vector1 + vector3) * 0.5f;
                var vectors = new HashSet<Vector>(5) {vector1, vector2, vector3, vector4, vector5};

                foreach (var vector in vectors)
                    yield return vector.Rotate(Position, Angle);
            }
        }

        public bool TryActivateLaser()
        {
            return Laser.TryActivate();
        }

        public override IEnumerable<PointF> GetCoordinates()
        {
            int count = 0;
            foreach (var vector in MainVectors)
            {
                yield return vector.ToPointF;
                count++;
                if (count == 3) yield break;
            }
        }

        public void SpeedUp()
        {
            Vector acceleration = new Vector(0, -0.5f);
            acceleration = acceleration.Rotate(Vector.Zero, Angle);
            Speed += acceleration;
        }
    }
}
