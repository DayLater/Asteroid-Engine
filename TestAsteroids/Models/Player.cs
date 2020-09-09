using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AsteroidsEngine.Entities;

namespace AsteroidsEngine
{
    public class Player : GameObject
    {
        public Laser Laser { get; }
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
            get { return GetMainVectors().Select(vector => vector.Rotate(Position, Angle)); }
        }

        private IEnumerable<Vector> GetMainVectors()
        {
            var vector1 = new Vector(Position.X, Position.Y - 25);
            var vector2 = new Vector(Position.X - 15, Position.Y + 25);
            var vector3 = new Vector(Position.X + 15, Position.Y + 25);
            yield return vector1;
            yield return vector2;
            yield return vector3;
            yield return (vector1 + vector2) * 0.5f;
            yield return (vector1 + vector3) * 0.5f;
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
