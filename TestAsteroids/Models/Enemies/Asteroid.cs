using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AsteroidsEngine.Entities
{
    public class Asteroid: Enemy
    {
        private readonly float r;
        public bool IsChild { get; }
        public override int Value => IsChild ? 150 : 100;

        private Vector[] vectorsToDraw;
        public override IEnumerable<Vector> MainVectors
        {
            get 
            { 
                return vectorsToDraw.Select(vector => (vector + Position)
                    .Rotate(Position, Angle));
            }
        }

        public Asteroid(Vector position, float r, bool isChild = false)
        {
            IsChild = isChild;

            Speed = isChild? GetRandomNotZeroVector(-3, 3) : GetRandomNotZeroVector(-2,2);
            Position = isChild? position + GetRandomNotZeroVector(-1, 1): position;

            this.r = r;
            CreatePointToDraw();
        }

        private void CreatePointToDraw()
        {
            vectorsToDraw = new Vector[12];
            for (int i = 0; i < 12; i++)
            {
                var point = new Vector(0, random.Next(-3, 3) + r).Rotate(Vector.Zero, i * 30) ;
                vectorsToDraw[i] = point;
            }
        }

        public override bool Contains(Vector vector)
        {
            var difference = vector - Position;
            return difference.Length < r;
        }
    }
}
