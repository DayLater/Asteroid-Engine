namespace AsteroidsEngine.Entities
{
    public class Bullet : GameObject
    {
        public Bullet(Vector position, float alpha) 
        {
            Position = position;
            Angle = alpha;
            MainPoints = new Vector[2];
            Speed = Vector.Zero;
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
