using System;
using System.Collections.Generic;

namespace AsteroidsEngine.Entities
{
    public class Bullet : GameObject
    {
        public override IEnumerable<Vector> MainVectors 
        {
            get
            {
                yield return (Position + new Vector(0, -5)).Rotate(Position, Angle);
                yield return (Position + new Vector(0, 5)).Rotate(Position, Angle);
            }
        }

        public Bullet(Vector position, float angle) 
        {
            Position = position;
            Angle = angle;
            Speed = new Vector(0, -15).Rotate(Vector.Zero, angle);
        }
    }
}
