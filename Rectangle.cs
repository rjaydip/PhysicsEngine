using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class Rectangle : Shape
    {
        public Vector Scale { get; set; }
        public Rectangle(Vector position, Vector velocity, Vector scale, float mass, Color color) : base(position, velocity, mass, color)
        {
            Scale = scale;
        }
        public override void Update(float deltaTime)
        {
            Velocity += Acceleration * deltaTime;
            position += Velocity * deltaTime;
        }
        public override bool IsColided(Shape shape)
        {
            if (shape is Rectangle rectangle)
            {
                return this.position.X < rectangle.position.X + rectangle.Scale.X &&
                       this.position.X + this.Scale.X > rectangle.position.X &&
                       this.position.Y < rectangle.position.Y + rectangle.Scale.Y &&
                       this.position.Y + this.Scale.Y > rectangle.position.Y;
            }
            else if (shape is Circle circle)
            {
                return Collision.AreColliding(this, circle);
            }

            return false;
        }
        public override void ResolveCollision(Shape shape)
        {
            throw new NotImplementedException();
            //if (shape is Rectangle rectangle)
            //{
            //    Collision.ShapeCollision(this, rectangle);
            //}
            //else if (shape is Circle circle)
            //{
            //    Collision.ShapeCollision(this, circle);
            //}
        }
        public override void ResolveScreenCollision(float screenWidth, float screenHeight)
        {
            if (position.X < 0 || position.X + Scale.X > screenWidth)
            {
                Velocity.X *= -1;
                if (position.X < 0) position.X = 0;
                if (position.X + Scale.X > screenWidth) position.X = screenWidth - Scale.X;
            }
            if (position.Y < 0 || position.Y + Scale.Y > screenHeight)
            {
                Velocity.Y *= -1;
                if (position.Y < 0) position.Y = 0;
                if (position.Y + Scale.Y > screenHeight) position.Y = screenHeight - Scale.Y;
            }
        }
        public override void Fill(Graphics g)
        {
            g.FillRectangle(new SolidBrush(color), (int)position.X, (int)position.Y, (int)Scale.X, (int)Scale.Y);
        }
        public override void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(color, 5), (int)position.X, (int)position.Y, (int)Scale.X, (int)Scale.Y);
        }
    }
}
