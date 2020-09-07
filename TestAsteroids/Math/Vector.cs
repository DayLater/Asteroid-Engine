using System;
using System.Drawing;

namespace AsteroidsEngine
{
    public class Vector
    {
        public float X { get; }
        public float Y { get; }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator+(Vector vector, Vector other)
        {
            return new Vector(vector.X + other.X, vector.Y + other.Y);
        }

        public static Vector operator -(Vector vector, Vector other)
        {
            return new Vector(vector.X - other.X, vector.Y - other.Y);
        }

        public static Vector operator *(Vector vector, float factor)
        {
            return new Vector(vector.X * factor, vector.Y * factor);
        }

        public static Vector operator *(float factor, Vector vector)
        {
            return vector * factor;
        }

        public float Length => (float)Math.Sqrt(X * X + Y * Y);

        public PointF ToPointF => new PointF(X, Y);

        public float Angle => (float) Math.Atan2(Y, X);

        public static Vector Zero => new Vector(0,0);

        public Vector Rotate(Vector center, float deltaAlpha)
        {
            Vector deltaVector = this - center;
            float radius = deltaVector.Length;

            float alpha = deltaVector.Angle;
            alpha -= (float)(deltaAlpha / 180 * Math.PI);

            var x = (float)(center.X + radius * Math.Cos(alpha));
            var y = (float)(center.Y + radius * Math.Sin(alpha));
            return new Vector(x, y);
        }


        private bool FloatEquals(float a, float b)
        {
            return Math.Abs(a - b) < 1e-6;
        }

        public bool Equals(Vector other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return FloatEquals(X, other.X) && FloatEquals(Y, other.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return GetType() == obj.GetType() && Equals((Vector) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
}
