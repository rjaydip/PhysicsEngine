using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

namespace PhysicsEngine
{
    public abstract class Shape
    {
        public Vector position { get; set; }
        public Vector Velocity { get; set; }
        public Vector Acceleration { get; set; } = new Vector(0, 0);
        public Color color = Color.Black;
        public float Mass { get; set; }
        public override string ToString() => $"Position : {position}, Velocity : {Velocity}, Acceleration : {Acceleration}, color : {color}";
        public Shape(Vector position, Vector velocity, float Mass, Color color)
        {
            this.position = position;
            this.Velocity = velocity;
            this.Acceleration = new Vector(0, 0);
            this.Mass = Mass;
            this.color = color;
            Engine.RegisterShape(this);
        }
        public void ApplyForce(Vector force)
        {
            Vector acceleration = force / Mass;
            this.Acceleration += acceleration;
        }
        public abstract void Update(float deltaTime);
        public abstract bool IsColided(Shape shape);
        public abstract void ResolveCollision(Shape shape);
        public abstract void ResolveScreenCollision(float screenWidth, float screenHeight);
        public abstract void Fill(Graphics g);
        public abstract void Draw(Graphics g);
    }
}
