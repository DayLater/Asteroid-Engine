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
            MainPoints = new Vector[5];
            Speed = Vector.Zero;
            Laser = new Laser(this, width, height);
            UpdateCoordinates();
        }

        protected override void UpdateCoordinates()
        {
            Speed *= .98f;
            Position += Speed;
            MainPoints[0] = new Vector(Position.X, Position.Y - 25);
            MainPoints[1] = new Vector(Position.X - 15, Position.Y + 25);
            MainPoints[2] = new Vector(Position.X + 15, Position.Y + 25);
            MainPoints[3] = (MainPoints[0] + MainPoints[1]) * 0.5f;
            MainPoints[4] = (MainPoints[0] + MainPoints[2]) * 0.5f;
        }

        public bool TryActivateLaser()
        {
            return Laser.TryActivate();
        }

        public override PointF[] GetCoordinates()
        {
            return new PointF[]
            {
                MainPoints[0].ToPointF, 
                MainPoints[1].ToPointF,
                MainPoints[2].ToPointF
            };
        }

        public Bullet Shoot()
        {
            return new Bullet(MainPoints[0], Angle);
        }

        public void SpeedUp()
        {
            Vector acceleration = new Vector(0, -0.5f);
            acceleration = acceleration.Rotate(Vector.Zero, Angle);
            Speed += acceleration;
        }
    }
}
