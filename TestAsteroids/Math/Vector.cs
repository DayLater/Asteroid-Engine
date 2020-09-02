using System;
using System.Drawing;

namespace AsteroidsEngine
{
    public class Vector
    {
        private static readonly Random random = new Random();
        public float X { get; }
        public float Y { get; }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector GetRandomVector(int minValue, int maxValue)
        {
            var x = (float)(random.Next(minValue, maxValue));
            var y = (float)(random.Next(minValue, maxValue));
            return new Vector(x, y);
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

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
}
