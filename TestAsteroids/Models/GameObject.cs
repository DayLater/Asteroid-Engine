using System.Drawing;
using System.Linq;

namespace AsteroidsEngine
{
    public abstract class GameObject
    {
        public Vector Position { get; protected set; }
        protected Vector Speed { get;  set; }
        public float Angle { get; protected set; }
        public Vector[] MainPoints { get; protected set; }

        protected void Rotate(float angle)
        {
            for (int i = 0; i < MainPoints.Length; i++)
                MainPoints[i] = MainPoints[i].Rotate(Position, angle);
        }

        internal void ChangePosition(Vector newPosition) => Position = newPosition;

        public virtual PointF[] GetCoordinates()
        {
            return MainPoints
                .Select(vector =>vector.ToPointF)
                .ToArray();
        }

        public void Turn(float angle)
        {
            Angle += angle;
        }

        protected virtual void UpdateCoordinates()
        {
            Position += Speed;
        }

        public void Update()
        {
            UpdateCoordinates();
            Rotate(Angle);
        }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
