﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace AsteroidsEngine
{
    public abstract class GameObject
    {
        public Vector Position { get; protected set; }
        public Vector Speed { get;  set; }
        public float Angle { get; protected set; }
        public Vector[] MainPoints { get; protected set; }

        protected void Rotate(float angle)
        {
            for (int i = 0; i < MainPoints.Length; i++)
                MainPoints[i] = MainPoints[i].Rotate(Position, angle);
        }

        internal void ChangePosition(Vector newPosition) => Position = newPosition;

        public virtual IEnumerable<PointF> GetCoordinates()
        {
            return MainPoints
                .Select(vector => vector.ToPointF);
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