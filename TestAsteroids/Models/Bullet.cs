using System;

namespace AsteroidsEngine.Entities
{
    public class Bullet : GameObject
    {
        public Bullet(Vector position, float angle) 
        {
            Position = position;
            Angle = angle;
            MainPoints = new Vector[2];
            Speed = new Vector(0, -15).Rotate(Vector.Zero, angle);
            UpdateCoordinates();
        }

        protected override void UpdateCoordinates()
        {
            Position += Speed;
            MainPoints[0] = Position + new Vector(0, -5);
            MainPoints[1] = Position + new Vector(0, 5);
        }
    }
}
