﻿using System;

namespace AsteroidsEngine.Entities
{
    public abstract class Enemy: GameObject
    {
        public abstract int Value { get;  }
        protected readonly Random random = new Random();

        public abstract bool Contains(Vector vector);
        public bool IsCollision(GameObject gameObject)
        {
            foreach (var point in gameObject.MainPoints)
            {
                if (Contains(point))
                    return true;
            }

            return false;
        }
    }
}
