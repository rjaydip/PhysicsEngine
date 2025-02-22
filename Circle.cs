using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class Circle : Shape
    {
        public float Radius;
        public Circle(Vector position, Vector velocity, float radius, float mass, Color color) : base(position, velocity, mass, color)
        {
            Radius = radius;
        }
        public override void Update(float deltaTime)
        {
            Velocity += Acceleration * deltaTime;
            position += Velocity * deltaTime;
        }
        public override bool IsColided(Shape shape)
        {
            if (shape is Circle circle)
            {
                Vector delta = circle.position - this.position;
                float distance = delta.Length();
                return distance < (this.Radius + circle.Radius);
            }
            else if (shape is Rectangle rectangle)
            {
                return Collision.AreColliding(rectangle, this);
            }
            return false;
        }
        public override void ResolveCollision(Shape shape)
        {
            if (shape is Circle circle)
            {
                Collision.ShapeCollision(this, circle);
            }
            //else if (shape is Rectangle rectangle)
            //{
            //    Collision.ShapeCollision(rectangle,this);
            //}
        }
        public override void ResolveScreenCollision(float screenWidth, float screenHeight)
        {
            if (position.X - Radius < 0)
            {
                position.X = Radius;
                Velocity.X *= -1;
            }
            else if (position.X + Radius > screenWidth)
            {
                position.X = screenWidth - Radius;
                Velocity.X *= -1;
            }

            if (position.Y - Radius < 0)
            {
                position.Y = Radius;
                Velocity.Y *= -1;
            }
            else if (position.Y + Radius > screenHeight)
            {
                position.Y = screenHeight - Radius;
                Velocity.Y *= -1;
            }
        }
        public override void Fill(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), (int)position.X, (int)position.Y, (int)(2 * Radius), (int)(2 * Radius));
        }
        public override void Draw(Graphics g)
        {
            g.DrawEllipse(new Pen(color, 1), (int)position.X, (int)position.Y, (int)(2 * Radius), (int)(2 * Radius));
        }
    }
}
