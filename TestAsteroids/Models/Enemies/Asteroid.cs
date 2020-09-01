﻿using System.Drawing;
using System.Linq;

namespace AsteroidsEngine.Entities
{
    public class Asteroid: Enemy
    {
        private readonly float r;
        public bool IsChild { get; }
        public override int Value => IsChild ? 150 : 100;
        private Vector[] pointToDraw;


        public Asteroid(Vector position, Vector speed, float r, bool isChild = false)
        {
            Position = position;
            Speed = speed;
            IsChild = isChild;
            MainPoints = new Vector[0];
            this.r = r;
            CreatePointToDraw();
            UpdateCoordinates();
        }

        private void CreatePointToDraw()
        {
            pointToDraw = new Vector[12];
            for (int i = 0; i < 12; i++)
            {
                var point = new Vector(0, random.Next(-3, 3) + r).Rotate(Vector.Zero, i * 30) ;
                pointToDraw[i] = point;
            }
        }

        protected override void UpdateCoordinates()
        {
            Position += Speed;
            MainPoints = pointToDraw.Select(v => v + Position).ToArray();
        }

        public override bool Contains(Vector vector)
        {
            var difference = vector - Position;
            return difference.Length < r;
        }
    }
}
