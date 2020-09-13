using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AsteroidsEngine
{
    public abstract class GameObject
    {
        public Vector Position { get; protected set; }
        public Vector Speed { get;  set; }
        public float Angle { get; protected set; }
        public virtual IEnumerable<Vector> MainVectors => new Vector[0];

        protected static readonly Random random = new Random();
        private static int nextId;
        private readonly int id;

        protected GameObject()
        {
            id = nextId;
            nextId++;
        }

        internal void ChangePosition(Vector newPosition) => Position = newPosition;

        public virtual IEnumerable<PointF> GetCoordinates() => MainVectors.Select(vector => vector.ToPointF);

        public void Turn(float angle)
        {
            Angle += angle;
        }

        public virtual void Update()
        {
            Position += Speed;
        }

        protected Vector GetRandomNotZeroVector(int minValue, int maxValue)
        {
            int x = 0;
            int y = 0;
            while (x == 0 && y == 0)
            {
                x = (random.Next(minValue, maxValue));
                y = (random.Next(minValue, maxValue));
            }

            return new Vector(x, y);
        }

        public override int GetHashCode() => id;

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return ReferenceEquals(this, obj);
        }
    }
}
