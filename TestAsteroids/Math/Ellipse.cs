namespace AsteroidsEngine
{
    public class Ellipse
    {
        public float Rx { get; }
        public float Ry { get; }
        public Vector Center { get; set; }

        public Ellipse(float rx, float ry, Vector center)
        {
            Rx = rx;
            Ry = ry;
            Center = center;
        }

        public bool Contains(Vector vector)
        {
            return ((vector.X - Center.X) * (vector.X - Center.X) / (Rx * Rx)
                    + (vector.Y - Center.Y) * (vector.Y - Center.Y) / (Ry * Ry)) <= 1;
        }
    }
}
