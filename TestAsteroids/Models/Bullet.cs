namespace AsteroidsEngine.Entities
{
    public class Bullet : GameObject
    {
        public Bullet(Vector position, float alpha) 
        {
            Position = position;
            Angle = alpha;
            Speed = new Vector(0, -15);
            Speed = Speed.Rotate(Vector.Zero, alpha);
            MainPoints = new Vector[2];
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
