using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class Game : Engine
    {
        Shape shape;
        int speed = 5;
        public Game() : base(new Vector(400,400), "Game")
        {

        }
        public override void OnLoad()
        {
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                new Circle(new Vector(random.Next(400), random.Next(400)), new Vector(0, random.Next(4000,5000)), 10,1.0f, Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
            }
        } 
        public override void OnUpdate()
        {
        }
    }
}
