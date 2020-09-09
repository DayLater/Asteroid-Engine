using System;
using System.Linq;
using TestAsteroids.Models.Enemies;

namespace AsteroidsEngine.Entities
{
    public abstract class Enemy: GameObject
    {
        public abstract int Value { get;  }
        private IBehavior behavior;

        protected Enemy(IBehavior behavior)
        {
            this.behavior = behavior;
        }

        public abstract bool Contains(Vector vector);

        public bool IsCollision(GameObject gameObject) => gameObject.MainVectors.Any(Contains);

        public void SetBehavior(IBehavior newBehavior) => behavior = newBehavior;

        public override void Update()
        {
            behavior?.Moving(this);
        }
    }
}
